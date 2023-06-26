using System.Data;

namespace Logica
{
   public class Script
   {
      public static DataTable TraerDatos()
      {
         return Datos.Script.TraerDatos();

         //DataTable dt = new DataTable();

         //dt.Columns.Add("Nombre");
         //dt.Columns.Add("Apellido");
         //dt.Columns.Add("Edad");

         //dt.Rows.Add("Juan", "Perez", 25);
         //dt.Rows.Add("Pedro", "Gomez", 30);
         //dt.Rows.Add("Maria", "Lopez", 35);

         //return dt;
      }
   }
}