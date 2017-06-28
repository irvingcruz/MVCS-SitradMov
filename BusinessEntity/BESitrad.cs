using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BESitrad
    {
        public int Nro { get; set; }
        [Required(ErrorMessage = "Favor de ingresar el numero de tramite.")]
        public string IDTramite { get; set; }
        public string Documento { get; set; }
        public string EnviaArea { get; set; }
        public string EnviaFecha { get; set; }
        public string RecibeArea { get; set; }
        public string RecibeFecha { get; set; }
        public string Estado { get; set; }
        public int Dias { get; set; }
        public string Observaciones { get; set; }
    }
}
