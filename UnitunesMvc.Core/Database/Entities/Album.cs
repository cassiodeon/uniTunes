using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public enum Ordem
    {
        [Display(Name = "Todos")]
        Todos,
        [Display(Name = "Recente")]
        Recente,
        [Display(Name = "Novo")]
        Novo
    }

public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string Lancamento { get; set; }
        public Categoria Genero { get; set; }
        public Autor Autor { get; set; }
        
        public List<Streaming> Musicas { get; set; }
    } 
}