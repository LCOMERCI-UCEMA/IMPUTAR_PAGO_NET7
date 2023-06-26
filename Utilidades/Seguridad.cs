using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades
{
   static class Seguridad
   {
      private static byte[] _key = Encoding.ASCII.GetBytes("ucema49585!");

      public string Encriptar(string textoPlano)
      {
         byte[] iv = new byte[16]; // Vector de inicialización
      }
   }
}