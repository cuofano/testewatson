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
    public class analisController : Controller
    {
        private personalteacherEntities db = new personalteacherEntities();

        // GET: analis
        public ActionResult Index()
        {
            return View(db.analises.ToList());
        }

        // GET: analis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            analis analis = db.analises.Find(id);
            if (analis == null)
            {
                return HttpNotFound();
            }
            return View(analis);
        }

        // GET: analis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: analis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dtanalise,qtdpalavras,resultado")] analis analis)
        {
            if (ModelState.IsValid)
            {
                db.analises.Add(analis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(analis);
        }

        // GET: analis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            analis analis = db.analises.Find(id);
            if (analis == null)
            {
                return HttpNotFound();
            }
            return View(analis);
        }

        // POST: analis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dtanalise,qtdpalavras,resultado")] analis analis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(analis);
        }

        // GET: analis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            analis analis = db.analises.Find(id);
            if (analis == null)
            {
                return HttpNotFound();
            }
            return View(analis);
        }

        // POST: analis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            analis analis = db.analises.Find(id);
            db.analises.Remove(analis);
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
