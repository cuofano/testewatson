using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TesteWatson.Models;

namespace TesteWatson.Controllers
{
    public class AnalisePerfilController : Controller
    {
        // GET: AnalisePerfil
        public ActionResult Index()
        {
            ViewBag.resultado = Analisar();
            return View();
        }

        private string Analisar()
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
            using (StreamReader sr = new StreamReader(Server.MapPath("~/profile.json")))
            {
                json = sr.ReadToEnd();
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
                return Traduzir(resultado);
            }
        }

        private string Traduzir(string mensagem)
        {
            /*{
              "url": "https://gateway.watsonplatform.net/language-translator/api",
              "password": "k4NCxrQHQKRQ",
              "username": "998e5723-0e90-454e-9f31-2930a7643144"
            } */
            var request = (HttpWebRequest)WebRequest.Create("https://gateway.watsonplatform.net/language-translator/api/v2/translate");

            request.Credentials = new NetworkCredential("998e5723-0e90-454e-9f31-2930a7643144", "k4NCxrQHQKRQ");
            request.ContentType = "application / json";
            var jsontraduz = "{\"source\":\"en\", \"target\":\"pt\":\"text\", mensagem }";
            var result = new JsonResult
            {
                [Data = JsonConvert.DeserializeObject(jsontraduz)
            };
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(result.Data);
                streamWriter.Flush();
                streamWriter.Close();
            }
            request.Method = "POST";
            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string resultado = streamReader.ReadToEnd();
                return resultado;
            }

            /*



            WebResponse response = request.GetResponse();

            using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
            {

                using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var strTraducao = reader.ReadToEnd();
                    //RetornoApiTraducaoModel myojb = (RetornoApiTraducaoModel)js.Deserialize(objText, typeof(RetornoApiTraducaoModel));
                    return strTraducao;
                }

            }
            */

        }
    }
}

