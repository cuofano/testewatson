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
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.resultado = Traduzir();
            return View();
        }

        public string Traduzir()
        {
            
            WebRequest request = WebRequest.Create( "https://gateway.watsonplatform.net/language-translator/api/v2/translate?source=en&target=es&text=hello");
            request.Credentials = new NetworkCredential("237a3d1a-3fc5-4627-84a0-ecc251855166", "3RD2edAA0E7f");
            WebResponse response = request.GetResponse();

            using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
            {

                using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    //RetornoApiTraducaoModel myojb = (RetornoApiTraducaoModel)js.Deserialize(objText, typeof(RetornoApiTraducaoModel));
                    return objText;
                }

            }




            return response.ToString();
        }

        
    }
}