namespace UCEMA.Imputar_Pago.NET7
{
   public partial class frmPrincipal : Form
   {
      public frmPrincipal()
      {
         InitializeComponent();
      }

      private void frmPrincipal_Load(object sender, EventArgs e)
      {

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
   }
}