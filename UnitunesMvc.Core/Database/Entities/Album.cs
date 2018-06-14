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

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "Lançamento")]
        public string Lancamento { get; set; }

        //[ForeignKey("Genero")]
        [Display(Name = "Gênero")]
        public Categoria Genero_Id { get; set; }

        [Display(Name = "Gênero")]
        public Categoria Genero { get; set; }

        [ForeignKey("Autor")]
        [Display(Name = "Autor")]
        public int Autor_Id { get; set; }

        [Display(Name = "Autor")]
        public virtual Autor Autor { get; set; }
        
        public virtual List<Streaming> Musicas { get; set; }
    } 
}