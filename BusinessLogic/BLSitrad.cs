using BusinessEntity;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BLSitrad
    {
        private SqlConnection oCon;
        public bool fnAutenticacion(BEUsuario oUsuario)
        {
            try
            {
                oCon = BLConexion.SIUBET();
                DASitrad obj = new DASitrad(oCon);
                return obj.fnAutenticacion(oUsuario);
                //oUsuario.Nombres = "Irving";
                //return true;
            }
            catch (Exception e) { throw e; }
        }
        public List<BESitrad> fnTrazabilidadSITRAD(string vNumeroHT)
        {
            try
            {
                oCon = BLConexion.SITRAD();
                DASitrad obj = new DASitrad(oCon);
                List<BESitrad> resultado = obj.fnTrazabilidadSITRAD(vNumeroHT);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
