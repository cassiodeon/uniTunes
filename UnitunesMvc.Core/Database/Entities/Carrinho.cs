using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public class Carrinho
    {
        private UnitunesEntities db = new UnitunesEntities();
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<CarrinhoItem> Items { get; set; }

        [NotMapped]
        [Display(Name = "Sub-Total")]
        public double SubTotal
        {
            get
            {
                double subtotal = 0;
                foreach(var item in this.Items)
                {
                    subtotal += (double)db.Midias.Find(item.MidiaId).Preco;
                }
                return subtotal;
            }
        }

        public Carrinho()
        {
            this.Items = new List<CarrinhoItem>();
        }

        public int EfetuarVenda() {
            var venda = new Venda();
            venda.UsuarioId = this.UsuarioId;
            double total = 0;

            var contaComprador = db.Contas.Find(this.Usuario.ContaId);
            if (this.SubTotal > contaComprador.Saldo) return 0;
            foreach (var item in this.Items)
            {
                var creditoAutor = (double)item.Midia.Preco * 0.9;
                var creditoAdmin = (double)item.Midia.Preco * 0.1;
                total += (double)item.Midia.Preco;

                var contaAutor = db.Contas.Find(item.Midia.AutorId);
                var contaAdministrador = db.Contas.Find(UnitunesEntities.USUARIO_ADMIN_ID);

                contaComprador.Transferir(creditoAutor, contaAutor);
                contaComprador.Transferir(creditoAdmin, contaAdministrador);

                venda.Items.Add(new VendaItem
                {
                    MidiaId = item.MidiaId,
                    Valor = (double)item.Midia.Preco
                });
            }
            venda.Total = total;
            if (venda.Items.Count  == 0) return 0;
            
            db.Vendas.Add(venda);
            db.SaveChanges();
            return venda.Id;
        }
    }
}
