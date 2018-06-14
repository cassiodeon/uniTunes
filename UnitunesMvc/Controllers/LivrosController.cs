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
    public class LivrosController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        private TipoMidia m_tipoMidia = TipoMidia.Livro;
  
        // GET: Livros
        public ActionResult Index(string pesquisa, string categoria)
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            ViewBag.UsuarioLogado = new LoginViewModel().Buscar(User.Identity.Name);

            var livros =  from l in db.Livros.Include(x => x.Categoria) select l;

            if (!String.IsNullOrEmpty(pesquisa))
            {
                livros = livros.Where(s => s.Descricao.Contains(pesquisa) || s.Nome.Contains(pesquisa));
            }
            if (!String.IsNullOrEmpty(categoria))
            {
                var categoriaInt = Convert.ToInt32(categoria);
                livros = livros.Where(s => s.CategoriaId == categoriaInt);
            }

            return View(livros.ToList());
        }


        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumeroPaginas,Nome,Descricao,Preco,CategoriaId")] Livro livro, HttpPostedFileBase imagem, HttpPostedFileBase conteudo)
        {
            livro.AutorId = new LoginViewModel().Buscar(User.Identity.Name).Id ;
            if (imagem != null && imagem.ContentLength > 0)
            {
                livro.Imagem.Bytes = new byte[imagem.ContentLength];
                imagem.InputStream.Read(livro.Imagem.Bytes, 0, imagem.ContentLength);
            }

            if (conteudo != null && conteudo.ContentLength > 0)
            {
                livro.Conteudo.Bytes = new byte[conteudo.ContentLength];
                conteudo.InputStream.Read(livro.Conteudo.Bytes, 0, conteudo.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livro);
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }


        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumeroPaginas,Nome,Descricao,Preco,Imagem,Conteudo,CategoriaId")] Livro livro)
        {
            livro.Tipo = m_tipoMidia;
            livro.AutorId = db.Livros.Where(x => x.Id == livro.Id).AsNoTracking().FirstOrDefault().AutorId;
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
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
