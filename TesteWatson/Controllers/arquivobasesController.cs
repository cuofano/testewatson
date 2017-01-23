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

        // GET: arquivobases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobase arquivobase = db.arquivobases.Find(id);
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
        public ActionResult Create([Bind(Include = "id,aluno_id,nome,arquivo")] arquivobase arquivobase, HttpPostedFileBase file)
        {
            //download do arquivo 

            //transformar em texto

            //transformar em array de byte
            byte[] fileContent = null;

            Stream arquivo = file.InputStream;

            System.IO.FileStream fs = new System.IO.FileStream(arquivo, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(arquivo).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);

            if (ModelState.IsValid)
            {
                db.arquivobases.Add(arquivobase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", arquivobase.aluno_id);
            return View(arquivobase);
        }

        // GET: arquivobases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobase arquivobase = db.arquivobases.Find(id);
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
        public ActionResult Edit([Bind(Include = "id,aluno_id,nome,arquivo")] arquivobase arquivobase)
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
            arquivobase arquivobase = db.arquivobases.Find(id);
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
            arquivobase arquivobase = db.arquivobases.Find(id);
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
