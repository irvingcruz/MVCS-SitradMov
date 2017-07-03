using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BEUsuario
    {
        [Required(ErrorMessage = "Favor de ingresar su USUARIO.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Favor de ingresar su PASSWORD.")]
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Datos { get; set; }
        public string Alerta { get; set; }
        public bool Recordarme { get; set; }
    }
}
