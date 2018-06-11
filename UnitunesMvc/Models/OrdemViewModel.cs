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
    public class OrdemViewModel
    {
        private UnitunesEntities db = new UnitunesEntities();
        public IEnumerable<SelectListItem> DeterminarOrdemViewBag(Ordem ordem)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            var todosItem = new SelectListItem { Text = "Todos", Value = "Todos" };
            var recenteItem = new SelectListItem { Text = "Recente", Value = "Recente" };
            var novoItem = new SelectListItem { Text = "Novo", Value = "Novo" };

            itens.Add(todosItem);
            itens.Add(recenteItem);
            itens.Add(novoItem);

            IEnumerable<SelectListItem> listaEnumerada = (IEnumerable<SelectListItem>) itens;
            
            return itens;
        }
    }
}