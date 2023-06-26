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
      private const string strConexion = @"
      Data Source=(
        DESCRIPTION=(
          ADDRESS=
            (PROTOCOL=TCP)
            (HOST=neptuno.ucema.edu.ar)
            (PORT=1521)
        )(
          CONNECT_DATA=
            (SERVICE_NAME=cema.ucema.edu.ar)
        )
      );
      User Id=UCEMADEV;
      Password=ucema49585!;";

      public static string StrConexion()
      {
         return strConexion;
      }
   }

}