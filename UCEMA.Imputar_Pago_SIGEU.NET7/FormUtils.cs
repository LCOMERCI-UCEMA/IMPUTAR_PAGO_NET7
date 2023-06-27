using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   public class FormUtils
   {
      [HasLoading("picLoading")]
      public static async Task<dynamic> RunAsync(Func<dynamic> action)
      {
         //var attribute = typeof(FormUtils)?
         //    .GetMethod(nameof(RunAsync))?
         //    .GetCustomAttribute<HasLoadingAttribute>();
         
         //attribute?.OnEntry(); // Llamada a OnEntry antes de ejecutar la acción

         var result = await Task.Run(action);

         //attribute?.OnExit(); // Llamada a OnExit después de ejecutar la acción

         return result;
      }
   }
}
