﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BLConexion
    {
        public static SqlConnection SITRAD()
        {
            SqlConnection Cno = new SqlConnection(ConfigurationManager.AppSettings["SITRAD"]);
            return Cno;
        }
        //public static SqlConnection USR_SITRAD()
        //{
        //    SqlConnection Cno = new SqlConnection(ConfigurationManager.AppSettings["USR_SITRAD"]);
        //    return Cno;
        //}

        public static SqlConnection SIUBET()
        {
            SqlConnection Cno = new SqlConnection(ConfigurationManager.AppSettings["SIUBET"]);
            return Cno;
        }
    }
}
