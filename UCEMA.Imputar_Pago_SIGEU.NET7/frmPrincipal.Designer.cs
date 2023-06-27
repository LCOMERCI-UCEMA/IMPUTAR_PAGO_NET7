namespace UCEMA.Imputar_Pago_SIGEU.NET7
{
   partial class frmPrincipal
   {
      /// <summary>
      /// Variable del diseñador necesaria.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Limpiar los recursos que se estén usando.
      /// </summary>
      /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Código generado por el Diseñador de Windows Forms

      /// <summary>
      /// Método necesario para admitir el Diseñador. No se puede modificar
      /// el contenido de este método con el editor de código.
      /// </summary>
      private void InitializeComponent()
      {
         components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
         lblTipoImputacion = new Label();
         cboTipoImputacion = new ComboBox();
         btnCargar = new Button();
         dlgAbrirArchivo = new OpenFileDialog();
         statusInformation = new StatusStrip();
         tlblStatus = new ToolStripStatusLabel();
         lblSTDOUT = new Label();
         txtStdout = new TextBox();
         btnLimpiarStdout = new Button();
         lblStderr = new Label();
         txtStderr = new TextBox();
         btnLimpiarStderr = new Button();
         splitCapturaStream = new SplitContainer();
         picLoading = new PictureBox();
         bindingSource1 = new BindingSource(components);
         dgvVerificacion = new DataGridView();
         lblVerificacion = new Label();
         btnVerificar = new Button();
         pgbInProgress = new ProgressBar();
         statusInformation.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize) splitCapturaStream).BeginInit();
         splitCapturaStream.Panel1.SuspendLayout();
         splitCapturaStream.Panel2.SuspendLayout();
         splitCapturaStream.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize) picLoading).BeginInit();
         ((System.ComponentModel.ISupportInitialize) bindingSource1).BeginInit();
         ((System.ComponentModel.ISupportInitialize) dgvVerificacion).BeginInit();
         SuspendLayout();
         // 
         // lblTipoImputacion
         // 
         lblTipoImputacion.AutoSize = true;
         lblTipoImputacion.Location = new Point(85, 16);
         lblTipoImputacion.Margin = new Padding(4, 0, 4, 0);
         lblTipoImputacion.Name = "lblTipoImputacion";
         lblTipoImputacion.Size = new Size(33, 15);
         lblTipoImputacion.TabIndex = 0;
         lblTipoImputacion.Text = "Tipo:";
         // 
         // cboTipoImputacion
         // 
         cboTipoImputacion.Anchor =  AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
         cboTipoImputacion.BackColor = Color.White;
         cboTipoImputacion.DropDownStyle = ComboBoxStyle.DropDownList;
         cboTipoImputacion.FlatStyle = FlatStyle.System;
         cboTipoImputacion.FormattingEnabled = true;
         cboTipoImputacion.Location = new Point(128, 12);
         cboTipoImputacion.Margin = new Padding(4, 3, 4, 3);
         cboTipoImputacion.Name = "cboTipoImputacion";
         cboTipoImputacion.Size = new Size(573, 23);
         cboTipoImputacion.TabIndex = 1;
         // 
         // btnCargar
         // 
         btnCargar.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         btnCargar.Location = new Point(709, 9);
         btnCargar.Margin = new Padding(4, 3, 4, 3);
         btnCargar.Name = "btnCargar";
         btnCargar.Size = new Size(138, 28);
         btnCargar.TabIndex = 2;
         btnCargar.Text = "&Cargar...";
         btnCargar.UseVisualStyleBackColor = true;
         btnCargar.Click += btnCargar_Click;
         // 
         // statusInformation
         // 
         statusInformation.Items.AddRange(new ToolStripItem[] { tlblStatus });
         statusInformation.Location = new Point(0, 821);
         statusInformation.Name = "statusInformation";
         statusInformation.Padding = new Padding(1, 0, 16, 0);
         statusInformation.Size = new Size(890, 22);
         statusInformation.TabIndex = 3;
         statusInformation.Text = "No pasa nada, loko. Está todo bien.";
         // 
         // tlblStatus
         // 
         tlblStatus.AutoSize = false;
         tlblStatus.Name = "tlblStatus";
         tlblStatus.Size = new Size(110, 17);
         // 
         // lblSTDOUT
         // 
         lblSTDOUT.AutoSize = true;
         lblSTDOUT.Location = new Point(20, 3);
         lblSTDOUT.Margin = new Padding(4, 0, 4, 0);
         lblSTDOUT.Name = "lblSTDOUT";
         lblSTDOUT.Size = new Size(98, 15);
         lblSTDOUT.TabIndex = 4;
         lblSTDOUT.Text = "Standard Output:";
         // 
         // txtStdout
         // 
         txtStdout.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         txtStdout.BackColor = SystemColors.Control;
         txtStdout.BorderStyle = BorderStyle.FixedSingle;
         txtStdout.Font = new Font("Cascadia Mono", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
         txtStdout.ForeColor = Color.FromArgb(  0,   64,   0);
         txtStdout.Location = new Point(130, 3);
         txtStdout.Margin = new Padding(4, 3, 4, 3);
         txtStdout.Multiline = true;
         txtStdout.Name = "txtStdout";
         txtStdout.ReadOnly = true;
         txtStdout.ScrollBars = ScrollBars.Both;
         txtStdout.Size = new Size(572, 306);
         txtStdout.TabIndex = 5;
         // 
         // btnLimpiarStdout
         // 
         btnLimpiarStdout.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         btnLimpiarStdout.Location = new Point(709, 3);
         btnLimpiarStdout.Margin = new Padding(4, 3, 4, 3);
         btnLimpiarStdout.Name = "btnLimpiarStdout";
         btnLimpiarStdout.Size = new Size(138, 29);
         btnLimpiarStdout.TabIndex = 6;
         btnLimpiarStdout.Text = "Limpiar Std&out";
         btnLimpiarStdout.UseVisualStyleBackColor = true;
         btnLimpiarStdout.Click += btnLimpiarStdout_Click;
         // 
         // lblStderr
         // 
         lblStderr.AutoSize = true;
         lblStderr.Location = new Point(30, 5);
         lblStderr.Margin = new Padding(4, 0, 4, 0);
         lblStderr.Name = "lblStderr";
         lblStderr.Size = new Size(85, 15);
         lblStderr.TabIndex = 4;
         lblStderr.Text = "Standard Error:";
         // 
         // txtStderr
         // 
         txtStderr.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         txtStderr.BackColor = SystemColors.Control;
         txtStderr.BorderStyle = BorderStyle.FixedSingle;
         txtStderr.Font = new Font("Cascadia Mono", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
         txtStderr.ForeColor = Color.FromArgb(  64,   0,   0);
         txtStderr.Location = new Point(128, 5);
         txtStderr.Margin = new Padding(4, 3, 4, 3);
         txtStderr.Multiline = true;
         txtStderr.Name = "txtStderr";
         txtStderr.ReadOnly = true;
         txtStderr.ScrollBars = ScrollBars.Both;
         txtStderr.Size = new Size(574, 295);
         txtStderr.TabIndex = 5;
         // 
         // btnLimpiarStderr
         // 
         btnLimpiarStderr.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         btnLimpiarStderr.Location = new Point(709, 5);
         btnLimpiarStderr.Margin = new Padding(4, 3, 4, 3);
         btnLimpiarStderr.Name = "btnLimpiarStderr";
         btnLimpiarStderr.Size = new Size(138, 29);
         btnLimpiarStderr.TabIndex = 6;
         btnLimpiarStderr.Text = "Limpiar Std&err";
         btnLimpiarStderr.UseVisualStyleBackColor = true;
         btnLimpiarStderr.Click += btnLimpiarStderr_Click;
         // 
         // splitCapturaStream
         // 
         splitCapturaStream.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         splitCapturaStream.Location = new Point(0, 43);
         splitCapturaStream.Margin = new Padding(4, 3, 4, 3);
         splitCapturaStream.Name = "splitCapturaStream";
         splitCapturaStream.Orientation = Orientation.Horizontal;
         // 
         // splitCapturaStream.Panel1
         // 
         splitCapturaStream.Panel1.Controls.Add(lblSTDOUT);
         splitCapturaStream.Panel1.Controls.Add(txtStdout);
         splitCapturaStream.Panel1.Controls.Add(btnLimpiarStdout);
         // 
         // splitCapturaStream.Panel2
         // 
         splitCapturaStream.Panel2.Controls.Add(lblStderr);
         splitCapturaStream.Panel2.Controls.Add(btnLimpiarStderr);
         splitCapturaStream.Panel2.Controls.Add(txtStderr);
         splitCapturaStream.Size = new Size(890, 627);
         splitCapturaStream.SplitterDistance = 312;
         splitCapturaStream.SplitterWidth = 5;
         splitCapturaStream.TabIndex = 7;
         // 
         // picLoading
         // 
         picLoading.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         picLoading.Image = Properties.Resources.loading;
         picLoading.Location = new Point(854, 9);
         picLoading.Margin = new Padding(4, 3, 4, 3);
         picLoading.Name = "picLoading";
         picLoading.Size = new Size(28, 28);
         picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
         picLoading.TabIndex = 7;
         picLoading.TabStop = false;
         picLoading.Visible = false;
         // 
         // dgvVerificacion
         // 
         dgvVerificacion.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         dgvVerificacion.BackgroundColor = SystemColors.Control;
         dgvVerificacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvVerificacion.Location = new Point(128, 669);
         dgvVerificacion.Margin = new Padding(4, 3, 4, 3);
         dgvVerificacion.Name = "dgvVerificacion";
         dgvVerificacion.Size = new Size(574, 145);
         dgvVerificacion.TabIndex = 8;
         // 
         // lblVerificacion
         // 
         lblVerificacion.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left;
         lblVerificacion.AutoSize = true;
         lblVerificacion.Location = new Point(46, 673);
         lblVerificacion.Margin = new Padding(4, 0, 4, 0);
         lblVerificacion.Name = "lblVerificacion";
         lblVerificacion.Size = new Size(71, 15);
         lblVerificacion.TabIndex = 4;
         lblVerificacion.Text = "Verificación:";
         // 
         // btnVerificar
         // 
         btnVerificar.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
         btnVerificar.Enabled = false;
         btnVerificar.Location = new Point(709, 669);
         btnVerificar.Margin = new Padding(4, 3, 4, 3);
         btnVerificar.Name = "btnVerificar";
         btnVerificar.Size = new Size(138, 29);
         btnVerificar.TabIndex = 6;
         btnVerificar.Text = "&Verificar";
         btnVerificar.UseVisualStyleBackColor = true;
         btnVerificar.Click += btnLimpiarStderr_Click;
         // 
         // pgbInProgress
         // 
         pgbInProgress.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
         pgbInProgress.Location = new Point(586, 822);
         pgbInProgress.Margin = new Padding(4, 3, 4, 3);
         pgbInProgress.Name = "pgbInProgress";
         pgbInProgress.Size = new Size(117, 21);
         pgbInProgress.Style = ProgressBarStyle.Continuous;
         pgbInProgress.TabIndex = 9;
         pgbInProgress.Visible = false;
         // 
         // frmPrincipal
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(890, 843);
         Controls.Add(pgbInProgress);
         Controls.Add(lblVerificacion);
         Controls.Add(btnVerificar);
         Controls.Add(dgvVerificacion);
         Controls.Add(picLoading);
         Controls.Add(splitCapturaStream);
         Controls.Add(statusInformation);
         Controls.Add(btnCargar);
         Controls.Add(cboTipoImputacion);
         Controls.Add(lblTipoImputacion);
         Icon = (Icon) resources.GetObject("$this.Icon");
         KeyPreview = true;
         Margin = new Padding(4, 3, 4, 3);
         Name = "frmPrincipal";
         StartPosition = FormStartPosition.CenterScreen;
         Text = "Imputación de pago";
         Load += frmPrincipal_Load;
         KeyDown += frmPrincipal_KeyDown;
         statusInformation.ResumeLayout(false);
         statusInformation.PerformLayout();
         splitCapturaStream.Panel1.ResumeLayout(false);
         splitCapturaStream.Panel1.PerformLayout();
         splitCapturaStream.Panel2.ResumeLayout(false);
         splitCapturaStream.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize) splitCapturaStream).EndInit();
         splitCapturaStream.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize) picLoading).EndInit();
         ((System.ComponentModel.ISupportInitialize) bindingSource1).EndInit();
         ((System.ComponentModel.ISupportInitialize) dgvVerificacion).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Label lblTipoImputacion;
      private ComboBox cboTipoImputacion;
      private Button btnCargar;
      private OpenFileDialog dlgAbrirArchivo;
      private BindingSource bindingSource1;
      private StatusStrip statusInformation;
      private Label lblSTDOUT;
      private TextBox txtStdout;
      private Button btnLimpiarStdout;
      private Label lblStderr;
      private TextBox txtStderr;
      private Button btnLimpiarStderr;
      private SplitContainer splitCapturaStream;
      private PictureBox picLoading;
      private DataGridView dgvVerificacion;
      private Label lblVerificacion;
      private Button btnVerificar;
      private ToolStripStatusLabel tlblStatus;
      private ProgressBar pgbInProgress;
   }
}

