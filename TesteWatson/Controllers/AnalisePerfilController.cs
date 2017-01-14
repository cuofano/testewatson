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
                //var model = JsonConvert.DeserializeObject<IEnumerable<RetornoApiPerfil>>(resultado);
                var model = JsonConvert.DeserializeObject<RetornoApiPerfil>(resultado);
                return resultado;
            }
        }

    }
}
