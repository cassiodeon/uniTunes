﻿using System;
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
    public class MusicasController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        private TipoMidia m_tipoMidia = TipoMidia.Musica;
        private TipoStreaming m_tipoStreaming = TipoStreaming.Musica;

        // GET: Musicas
        public ActionResult Index(string pesquisa, string categoria)
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            ViewBag.UsuarioLogado = new LoginViewModel().Buscar(User.Identity.Name);

            var musicas = db.Streamings.Where(
                musica => musica.TipoStreaming == m_tipoStreaming).Include(x => x.Categoria);

            if (!String.IsNullOrEmpty(pesquisa))
            {
                musicas = musicas.Where(s => s.Descricao.Contains(pesquisa) || s.Nome.Contains(pesquisa));
            }

            if (!String.IsNullOrEmpty(categoria))
            {
                var categoriaInt = Convert.ToInt32(categoria);
                musicas = musicas.Where(s => s.CategoriaId == categoriaInt);
            }
            return View(musicas.ToList());
        }

        // GET: Musicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Streaming streaming = db.Streamings.Find(id);
            if (streaming == null)
            {
                return HttpNotFound();
            }
            return View(streaming);
        }

        // GET: Musicas/Create
        public ActionResult Create()
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            return View();
        }

        // POST: Musicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Duracao,Tipo,Nome,Descricao,Preco,CategoriaId")] Streaming streaming, HttpPostedFileBase imagem, HttpPostedFileBase conteudo)
        {
            streaming.Tipo = m_tipoMidia;
            streaming.TipoStreaming = m_tipoStreaming;
            streaming.AutorId = new LoginViewModel().Buscar(User.Identity.Name).Id;
            if (imagem != null && imagem.ContentLength > 0)
            {
                streaming.Imagem = new ArquivoBinario();
                streaming.Imagem.Bytes = new byte[imagem.ContentLength];
                imagem.InputStream.Read(streaming.Imagem.Bytes, 0, imagem.ContentLength);
            }

            if (conteudo != null && conteudo.ContentLength > 0)
            {
                streaming.Conteudo = new ArquivoBinario();
                streaming.Conteudo.Bytes = new byte[conteudo.ContentLength];
                conteudo.InputStream.Read(streaming.Conteudo.Bytes, 0, conteudo.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Streamings.Add(streaming);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(streaming);
        }

        // GET: Musicas/Edit/id
        public ActionResult Edit(int? id)
        {
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Streaming streaming = db.Streamings.Find(id);
            if (streaming == null)
            {
                return HttpNotFound();
            }
            return View(streaming);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Duracao,Tipo,Nome,Descricao,Preco,Imagem,Conteudo,CategoriaId")] Streaming streaming)
        {
            streaming.Tipo = m_tipoMidia;
            streaming.TipoStreaming = m_tipoStreaming;
            streaming.AutorId = db.Streamings.Where(x => x.Id == streaming.Id).AsNoTracking().FirstOrDefault().AutorId;
            if (ModelState.IsValid)
            {
                db.Entry(streaming).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(m_tipoMidia);
            return View(streaming);
        }

        // GET: Musicas/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Streaming streaming = db.Streamings.Find(id);
            if (streaming == null)
            {
                return HttpNotFound();
            }
            return View(streaming);
        }

        // POST: Musicas/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Streaming streaming = db.Streamings.Find(id);
            db.Streamings.Remove(streaming);
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
