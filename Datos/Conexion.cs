using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public class Conexion
   {
      //DESA
      //public static string strConexion = @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=pluton.ucema.edu.ar)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=pdbpluton.ucema.edu.ar)));User Id=;Password=;";
      //PRODUCCION
      private const string DATASOURCE_NEPTUNO = @"(
         DESCRIPTION=(
            ADDRESS=
               (PROTOCOL=TCP)
               (HOST=neptuno.ucema.edu.ar)
               (PORT=1521)
            )(
            CONNECT_DATA=
               (SERVICE_NAME=cema.ucema.edu.ar)
         )
      )";

      // A Oracle no le gusta que haya espacios en blanco antes de los parametros de conexion, por eso lo meto en una líneae.
      private const string strConexion = $"Data Source={DATASOURCE_NEPTUNO};User Id=UCEMADEV;Password=ucema49585!;";

      public static string StrConexion()
      {
         return strConexion;
      }
   }

}