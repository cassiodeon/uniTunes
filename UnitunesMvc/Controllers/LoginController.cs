using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitunesMvc.Models;
using UnitunesMvc.Core.Database.Entities;
using System.Web.Security;

namespace UnitunesMvc.Controllers
{
    public class LoginController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login(string erro)
        {
            if (erro != null)
            {
                ModelState.AddModelError("", erro );
            }

            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Login", new { erro = "Dados inválidos" });
            }

            var v = db.Usuarios.Where(a => a.Email.Equals(login.Email ) && a.Senha.Equals(login.Senha)).FirstOrDefault();
            if (v != null)
            {
                FormsAuthentication.RedirectFromLoginPage(login.Email, true);
                return RedirectToAction("Index", "Home");
            }

    
            return RedirectToAction("Login", "Login", new { erro = "Usuário ou senha inválidos.Tente novamente" });
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}