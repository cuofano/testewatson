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
    public class analisesController : Controller
    {
        private personalteacherEntities db = new personalteacherEntities();

        // GET: analises
        public ActionResult Index()
        {
            return View(db.view_analises.ToList());
        }

        // GET: analises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_analises view_analises = db.view_analises.SingleOrDefault(a=> a.analiseid==id);
            view_analises.analises_arquivos = db.view_analises_arquivos.Where(b => b.analise_id == id).ToList();

            if (view_analises == null)
            {
                return HttpNotFound();
            }
            return View(view_analises);
        }

        // GET: analises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: analises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,arquivobase_id,analise_id,analiseid,analisedt,alunoid,alunonome,TotalArquivos")] view_analises view_analises)
        {
            if (ModelState.IsValid)
            {
                db.view_analises.Add(view_analises);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_analises);
        }

        // GET: analises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_analises view_analises = db.view_analises.Find(id);
            if (view_analises == null)
            {
                return HttpNotFound();
            }
            return View(view_analises);
        }

        // POST: analises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,arquivobase_id,analise_id,analiseid,analisedt,alunoid,alunonome,TotalArquivos")] view_analises view_analises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_analises).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_analises);
        }

        // GET: analises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_analises view_analises = db.view_analises.Find(id);
            if (view_analises == null)
            {
                return HttpNotFound();
            }
            return View(view_analises);
        }

        // POST: analises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            view_analises view_analises = db.view_analises.Find(id);
            db.view_analises.Remove(view_analises);
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
