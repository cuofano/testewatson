using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteWatson.Models;
using Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Text;
using Newtonsoft.Json;
using System.Data.Entity.Core.EntityClient;

namespace TesteWatson.Controllers
{
    public class arquivobasesController : Controller
    {
        private personalteacherEntities db = new personalteacherEntities();

        // GET: arquivobases
        public ActionResult Index()
        {

            personalteacherEntities entities = new personalteacherEntities();
            ViewBag.aluno = new SelectList(entities.alunoes, "id", "nome" );
            var arquivobases = db.arquivobases.Include(a => a.aluno);
            return View(arquivobases.ToList());
        }
        [HttpPost]
        public ActionResult Index( int[] SelectedFiles)
        {
            List<view_documentotraducao> arquivos = new List<view_documentotraducao>();
            
            arquivos = db.view_documentotraducao.ToList();
            //retornando os IDs dos arquivos
            int documentoAnalisado = AnalisarDocumento(arquivos);
            if (documentoAnalisado == 0) {
                return RedirectToAction("Index"); 
            }
            return RedirectToAction("Details", "analises", new { id = documentoAnalisado });

        }


        private int AnalisarDocumento(List<view_documentotraducao> arquivos)
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

            foreach(view_documentotraducao arq in arquivos)
            {
                contentItems.language = "en";
                contentItems.contenttype = "text/plain";
                contentItems.id = arq.id.ToString();
                string textotraduzido = arq.textotraduzido;
                if (textotraduzido == null) {
                    textotraduzido = TraduzirPost(arq.id,arq.textodocumento);
                }
                contentItems.content = textotraduzido;

                JSONInputAnalise jsonInputAnalise = new JSONInputAnalise();
                jsonInputAnalise.contentItems.Add(contentItems);

                json = JsonConvert.SerializeObject(jsonInputAnalise, Formatting.Indented);
            }

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
                return GravarAnalise(arquivos, model);
            }
        }

        private int GravarAnalise(List<view_documentotraducao> arquivos,RetornoApiPerfil perfil)
        {
            analis analiserealizada = new analis();
            analiserealizada.dtanalise = DateTime.Now;
            analiserealizada.qtdpalavras = perfil.word_count;
            analiserealizada.resultado = JsonConvert.SerializeObject(perfil);
            db.analises.Add(analiserealizada);
            db.SaveChanges();

            int idgerado = db.analises.Max(a=> a.id);
            foreach (view_documentotraducao doctrad in arquivos)
            {
                arquivoanalis arqana = new arquivoanalis();
                arqana.analise_id = idgerado;
                arqana.arquivobase_id = doctrad.id;
                db.arquivoanalises.Add(arqana);
                db.SaveChanges();
            }
            return idgerado;
        }
        private string TraduzirPost(int iddocumento,string mensagem)
        {
            /*{
              "url": "https://gateway.watsonplatform.net/language-translator/api",
              "password": "k4NCxrQHQKRQ",
              "username": "998e5723-0e90-454e-9f31-2930a7643144"
            } */
            var request = (HttpWebRequest)WebRequest.Create("https://gateway.watsonplatform.net/language-translator/api/v2/translate");

            request.Credentials = new NetworkCredential("998e5723-0e90-454e-9f31-2930a7643144", "k4NCxrQHQKRQ");
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("ContentType: application/json");
            request.Method = "POST";

            GerarJsonConsulta gjc = new GerarJsonConsulta();
            gjc.source = "pt";
            gjc.target = "en";
            gjc.text = new string[] { mensagem };
            string arqJSon = JsonConvert.SerializeObject(gjc, Formatting.Indented);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(arqJSon);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //Erro 404 - validar metodo post....
            var httpResponse = (HttpWebResponse)request.GetResponse();

            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            string resultado = streamReader.ReadToEnd();
            var model = JsonConvert.DeserializeObject<RetornoApiTraducaoModel>(resultado);
            return (GravarTraducao(iddocumento, model));
             
        }

        private string GravarTraducao(int arquivoid, RetornoApiTraducaoModel respTraducao)
        {
            
            traducaoarquivo tradArquivo = new traducaoarquivo();
            tradArquivo.arquivobase_id = arquivoid;
            tradArquivo.qtdletras = respTraducao.character_count;
            tradArquivo.qtdpalavras = respTraducao.word_count;
            foreach(Traduzir traduzido in respTraducao.translations)
            {
                tradArquivo.textotraduzido = traduzido.translation;
            }
            db.traducaoarquivoes.Add(tradArquivo);
            db.SaveChanges();
            return tradArquivo.textotraduzido;
        }
        // GET: arquivobases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobas arquivobase = db.arquivobases.Find(id);
            if (arquivobase == null)
            {
                return HttpNotFound();
            }
            return View(arquivobase);
        }

        // GET: arquivobases/Create
        public ActionResult Create()
        {
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "nome");
            return View();
        }

        // POST: arquivobases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create ([Bind(Include = "id,aluno_id,nome,arquivo")] arquivobas arquivobase, HttpPostedFileBase file)
        {
            //transformar em array de byte
            if (file != null && file.ContentLength > 0)
            {

                //GravarArquivo
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                //Retornar array do txt
                string textoDocumento = RetornarDadosArquivo(Path.GetDirectoryName(path), fileName);
                
               
                if (ModelState.IsValid)
                {
                    arquivobase.textodocumento = textoDocumento;
                    arquivobase.arquivo = path;
                    arquivobase.dtupload = DateTime.Now;
                    db.arquivobases.Add(arquivobase);
                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }

                ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", arquivobase.aluno_id);
                return View(arquivobase);
            }
            return View(arquivobase);
        }


        private string RetornarDadosArquivo(string caminho, string arquivo)
        {
            object missingType = Type.Missing;

            //abrir word
            object word = Path.Combine(caminho, arquivo);
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
                       

        }

        private void TextoDoc(MemoryStream target)
        {
            /*object missingType = Type.Missing;

            ApplicationClass applicationclass = new ApplicationClass();
            applicationclass.Documents.Open(ref target, true,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType, false,
                                    ref missingType, ref missingType, ref missingType,
                                    ref missingType, ref missingType);

            applicationclass.Visible = false;
            */

        }

        // GET: arquivobases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobas arquivobase = db.arquivobases.Find(id);
            if (arquivobase == null)
            {
                return HttpNotFound();
            }
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", arquivobase.aluno_id);
            return View(arquivobase);
        }

        // POST: arquivobases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,aluno_id,nome,arquivo")] arquivobas arquivobase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arquivobase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", arquivobase.aluno_id);
            return View(arquivobase);
        }

        // GET: arquivobases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobas arquivobase = db.arquivobases.Find(id);
            if (arquivobase == null)
            {
                return HttpNotFound();
            }
            return View(arquivobase);
        }

        // POST: arquivobases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            arquivobas arquivobase = db.arquivobases.Find(id);
            db.arquivobases.Remove(arquivobase);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
