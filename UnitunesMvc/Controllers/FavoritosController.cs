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
using UnitunesMvc.Helpers;


namespace UnitunesMvc.Controllers
{
    public class FavoritosController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        // GET: Favoritos
        public ActionResult Index(int? tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoMidia = (TipoMidia)tipo;
            ViewBag.Title = "Favoritos - " + EnumHelper.GetDescription(tipoMidia);

            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            ViewBag.Categorias = new CategoriaViewModel().DeterminarCategoriasViewBag(tipoMidia);
            var favoritosId = from uf in db.Favoritos where uf.UsuarioId == usuario.Id && uf.Midia.Tipo == tipoMidia select uf.MidiaId;
            var favoritos = from l in db.Midias
                            where favoritosId.Contains(l.Id)
                         select l;

            return View(favoritos.ToList());
        }

        public ActionResult Add(int? id)
        {
            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var favorito = db.Favoritos.Where(x => x.MidiaId == id && x.UsuarioId == usuario.Id).FirstOrDefault();
            
            if (favorito != null)
            {
                db.Favoritos.Remove(favorito);
            }

            favorito = new UsuarioFavoritos() { UsuarioId = usuario.Id, MidiaId = (int)id };

            db.Favoritos.Add(favorito);
            db.SaveChanges();

            var tipo = db.Midias.Find(id).Tipo;
            return RedirectToAction("Index", new { tipo = (int)tipo });
        }

        public ActionResult Delete(int? id)
        {
            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var favorito = db.Favoritos.Where(x => x.MidiaId == id && x.UsuarioId == usuario.Id).FirstOrDefault();
            var tipo = db.Midias.Find(id).Tipo; 
            if (favorito != null)
            {   
                db.Favoritos.Remove(favorito);
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { tipo = (int)tipo });
        }
    }
}