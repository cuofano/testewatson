using Newtonsoft.Json;
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
            var model = JsonConvert.DeserializeObject<RetornoApiPerfil>(view_analises.analiseresultado);
            ViewBag.model = GerarArvore(model);
            return View(view_analises);
        }

        public static List<TreeViewModels> GerarArvore(RetornoApiPerfil analise)
        {
            var analisedesm = new List<TreeViewModels>();

            analisedesm.Add(new TreeViewModels
            {
                Name="Personalidade",
                Nodes=GerarArvorePersonalidade(analise)
            });
            analisedesm.Add(new TreeViewModels
            {
                Name = "Necessidades",
                Nodes = GerarArvoreNeeds(analise)
            });
            analisedesm.Add(new TreeViewModels
            {
                Name = "Valores",
                Nodes = GerarArvoreValores(analise)
            });

            return analisedesm;
        }

        private static List<TreeViewModels> GerarArvorePersonalidade(RetornoApiPerfil analise)
        {
            var personalidades = new List<TreeViewModels>();

            foreach (Personality personalidade in analise.personality)
            {
                personalidades.Add(new TreeViewModels
                {
                    Name = personalidade.name,
                    Nota = personalidade.percentile,
                    Nodes = GerarArvoreChield(personalidade)
                });
            }


            return personalidades;
        }
        private static List<TreeViewModels> GerarArvoreChield(Personality personalidade)
        {
            var childs = new List<TreeViewModels>();

            foreach (Child child in personalidade.children)
            {
                childs.Add(new TreeViewModels
                {
                    Name = child.name,
                    Nota = child.percentile
                });
            }


            return childs;
        }
        private static List<TreeViewModels> GerarArvoreNeeds(RetornoApiPerfil analise)
        {
            var needs = new List<TreeViewModels>();

            foreach (Need need in analise.needs)
            {
                needs.Add(new TreeViewModels
                {
                    Name = need.name,
                    Nota = need.percentile
                });
            }


            return needs;
        }

        private static List<TreeViewModels>GerarArvoreValores(RetornoApiPerfil analise)
        {
            var valores = new List<TreeViewModels>();
            
            foreach(Value val in analise.values)
            {
                valores.Add(new TreeViewModels
                {
                    Name = val.name,
                    Nota = val.percentile
                });
            }
                
            
            return valores;
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
