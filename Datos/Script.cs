using System;
using System.Collections.Generic;
using System.Data;
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

      public static DataTable TraerEntidades(string tabla, string campos = "*", string orden = "")
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
      
      public static DataTable TraerDatos()
      {
         DataTable dt = new DataTable();

         using (OracleConnection con = new OracleConnection(Conexion.StrConexion()))
         {
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
            {
               cmd.CommandText = @"
                             SELECT DISTINCT P.D_NOMBRES || ' ' || P.D_APELLIDOS AS NOMBRE,
                                    CT.C_CATEGORIA,
                                    PG.D_DESCRED,
                                    PG.D_DESCRIP,
                                    M.N_ID_MATERIA,
                                    M.D_DESCRIP                         AS M_D_DESCRIP,
                                    CO.D_DESCRIP                        AS CO_D_DESCRIP,
                                    CU.D_DESCRIP                        AS CU_D_DESCRIP,
                                    CU.F_INICIO,
                                    CU.F_FIN,
                                    (CU.F_FIN - CU.F_INICIO)            AS diasReales,
                                    CU.C_TIPO_PERIODO                   AS rotuladoComo,
                                    CASE CU.C_TIPO_PERIODO
                                        WHEN 'Semana' THEN 7
                                        WHEN 'Mes' THEN 30
                                        WHEN '1/2 Trimestre' THEN 45
                                        WHEN 'Bimestre' THEN 60
                                        WHEN 'Trimestre' THEN 90
                                        WHEN 'Cuatrimestre' THEN 120
                                        WHEN 'Semestre' THEN 180
                                        WHEN 'Anual' THEN 365
                                        END                             AS diasPeriodo,
                                    CU.C_USUARIOACT,
                                    CU.C_USUARIOALT
                              FROM CEMAP.PROGRAMAS PG
                                      INNER JOIN
                                  CEMAP.COMISIONES CO ON PG.C_IDENTIFICACION = CO.C_IDENTIFICACION AND
                                                          PG.C_PROGRAMA = CO.C_PROGRAMA AND
                                                          PG.C_ORIENTACION = CO.C_ORIENTACION
                                      INNER JOIN
                                  CEMAP.PLANES_MATERIAS PM ON PG.C_IDENTIFICACION = PM.C_IDENTIFICACION AND
                                                              PG.C_PROGRAMA = PM.C_PROGRAMA AND
                                                              PG.C_ORIENTACION = PM.C_ORIENTACION
                                      INNER JOIN
                                  CEMAP.MATERIAS M ON PM.N_ID_MATERIA = M.N_ID_MATERIA
                                      INNER JOIN
                                  CEMAP.CURSOS CU ON CU.N_ID_MATERIA = M.N_ID_MATERIA
                                      INNER JOIN
                                  CEMAP.CLASES CL ON CU.N_CURSO = CL.N_CURSO AND CU.C_AÑO_LECTIVO = CL.C_AÑO_LECTIVO
                                      INNER JOIN
                                  CEMAP.PERSONAS P ON P.N_ID_PERSONA = CL.N_ID_PERSONA
                                      INNER JOIN
                                  CEMAP.CONTRATOS CT ON CT.N_ID_PERSONA = P.N_ID_PERSONA AND CT.F_HASTA IS NULL
                              WHERE 1 = 1
                                AND (PM.C_IDENTIFICACION || PM.C_PROGRAMA || PM.C_ORIENTACION) = 21500
                                -- AND CU.C_AÑO_LECTIVO >= 2022
                                AND CT.C_CATEGORIA = 'Titular'
                              ORDER BY M.N_ID_MATERIA, CO.D_DESCRIP";
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
