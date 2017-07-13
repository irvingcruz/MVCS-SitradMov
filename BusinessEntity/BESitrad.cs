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
        public int IDDocumento { get; set; }
        public string Documento { get; set; }
        public string EnviaArea { get; set; }
        public string EnviaFecha { get; set; }
        public string EnviaUser { get; set; }
        public string RecibeArea { get; set; }
        public string RecibeFecha { get; set; }
        public string RecibeUser { get; set; }
        public string Estado { get; set; }
        public int Dias { get; set; }
        public string Observaciones { get; set; }
        //public string FechaEmision { get; set; }
        //public string Remitente { get; set; }
        //public string Destinatario { get; set; }
        //public List<BESitradTraza> oTraza { get; set; }
    }

    public class BESitradTraza
    {
        public int Nro { get; set; }
        public string Documento { get; set; }
        public string EnviaArea { get; set; }
        public string EnviaFecha { get; set; }
        public string RecibeArea { get; set; }
        public string RecibeFecha { get; set; }
        public string Estado { get; set; }
        //public int Dias { get; set; }
        //public string Observaciones { get; set; }
    }
}
