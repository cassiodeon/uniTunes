using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public enum TipoUsuario
    {
        [Display(Name = "Acadêmico")]
        Academico,
        [Display(Name = "Autor")]
        Autor,
        [Display(Name = "Administrador")]
        Administrador
    }
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Primeiro Nome")]
        [StringLength(20, MinimumLength = 3)]
        public String PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Último Nome")]
        public String UltimoNome { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "E-mail precisa ser válido")]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public TipoUsuario Tipo { get; set; }

        [ForeignKey("Conta")]
        [Display(Name = "Conta")]
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }

        public virtual ICollection<UsuarioFavoritos> Favoritos { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [StringLength(30, ErrorMessage = "No mínimo 6 e no máximo 30 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmação Senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [StringLength(30, ErrorMessage = "No mínimo 6 e no máximo 30 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }

        [NotMapped]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto
        {
            get{ return UltimoNome + ", " + PrimeiroNome ;}
        }

        public Usuario()
        {
            this.Favoritos = new List<UsuarioFavoritos>();
        }
    }
}
