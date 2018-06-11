using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        public virtual ICollection<VendaItem> Items { get; set; }

        [Required]
        [Display(Name = "Total")]
        public Double Total { get; set; }
        public Venda()
        {
            this.Data = DateTime.Now;
            this.Items = new List<VendaItem>();
        }
    }
}
