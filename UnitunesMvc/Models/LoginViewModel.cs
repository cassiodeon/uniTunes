using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UnitunesMvc.Core.Database.Entities;

namespace UnitunesMvc.Models
{
    public class LoginViewModel
    {
        private UnitunesEntities db = new UnitunesEntities();

        [Required]
        [Display(Name = "E-Mail")]
        public string Email{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
   
        public Usuario Buscar(string email)
        {
            return db.Usuarios.Where(a => a.Email.Equals(email)).FirstOrDefault();
        }    
    }
}