using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitunesMvc.Core.Database.Entities
{
    public enum TipoStreaming
    {
        [Display(Name = "Música")]
        Musica,
        [Display(Name = "Video")]
        Video,
        [Display(Name = "Podcast")]
        Podcast
    }

    public class Streaming : Midia 
    {
        [Required]
        [Display(Name = "Duração")]
        public long Duracao { get; set; }

        [Required]
        public TipoStreaming TipoStreaming { get; set; }
   
    }
}
