using System;
using Console = System.Diagnostics.Debug;

namespace UCEMA.Proyecto_Base.NET7
{
   public partial class frmPrincipal : Form
   {
      public frmPrincipal()
      {
         InitializeComponent();
      }

      private void frmPrincipal_Load(object sender, EventArgs e)
      {
         this.KeyPreview = true;
         //this.KeyDown += new KeyEventHandler(frmPrincipal_KeyDown); // Si ya está bindeado desde el designer, acá no hace falta
         Console.WriteLine("frmPrincipal_Load");
      }

      private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
      {
         switch (e.KeyCode)
         {
            case Keys.Escape:
               {
                  DialogResult res = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                  if (res == DialogResult.Yes)
                  {
                     Application.Exit();
                  }
                  break;
               }
         }
      }

      private async void btnTestDB_Click(object sender, EventArgs e)
      {
         dgvResultados.DataSource = null;
         Refresh();

         Console.WriteLine("Empieza el RunAsync...");
         dgvResultados.DataSource = await FormUtils.RunAsync(() =>
         {
            return Logica.Script.TraerDatos();

            //Console.WriteLine("Empieza el RunAsync...");
            //Thread.Sleep(3000);
            //Console.WriteLine("Termina el RunAsync...");
            //return null;
         });
      }

      private void btnTest_Click(object sender, EventArgs e)
      {
         //statusProgress.
      }

      private void statusProgress_Click(object sender, EventArgs e)
      {

      }
   }
}