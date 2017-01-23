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
    public class AlunoController : Controller
    {
        
        public ActionResult Index()
        {

            personalteacherEntities entities = new personalteacherEntities();
            return View(from aluno in entities.alunoes.Take(10) select aluno);
        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            
            personalteacherEntities entities = new personalteacherEntities();
            ViewBag.escola = new SelectList(entities.view_escola, "id", "nome");
            return View();
        }
        [HttpPost]
        public ViewResult Cadastrar([Bind(Include = "escola_id,matricula,nome,dtnascimento")] aluno Aluno)
        {
            personalteacherEntities entities = new personalteacherEntities();
            
            //salvar dados dos alunos
            if (ModelState.IsValid)
            {
                entities.alunoes.Add(Aluno);
                entities.SaveChanges();
                RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: Aluno
        
        // GET: Aluno/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Cadastrar");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Aluno/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            personalteacherEntities entities = new personalteacherEntities();
            try
            {
                // TODO: Add delete logic here
                aluno Aluno=entities.alunoes.Find(id);
                entities.alunoes.Remove(Aluno);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
