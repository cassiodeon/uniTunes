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
    public class CarrinhoController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        // GET: Carrinho
        public ActionResult Index(string erro)
        {
            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            var car = (from c in db.Carrinhos.Include(x => x.Items) where c.UsuarioId == usuario.Id select c).FirstOrDefault();

            if (car == null)
            {
                car = new Carrinho();
                car.UsuarioId = usuario.Id;
                db.Carrinhos.Add(car);
                db.SaveChanges();
            }
            var items = from ic in db.CarrinhoItems.Include(x => x.Midia) where ic.CarrinhoId == car.Id select ic;
            
            if (erro != null)
            {
                ModelState.AddModelError("", erro);
            }

            ViewBag.SubTotal = car.SubTotal;
            ViewBag.CarrinhoId = car.Id;


            return View(items.ToList());
        }

        public ActionResult Add(int? id)
        {
            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var carrinho = db.Carrinhos.Where(x => x.UsuarioId == usuario.Id).FirstOrDefault();

            if (carrinho == null)
            {
                carrinho = new Carrinho() { UsuarioId = usuario.Id };
                db.Carrinhos.Add(carrinho);
            }
            var itemCarrinho = carrinho.Items.Where(x => x.MidiaId == id).FirstOrDefault();

            if (itemCarrinho != null)
            {
                carrinho.Items.Remove(itemCarrinho);
                db.SaveChanges();
            }

            itemCarrinho = new CarrinhoItem() { MidiaId = (int)id };

            carrinho.Items.Add(itemCarrinho);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Comprar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var car = db.Carrinhos.Where(x => x.Id == id).Include(x => x.Items).Include(y => y.Usuario).FirstOrDefault();
           
            var vendaId = 0;
            if (car != null) {
                vendaId = car.EfetuarVenda();
            }
            if (vendaId > 0)
            {
                db.Carrinhos.Remove(car);
                db.SaveChanges();
                return RedirectToAction("Details", "Vendas", new { id = vendaId });
            }

            return RedirectToAction("Index", "Carrinho", new { erro = "Você não possui créditos (saldo)." });
        }

        public ActionResult Esvaziar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var car = db.Carrinhos.Where(x => x.Id == id).Include(x => x.Items).FirstOrDefault();
            
            if (car != null)
            {
                db.Carrinhos.Remove(car);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}