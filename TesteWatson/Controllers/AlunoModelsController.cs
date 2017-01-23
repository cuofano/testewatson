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
    public class AlunoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AlunoModels
        public ActionResult Index()
        {
            return View(db.AlunoModels.ToList());
        }

        // GET: AlunoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModels alunoModels = db.AlunoModels.Find(id);
            if (alunoModels == null)
            {
                return HttpNotFound();
            }
            return View(alunoModels);
        }

        // GET: AlunoModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlunoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,escola_id,matricula,nome,dtnascimento")] AlunoModels alunoModels)
        {
            if (ModelState.IsValid)
            {
                db.AlunoModels.Add(alunoModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alunoModels);
        }

        // GET: AlunoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModels alunoModels = db.AlunoModels.Find(id);
            if (alunoModels == null)
            {
                return HttpNotFound();
            }
            return View(alunoModels);
        }

        // POST: AlunoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,escola_id,matricula,nome,dtnascimento")] AlunoModels alunoModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunoModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alunoModels);
        }

        // GET: AlunoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModels alunoModels = db.AlunoModels.Find(id);
            if (alunoModels == null)
            {
                return HttpNotFound();
            }
            return View(alunoModels);
        }

        // POST: AlunoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunoModels alunoModels = db.AlunoModels.Find(id);
            db.AlunoModels.Remove(alunoModels);
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
