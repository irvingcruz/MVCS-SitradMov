using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DASitrad
    {
        SqlConnection oCon;
        public DASitrad(SqlConnection _oCon)
        {
            this.oCon = _oCon;
        }

        public bool fnAutenticacion(BEUsuario oUsuario)
        {
            bool rpta = false;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AutenticacionSitradMovil", oCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", oUsuario.UserName);
                cmd.Parameters.AddWithValue("@Password", oUsuario.Password);
                oCon.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        oUsuario.Correo = dr["Correo"].ToString();
                        oUsuario.Nombres = dr["Nombres_Personal"].ToString();
                        rpta = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            finally { oCon.Close(); }
            return rpta;
        }

        public List<BESitrad> fn_SelectDocumentosTramite(string vNumeroHT)
        {
            string[] valores = vNumeroHT.Split(Convert.ToChar("-"));
            int IDTramite = Convert.ToInt32(valores[0]);
            int Anio = Convert.ToInt32(valores[1]);
            List<BESitrad> listado = new List<BESitrad>();
            try
            {
                SqlCommand cmd = new SqlCommand("spSM_SelectDocumentosTramite", oCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDTramite", IDTramite);
                cmd.Parameters.AddWithValue("@Anio", Anio);
                oCon.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        BESitrad oDocs = new BESitrad();
                        oDocs.Nro = Convert.ToInt32(dr["number"]);
                        oDocs.Documento = dr["DOCUMENTO"].ToString();
                        oDocs.EnviaArea = dr["REMITENTE"].ToString();
                        oDocs.EnviaUser = dr["USER_R"].ToString();
                        oDocs.EnviaFecha = dr["Fecha_Modificacion"].ToString();
                        oDocs.RecibeArea = dr["DESTINATARIO"].ToString();
                        oDocs.RecibeUser = dr["USER_D"].ToString();
                        oDocs.RecibeFecha = dr["Fecha_Atencion"].ToString();
                        oDocs.Estado = dr["Descripcion"].ToString();
                        oDocs.Dias = Convert.ToInt32(dr["DIAS_QUE_LO_TIENE"]);
                        oDocs.Observaciones = dr["Observaciones"].ToString();
                        listado.Add(oDocs);
                    }
                }
            }
            catch (Exception e) { throw e; }
            finally { oCon.Close(); }
            return listado;
        }

        //public List<BESitrad> fnTrazabilidadSITRAD(string vNumeroHT)
        //{
        //    string[] valores = vNumeroHT.Split(Convert.ToChar("-"));
        //    int IDTramite = Convert.ToInt32(valores[0]);
        //    int Anio = Convert.ToInt32(valores[1]); ;
        //    int FK_Id_Tramite = -1;
        //    List<BESitrad> listado = new List<BESitrad>();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("spBuscarTramite", oCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@idtramite", IDTramite);
        //        cmd.Parameters.AddWithValue("@anno", Anio);
        //        oCon.Open();
        //        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            while (dr.Read())
        //            {
        //                FK_Id_Tramite = Convert.ToInt32(dr["FK_Id_Tramite"]);
        //            }
        //        }
        //        cmd = new SqlCommand("sp_get_derivaciones_new", oCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@PK_ID_TRAMITE", FK_Id_Tramite);
        //        oCon.Open();
        //        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            while (dr.Read())
        //            {
        //                BESitrad oTraza = new BESitrad();
        //                oTraza.Nro = Convert.ToInt32(dr["number"]);
        //                oTraza.Documento = dr["DOCUMENTO"].ToString();
        //                oTraza.EnviaArea = dr["REMITENTE"].ToString();
        //                oTraza.EnviaFecha = dr["Fecha_Modificacion"].ToString();
        //                oTraza.RecibeArea = dr["DESTINATARIO"].ToString();
        //                oTraza.RecibeFecha = dr["FECHA_ATENCION"].ToString();
        //                oTraza.Estado = dr["Descripcion"].ToString();
        //                oTraza.Dias = Convert.ToInt32(dr["DIAS_QUE_LO_TIENE"]);
        //                oTraza.Observaciones = dr["Observaciones"].ToString();
        //                listado.Add(oTraza);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        oCon.Close();
        //    }
        //    return listado;
        //}
    }
}
