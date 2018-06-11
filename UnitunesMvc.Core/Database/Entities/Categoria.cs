using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UnitunesMvc.Core.Database.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public TipoMidia Tipo { get; set; }
    }
}