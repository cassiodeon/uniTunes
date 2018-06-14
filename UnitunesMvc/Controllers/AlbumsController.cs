using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnitunesMvc.Core.Database.Entities;
using UnitunesMvc.Models;

namespace UnitunesMvc.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();

        // GET: Albuns
        public ActionResult Index(string pesquisa, string ordem)
        {
            Ordem current_order = Ordem.Todos;

            if (ordem == "Recente")
                current_order = Ordem.Recente;
            else if (ordem == "Novo")
                current_order = Ordem.Novo;
               
            ViewBag.Ordem = new OrdemViewModel().DeterminarOrdemViewBag(current_order);
            var albuns = from l in db.Albums select l;          

            if (!String.IsNullOrEmpty(pesquisa))
            {
                albuns = albuns.Where(s => s.Titulo.Contains(pesquisa));
            }
            if (current_order != Ordem.Todos)
            {
                if (current_order == Ordem.Novo)
                    albuns = albuns.Where(s => (DateTime.Now - Convert.ToDateTime(s.Lancamento)).TotalDays <= 60);
                else if (current_order == Ordem.Recente)
                    albuns = albuns.Where(s => (DateTime.Now - Convert.ToDateTime(s.Lancamento)).TotalDays > 60);
            }
            ViewBag.UsuarioLogado = new LoginViewModel().Buscar(User.Identity.Name);
            return View(albuns.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Preco,Data")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.Lancamento = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Preco,Lancamento")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
