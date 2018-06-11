using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UnitunesMvc.Core.Database.Entities
{
    public class ArquivoBinario
    {
        [Key]
        public int Id { get; set; }
        public Byte[] Bytes { get; set; }
    }
}
