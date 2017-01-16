using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TesteWatson.Models;
using static System.Net.Mime.MediaTypeNames;

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
            return View();
        }

        
        /*
        public ViewResult LerArquivo(HttpPostedFileBase file)
        {
            List<DadosArquivos> csvData = new List<DadosArquivos>();

            if (file.ContentLength > 0)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(file.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        csvData.Add(new DadosArquivos() { Dado = reader.ReadLine() });
                    }
                }
            }
            ViewBag.DadosArquivos = csvData;
            return View();
        }
        */



    }
}
