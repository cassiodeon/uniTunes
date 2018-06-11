using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public class Conta
    {
        
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Saldo")]
        public double Saldo { get; set; }
        
        public void Transferir(double valor, Conta contaDestinataria)
        {
            if (this.Id == contaDestinataria.Id)
            {
                return;
            }
            contaDestinataria.Saldo += valor;
            this.Saldo -= valor;
        }
    }
}
