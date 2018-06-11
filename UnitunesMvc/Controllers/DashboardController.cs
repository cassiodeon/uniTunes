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
    public class DashboardController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            var usuario = new LoginViewModel().Buscar(User.Identity.Name);
            if (usuario != null && usuario.Tipo == TipoUsuario.Administrador)
            {
                return View(db.Vendas.ToList());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}