using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Mvc.BillSales.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es dato requerido.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es dato requerido.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string RequestPath { get; set; }
    }
}
