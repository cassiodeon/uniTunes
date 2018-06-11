using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public enum TipoMidia
    {
        [Display(Name = "Música")]
        Musica,
        [Display(Name = "Video")]
        Video,
        [Display(Name = "Podcast")]
        Podcast,
        [Display(Name = "Livro")]
        Livro
    }

    public abstract class Midia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Preço")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Preço deve ser de pelo menos 0.01")]
        public Decimal Preco { get; set; }

        [Display(Name = "Imagem")]
        public ArquivoBinario Imagem { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Conteúdo")]
        public ArquivoBinario Conteudo { get; set; }

        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        public TipoMidia Tipo { get; set; }

        public static string ContentType(TipoMidia tipo)
        {
            switch (tipo)
            {
                case TipoMidia.Musica:
                    return "audio/mpeg";
                case TipoMidia.Video:
                    return "video/mpeg";
                case TipoMidia.Podcast:
                    return "audio/mpeg";
                case TipoMidia.Livro:
                    return "application/pdf";
                default:
                    break;
            }

            return String.Empty; 
        }
    }
}
