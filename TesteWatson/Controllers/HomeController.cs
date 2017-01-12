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
            
            WebRequest request = WebRequest.Create("https://gateway.watsonplatform.net/language-translator/api/v2/translate?source=" + traducao.strLinguaOriginal 
                                                    + "&target=" + traducao.strLinguaTraducao 
                                                    + "&text=" + traducao.strTextoOriginal
                                                    );

            request.Credentials = new NetworkCredential("237a3d1a-3fc5-4627-84a0-ecc251855166", "3RD2edAA0E7f");
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