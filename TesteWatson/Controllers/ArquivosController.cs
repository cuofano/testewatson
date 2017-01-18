using Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TesteWatson.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net.Http;

namespace TesteWatson.Controllers
{
    public class ArquivosController : Controller
    {
        // GET: Arquivos
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LerArquivo()
        {
            
            return View();
        }
        [HttpPost]
        
        public ViewResult LerArquivo(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    //upload do arquivo
                    var fileName = Guid.NewGuid() + "_"+Path.GetFileName(file.FileName) ;
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                    string textoDocumento = RetornarDadosArquivo(Path.GetDirectoryName(path), fileName);

                    string textoDocumentoTraduzido = TraduzirPost(textoDocumento);

                    string documentoAnalisado = AnalisarDocumento(textoDocumentoTraduzido, fileName);
                    ViewBag.resultado = documentoAnalisado ;
                    
                    
                }
                catch (Exception ex)
                {
                    ViewBag.resultado = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.resultado = "Arquivo não especificado";
            }

            return View();
        }


        //arquivo = nome do arquivo (caminho completo)
        private string RetornarDadosArquivo (string caminho,string arquivo)
        {
            object missingType = Type.Missing;

            //abrir word
            object word = Path.Combine(caminho,arquivo);
            ApplicationClass applicationclass = new ApplicationClass();
            applicationclass.Documents.Open(ref word, true,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType, false,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType);

            applicationclass.Visible = false;
            //salvartxt
            Document document = applicationclass.ActiveDocument;
            object arquivoText = Path.Combine(caminho, Guid.NewGuid() + ".txt");
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText;
            applicationclass.ActiveDocument.SaveAs(ref arquivoText, ref format,
                                                    ref missingType, ref missingType, ref missingType,
                                                    ref missingType, ref missingType, ref missingType,
                                                    ref missingType, ref missingType, ref missingType,
                                                    ref missingType, ref missingType, ref missingType,
                                                   ref missingType, ref missingType);


            
            //Close the word document
            document.Close(ref missingType, ref missingType, ref missingType);

            using (StreamReader fs = new StreamReader(arquivoText.ToString(), Encoding.GetEncoding("Windows-1252")))
            {
                return fs.ReadToEnd();

            }
            
            /*
            //Delete the Html File
            File.Delete(htmlFilePath.ToString());
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                File.Delete(file);
            }
            */
        }

        private string TraduzirPost(string mensagem)
        {
            /*{
              "url": "https://gateway.watsonplatform.net/language-translator/api",
              "password": "k4NCxrQHQKRQ",
              "username": "998e5723-0e90-454e-9f31-2930a7643144"
            } */
            var request = (HttpWebRequest)WebRequest.Create("https://gateway.watsonplatform.net/language-translator/api/v2/translate");

            request.Credentials = new NetworkCredential("998e5723-0e90-454e-9f31-2930a7643144", "k4NCxrQHQKRQ");
            request.ContentType = "application/json";
            request.Method = "POST";
            
            GerarJsonConsulta gjc = new GerarJsonConsulta();
            gjc.source = "pt";
            gjc.target = "en";
            gjc.text = new string[] { mensagem};
            string arqJSon = JsonConvert.SerializeObject(gjc, Formatting.Indented);
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(arqJSon);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //Erro 404 - validar metodo post....
            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string resultado = streamReader.ReadToEnd();
                return resultado;
            }
            
        }

        private string AnalisarDocumento (string textoDocumento,string nomeDocumento)
        {
            string json = "";
            /*{
              "url": "https://gateway.watsonplatform.net/personality-insights/api",
              "password": "za4z3gyaD4TG",
              "username": "75013c0a-5348-4af9-add7-d42a08fea139"
            }*/

            var request = (HttpWebRequest)WebRequest.Create("https://gateway.watsonplatform.net/personality-insights/api/v3/profile?version=2016-10-20&consumption_preferences=true&raw_scores=true");
            
            request.Credentials = new NetworkCredential("75013c0a-5348-4af9-add7-d42a08fea139", "za4z3gyaD4TG");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers.Add("Accept-Language: pt-br");

            ContentItems contentItems = new ContentItems();
            contentItems.language = "en";
            contentItems.contenttype = "text/plain";
            contentItems.id = nomeDocumento;
            contentItems.content = textoDocumento;

            JSONInputAnalise jsonInputAnalise = new JSONInputAnalise();
            jsonInputAnalise.contentItems.Add(contentItems);

            json = JsonConvert.SerializeObject(jsonInputAnalise, Formatting.Indented);

           

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string resultado = streamReader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<RetornoApiPerfil>(resultado);
                return JsonConvert.SerializeObject(model, Formatting.Indented);//resultado;
            }
        }


    }
}
