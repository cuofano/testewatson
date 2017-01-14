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

        public JsonResult ReadTextFile(string fileName)
        {
            string retString = string.Empty;
            string path = Path.Combine(Server.MapPath("~/uploads"), fileName);
            if (System.IO.File.Exists(path))
            {
                if (Path.GetExtension(path) == "doc" || Path.GetExtension(path) == ".docx")
                {
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    object miss = System.Reflection.Missing.Value;
                    object readOnly = true;
                    object wordPath = path;
                    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(
                        ref wordPath,
                        ref miss,
                        ref readOnly,
                        ref miss, ref miss, ref miss,
                        ref miss, ref miss, ref miss,
                        ref miss, ref miss, ref miss,
                        ref miss, ref miss, ref miss, ref miss);
                    for (int i = 0; i < docs.Paragraphs.Count; i++)
                    {
                        retString += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
                    }
                }
                else if (Path.GetExtension(path) == "txt")
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        retString = sr.ReadToEnd();
                    }
                }
            }
            return Json(retString, JsonRequestBehavior.AllowGet);
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
