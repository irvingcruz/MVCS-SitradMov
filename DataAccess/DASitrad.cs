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
                        oDocs.IDDocumento = Convert.ToInt32(dr["FK_Id_Documento"]);
                        oDocs.Documento = dr["DOCUMENTO"].ToString();
                        oDocs.FechaEmision = dr["Fecha_Emision"].ToString();
                        oDocs.Remitente = dr["REMITENTE"].ToString();
                        oDocs.Destinatario = dr["DESTINATARIO"].ToString();
                        oDocs.IDTramite = dr["IDTramite"].ToString();  
                        listado.Add(oDocs);
                    }                   
                }

                foreach (BESitrad doc in listado)
                {
                    if (oCon.State == ConnectionState.Open) oCon.Close();

                    List<BESitradTraza> listadoTraza = new List<BESitradTraza>();
                    cmd = new SqlCommand("sp_get_derivaciones_new", oCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PK_ID_TRAMITE", doc.IDTramite);
                    cmd.Parameters.AddWithValue("@NOMENCLATURA", doc.Documento);
                    cmd.Connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            BESitradTraza oTraza = new BESitradTraza();
                            oTraza.Nro = Convert.ToInt32(dr["number"]);
                            oTraza.Documento = dr["DOCUMENTO"].ToString();
                            oTraza.EnviaArea = dr["REMITENTE"].ToString();
                            oTraza.EnviaFecha = dr["Fecha_Modificacion"].ToString();
                            oTraza.RecibeArea = dr["DESTINATARIO"].ToString();
                            oTraza.RecibeFecha = dr["FECHA_ATENCION"].ToString();
                            oTraza.Estado = dr["Descripcion"].ToString();
                            oTraza.Dias = Convert.ToInt32(dr["DIAS_QUE_LO_TIENE"]);
                            oTraza.Observaciones = dr["Observaciones"].ToString();
                            listadoTraza.Add(oTraza);
                        }
                    }
                    doc.oTraza = listadoTraza;
                }
            }
            catch (Exception e) { throw e; }
            finally { oCon.Close(); }
            return listado;
        }

        public List<BESitrad> fnTrazabilidadSITRAD(int IDTramite,string Documento)
        {            
            List<BESitrad> listado = new List<BESitrad>();
            //try
            //{
            //    SqlCommand cmd = new SqlCommand("sp_get_derivaciones_new", oCon);               
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@PK_ID_TRAMITE", IDTramite);
            //    cmd.Parameters.AddWithValue("@NOMENCLATURA", Documento);
            //    oCon.Open();
            //    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            //    {
            //        while (dr.Read())
            //        {
            //            BESitrad oTraza = new BESitrad();
            //            oTraza.Nro = Convert.ToInt32(dr["number"]);
            //            oTraza.Documento = dr["DOCUMENTO"].ToString();
            //            oTraza.EnviaArea = dr["REMITENTE"].ToString();
            //            oTraza.EnviaFecha = dr["Fecha_Modificacion"].ToString();
            //            oTraza.RecibeArea = dr["DESTINATARIO"].ToString();
            //            oTraza.RecibeFecha = dr["FECHA_ATENCION"].ToString();
            //            oTraza.Estado = dr["Descripcion"].ToString();
            //            oTraza.Dias = Convert.ToInt32(dr["DIAS_QUE_LO_TIENE"]);
            //            oTraza.Observaciones = dr["Observaciones"].ToString();
            //            listado.Add(oTraza);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    oCon.Close();
            //}
            return listado;
        }
    }
}
