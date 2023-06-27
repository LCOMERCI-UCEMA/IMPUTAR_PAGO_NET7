using System;
using System.Reflection;
using System.Windows.Forms;
using UCEMA.Imputar_Pago_SIGEU.NET7;

using Console = System.Diagnostics.Debug;

namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   [AttributeUsage(AttributeTargets.Method)]
   public class HasLoadingAttribute : Attribute
   {
      public string[] ControlesParaDeshabilitar { get; set; }
      public string ControlLoading { get; set; }

      public HasLoadingAttribute(string cntlLoading, string[] controlesParaDeshabilitar)
      {
         ControlLoading = cntlLoading;
         ControlesParaDeshabilitar = controlesParaDeshabilitar;
      }

      public HasLoadingAttribute(string cntlLoading, string controlParaDeshabilitar)
      {
         ControlLoading = cntlLoading;
         ControlesParaDeshabilitar = new string[] { controlParaDeshabilitar };
      }

      // Ctor def.
      public HasLoadingAttribute(string cntlLoading)
      {
         string msj = "HasLoadingAttribute::HasLoadingAttribute(string cntlLoading)";

         Console.WriteLine(msj);
         //MessageBox.Show(msj);
         ControlLoading = cntlLoading;
         ControlesParaDeshabilitar = new string[] { };
      }

      public HasLoadingAttribute(Action startupCode)
      {
         startupCode.Invoke();
         ControlLoading = "picLoading";
         ControlesParaDeshabilitar = new string[] { };
      }

      // Este método va a ser llamado por el método que se está decorando.
      public void LoadingON()
      {
         Console.WriteLine("HasLoadingAttribute::LoadingON()");
         // Busco el control picLoading en el formulario que está ejecutando el método decorado.
         // Si no lo encuentra, tira una excepción.
         string loading = ControlLoading;

         MemberInfo[] refLoading = typeof(Program).GetMember(loading, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

         Console.WriteLine("memberInfo:");
         foreach (MemberInfo memberInfo in refLoading)
         {
            // Escribo la info de memberInfo en la consola sin importar el tipo de dato que sea.
            Console.WriteLine(memberInfo);
         }

         //// Si lo encuentra, lo pone visible.
         //loading.Visible = true;

         //if (ControlesParaDeshabilitar.Length > 0){

         //   // Deshabilito los controles que son para deshabilitar.
         //   foreach (string control in ControlesParaDeshabilitar)
         //   {
         //      control.Enabled = false;
         //   }
         //}
      }

      // Declaro el método LoadingOFF, que será llamado al finalizar la ejecución del método decorado.
      public void LoadingOFF()
      {
         // Busco el control picLoading en el formulario que está ejecutando el método decorado.
         // Si no lo encuentra, tira una excepción.
         string loading = ControlLoading;

         // Tomar el nombre del control del string loading e instanciar un Control con ese nombre.
         // Si no lo encuentra, tira una excepción.
         MemberInfo[] refLoading = typeof(Program).GetMember(loading, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

         //// Si lo encuentra, lo pone invisible.
         //loading.Visible = false;

         //if (ControlesParaDeshabilitar.Length > 0){
         //   // Habilito los controles que son para deshabilitar.
         //   foreach (Control control in ControlesParaDeshabilitar)
         //   {
         //      control.Enabled = true;
         //   }
         //}
      }

      public void OnEntry()
      {
          LoadingON();
      }

      public void OnExit()
      {
          LoadingOFF();
      }
   }
}
