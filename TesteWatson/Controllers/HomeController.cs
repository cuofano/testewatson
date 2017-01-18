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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Traducao()
        {
           
            return View();
        }

        [HttpPost]
        public ViewResult Traducao(TraducaoModels traducaoResposta)
        {
            ViewBag.resultado = Traduzir(traducaoResposta);
            return View();
        }

        private string Traduzir(TraducaoModels traducao)
        {
            /*{
              "url": "https://gateway.watsonplatform.net/language-translator/api",
              "password": "k4NCxrQHQKRQ",
              "username": "998e5723-0e90-454e-9f31-2930a7643144"
            } */
            WebRequest request = WebRequest.Create("https://gateway.watsonplatform.net/language-translator/api/v2/translate?source=" + traducao.strLinguaOriginal 
                                                    + "&target=" + traducao.strLinguaTraducao 
                                                    + "&text=" + traducao.strTextoOriginal
                                                    );

            request.Credentials = new NetworkCredential("998e5723-0e90-454e-9f31-2930a7643144", "k4NCxrQHQKRQ");
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

        }

        

    }
}