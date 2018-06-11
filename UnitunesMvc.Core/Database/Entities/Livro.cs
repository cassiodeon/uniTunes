using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public class Livro : Midia
    {
        [Required]
        [Display(Name = "Páginas")]
        public long NumeroPaginas { get; set; }

        public Livro()
        {
            base.Tipo = TipoMidia.Livro;
        }
    }
}
