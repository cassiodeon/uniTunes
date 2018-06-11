using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public class CarrinhoItem
    {
        [Key, Column(Order = 0)]
        public int CarrinhoId { get; set; }
        [Key, Column(Order = 1)]
        public int MidiaId { get; set; }

        public virtual Carrinho Carrinho { get; set; }
        public virtual Midia Midia { get; set; }
    }
}
