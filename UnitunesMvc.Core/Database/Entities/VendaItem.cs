using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UnitunesMvc.Core.Database.Entities
{
    public class VendaItem
    {
        [Key, Column(Order = 0)]
        public int VendaId { get; set; }
        [Key, Column(Order = 1)]
        public int MidiaId { get; set; }

        public virtual Venda Venda { get; set; }
        public virtual Midia Midia { get; set; }

        public double Valor { get; set; }
    }
}
