using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class ValidationException: Exception
   {
      // creo el constructor
      public ValidationException(string message) : base(message)
      {
      }
   }
}
