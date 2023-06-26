using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public static class Script
   {
      public static DataTable GetVerificacionImputacion()
      {
         DataTable dt = new DataTable();

         using (OracleConnection con = new OracleConnection(Conexion.StrConexion()))
         {
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
            {
               cmd.CommandText = @"
                  SELECT C_FORMA_COBRO, COUNT(*)
                  FROM COBROS C,
                       COBROS_FORMA CF
                  WHERE TRUNC(C.F_ALTA) = TRUNC(SYSDATE)
                    AND C.C_USUARIOALT = 'CEMAP'
                    AND C.N_ID_COBRO = CF.N_ID_COBRO
                  GROUP BY C_FORMA_COBRO";
               cmd.CommandType = CommandType.Text;

               using (OracleDataAdapter adp = new OracleDataAdapter(cmd))
               {
                  adp.Fill(dt);
               }
            }
            con.Close();
         }
         return dt;
      }

      public static DataTable traerEntidades(string tabla, string campos = "*", string orden = "")
      {
         DataTable dt = new DataTable();

         using (OracleConnection con = new OracleConnection(Conexion.StrConexion()))
         {
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
            {
               cmd.CommandText =
                   @"SELECT " + campos +
                    " FROM " + tabla + (orden.Length > 0
                       ? (" ORDER BY " + orden)
                       : "");
               cmd.CommandType = CommandType.Text;

               using (OracleDataAdapter adp = new OracleDataAdapter(cmd))
               {
                  adp.Fill(dt);
               }
            }

            con.Close();
         }

         return dt;
      }
   }
}
