using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades
{
   static class Seguridad
   {
      private static byte[] _key = Encoding.UTF8.GetBytes("ucema49585!");

      /// <summary>
      ///  Encripta cadenas de texto en base64.
      /// </summary>
      /// <param name="textoPlano">
      ///  Cadena de texto a encriptar.
      /// </param>
      /// <returns>
      ///  Cadena de texto encriptada.
      /// </returns>

      /// <exception cref="CryptographicException">
      /// Se lanza cuando la cadena de texto no puede ser desencriptada.
      /// </exception>
      /// <exception cref="FormatException">
      /// Se lanza cuando la cadena de texto no está en formato base64.
      /// </exception>
      /// <!--
      /// <exception cref="ArgumentNullException">
      /// Se lanza cuando la cadena de texto es nula.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// Se lanza cuando la cadena de texto está vacía.
      /// </exception>
      /// <exception cref="OverflowException">
      /// Se lanza cuando la cadena de texto es demasiado larga.
      /// </exception>
      /// <exception cref="ObjectDisposedException">
      /// Se lanza cuando el objeto ha sido desechado.
      /// </exception>
      /// <exception cref="OutOfMemoryException">
      /// Se lanza cuando no hay suficiente memoria para continuar con la operación.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// Se lanza cuando el sistema operativo no es compatible.
      /// </exception>
      /// <exception cref="SystemException">
      /// Se lanza cuando se produce un error de desencriptación.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// Se lanza cuando no se tiene acceso a la cadena de texto.
      /// </exception>
      /// <exception cref="IOException">
      /// Se lanza cuando se produce un error de entrada/salida.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// Se lanza cuando no se admite la operación.
      /// </exception>
      /// <exception cref="NullReferenceException">
      /// Se lanza cuando se intenta desencriptar una cadena de texto nula.
      /// </exception>
      /// -->
      public static string Encriptar(string textoPlano)
      {
         byte[] iv = new byte[16]; // Vector de inicialización

         //using AesManaged aes = new();
         using Aes aes = Aes.Create();

         aes.Key = _key;
         aes.IV = iv;

         using (MemoryStream ms = new MemoryStream())
         {
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
               using (StreamWriter sw = new StreamWriter(cs))
               {
                  sw.Write(textoPlano);
               }
               return Convert.ToBase64String(ms.ToArray());
            }
         }
      }


      /// <summary>
      /// Desencripta cadenas de texto en base64.
      /// </summary>
      /// <param name="textoEncriptado">
      /// Cadena de texto a desencriptar.
      /// </param>
      /// <returns>
      /// Cadena de texto desencriptada.
      /// </returns>

      /// <exception cref="CryptographicException">
      /// Se lanza cuando la cadena de texto no puede ser desencriptada.
      /// </exception>
      /// <exception cref="FormatException">
      /// Se lanza cuando la cadena de texto no está en formato base64.
      /// </exception>
      /// <!--
      /// <exception cref="ArgumentNullException">
      /// Se lanza cuando la cadena de texto es nula.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// Se lanza cuando la cadena de texto está vacía.
      /// </exception>
      /// <exception cref="OverflowException">
      /// Se lanza cuando la cadena de texto es demasiado larga.
      /// </exception>
      /// <exception cref="ObjectDisposedException">
      /// Se lanza cuando el objeto ha sido desechado.
      /// </exception>
      /// <exception cref="OutOfMemoryException">
      /// Se lanza cuando no hay suficiente memoria para continuar con la operación.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// Se lanza cuando el sistema operativo no es compatible.
      /// </exception>
      /// <exception cref="SystemException">
      /// Se lanza cuando se produce un error de desencriptación.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// Se lanza cuando no se tiene acceso a la cadena de texto.
      /// </exception>
      /// <exception cref="IOException">
      /// Se lanza cuando se produce un error de entrada/salida.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// Se lanza cuando no se admite la operación.
      /// </exception>
      /// <exception cref="NullReferenceException">
      /// Se lanza cuando se intenta desencriptar una cadena de texto nula.
      /// </exception>
      /// -->
      public static string Desencriptar(string textoEncriptado)
      {
         byte[] iv = new byte[16];

         //using AesManaged aes = new();
         using Aes aes = Aes.Create();

         aes.Key = _key;
         aes.IV = iv;

         byte[] encryptedBytes = Convert.FromBase64String(textoEncriptado);

         using (MemoryStream ms = new MemoryStream(encryptedBytes))
         {
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
               using (StreamReader sr = new StreamReader(cs))
               {
                  return sr.ReadToEnd();
               }
            }
         }
      }
   }
}