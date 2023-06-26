using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class Stream
   {
      public Stream(string STDIN, string STDOUT, string STDERR)
      {
         this.STDIN = STDIN;
         this.STDOUT = STDOUT;
         this.STDERR = STDERR;
      }

      public string STDIN { get; set; }
      public string STDOUT { get; set; }
      public string STDERR { get; set; }
   }
}
