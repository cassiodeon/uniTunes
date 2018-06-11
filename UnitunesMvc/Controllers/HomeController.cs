using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitunesMvc.Core.Database.Entities;

namespace UnitunesMvc.Controllers
{
    
    public class HomeController : Controller
    {
        private UnitunesEntities db = new UnitunesEntities();
       
        public ActionResult Index()
        {   
            //se o banco estiver vazio por mudanca nos models alimenta ele 
            if (db.Usuarios.Count() == 0)
            {
                db.feed(); 
            }
      
            if (!User.Identity.IsAuthenticated )
            {
                return RedirectToAction("Index","Login");
            }
            return View();
        }

        [HttpGet]
        public FileResult GetImage(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var intId = Convert.ToInt32(id);
                var item = db.Binarios.Where(file => file.Id == intId).FirstOrDefault();
                var ms = new System.IO.MemoryStream(item.Bytes);
                FileStreamResult result = new FileStreamResult(ms, "image/png");
                result.FileDownloadName = item.Id.ToString(); // or item.Id or something (3)
                return result;
            }

            return new FileStreamResult(new System.IO.MemoryStream(), "image/png");
        }

        [HttpGet]
        public FileResult GetFile(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var intId = Convert.ToInt32(id);
                var item = db.Binarios.Where(file => file.Id == intId).FirstOrDefault();

                var contentType = Midia.ContentType(db.Midias.Find(intId).Tipo);
                
                var ms = new System.IO.MemoryStream(item.Bytes);
                FileStreamResult result = new FileStreamResult(ms, contentType);
                result.FileDownloadName = item.Id.ToString(); // or item.Id or something (3)
                return result;
            }

            return null;
        }
    }

    
}