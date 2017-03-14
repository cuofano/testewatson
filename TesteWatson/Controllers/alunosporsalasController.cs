using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteWatson.Models;

namespace TesteWatson.Controllers
{
    public class alunosporsalasController : Controller
    {
        private personalteacherEntities db = new personalteacherEntities();

        // GET: alunosporsalas
        public ActionResult Index()
        {
            var alunosporsalas = db.alunosporsalas.Include(a => a.aluno).Include(a => a.sala);
            return View(alunosporsalas.ToList());
        }

        // GET: alunosporsalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alunosporsala alunosporsala = db.alunosporsalas.Find(id);
            if (alunosporsala == null)
            {
                return HttpNotFound();
            }
            return View(alunosporsala);
        }

        // GET: alunosporsalas/Create
        public ActionResult Create()
        {
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula");
            ViewBag.sala_id = new SelectList(db.salas, "id", "ano_escolar");
            return View();
        }

        // POST: alunosporsalas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,aluno_id,sala_id")] alunosporsala alunosporsala)
        {
            if (ModelState.IsValid)
            {
                db.alunosporsalas.Add(alunosporsala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", alunosporsala.aluno_id);
            ViewBag.sala_id = new SelectList(db.salas, "id", "ano_escolar", alunosporsala.sala_id);
            return View(alunosporsala);
        }

        // GET: alunosporsalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alunosporsala alunosporsala = db.alunosporsalas.Find(id);
            if (alunosporsala == null)
            {
                return HttpNotFound();
            }
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", alunosporsala.aluno_id);
            ViewBag.sala_id = new SelectList(db.salas, "id", "ano_escolar", alunosporsala.sala_id);
            return View(alunosporsala);
        }

        // POST: alunosporsalas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,aluno_id,sala_id")] alunosporsala alunosporsala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunosporsala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aluno_id = new SelectList(db.alunoes, "id", "matricula", alunosporsala.aluno_id);
            ViewBag.sala_id = new SelectList(db.salas, "id", "ano_escolar", alunosporsala.sala_id);
            return View(alunosporsala);
        }

        // GET: alunosporsalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alunosporsala alunosporsala = db.alunosporsalas.Find(id);
            if (alunosporsala == null)
            {
                return HttpNotFound();
            }
            return View(alunosporsala);
        }

        // POST: alunosporsalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            alunosporsala alunosporsala = db.alunosporsalas.Find(id);
            db.alunosporsalas.Remove(alunosporsala);
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
