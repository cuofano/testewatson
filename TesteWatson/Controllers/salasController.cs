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
    public class salasController : Controller
    {
        private personalteacherEntities db = new personalteacherEntities();

        // GET: salas
        public ActionResult Index()
        {
            var salas = db.salas.Include(s => s.escola);
            return View(salas.ToList());
        }

        // GET: salas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: salas/Create
        public ActionResult Create()
        {
            ViewBag.escola_id = new SelectList(db.escolas, "id", "nome");
            ViewBag.aluno = new SelectList( db.alunoes,"id","matricula","nome");
            return View(db.alunoes.ToList());

            
        }

        // POST: salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,escola_id,ano_escolar,turma,nome,ano_corrente,ativo")] sala sala)
        {
            if (ModelState.IsValid)
            {
                db.salas.Add(sala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.escola_id = new SelectList(db.escolas, "id", "nome", sala.escola_id);
            return View(sala);
        }

        // GET: salas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            ViewBag.escola_id = new SelectList(db.escolas, "id", "nome", sala.escola_id);
            return View(sala);
        }

        // POST: salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,escola_id,ano_escolar,turma,nome,ano_corrente,ativo")] sala sala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.escola_id = new SelectList(db.escolas, "id", "nome", sala.escola_id);
            return View(sala);
        }

        // GET: salas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sala sala = db.salas.Find(id);
            db.salas.Remove(sala);
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
