using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using UnitunesMvc.Core.Database.Entities;
using UnitunesMvc.Models;

namespace UnitunesMvc.Models
{
    public class CategoriaViewModel
    {
        private UnitunesEntities db = new UnitunesEntities();
        public IEnumerable<SelectListItem> DeterminarCategoriasViewBag(TipoMidia tipo)
        {
            var listaCategoriasVideo = db.Categorias.Where(c => c.Tipo == tipo);
            IEnumerable<SelectListItem> listaEnumerada = listaCategoriasVideo.Select(c => new SelectListItem { Text = c.Nome, Value = c.Id.ToString() });
            
            return listaEnumerada;
        }
    }
}