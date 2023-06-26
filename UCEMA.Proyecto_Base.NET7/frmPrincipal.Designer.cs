namespace UCEMA.Proyecto_Base.NET7
{
   partial class frmPrincipal
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
         dgvResultados = new DataGridView();
         picLoading = new PictureBox();
         statusBar = new StatusStrip();
         statusProgress = new ToolStripProgressBar();
         statusInformation = new ToolStripStatusLabel();
         statusDropdown = new ToolStripDropDownButton();
         statusSplitButton = new ToolStripSplitButton();
         dlgAbrirArchivo = new OpenFileDialog();
         btnTestDB = new Button();
         btnTest = new Button();
         ((System.ComponentModel.ISupportInitialize) dgvResultados).BeginInit();
         ((System.ComponentModel.ISupportInitialize) picLoading).BeginInit();
         statusBar.SuspendLayout();
         SuspendLayout();
         // 
         // dgvResultados
         // 
         dgvResultados.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle2.BackColor = Color.Firebrick;
         dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
         dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
         dataGridViewCellStyle2.SelectionBackColor = Color.IndianRed;
         dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
         dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
         dgvResultados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
         dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvResultados.Location = new Point(12, 12);
         dgvResultados.Name = "dgvResultados";
         dgvResultados.RowTemplate.Height = 25;
         dgvResultados.Size = new Size(731, 429);
         dgvResultados.TabIndex = 0;
         // 
         // picLoading
         // 
         picLoading.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         picLoading.Image = Proyecto_Base.NET7.Properties.Resources.loading;
         picLoading.Location = new Point(751, 58);
         picLoading.Name = "picLoading";
         picLoading.Size = new Size(40, 40);
         picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
         picLoading.TabIndex = 1;
         picLoading.TabStop = false;
         picLoading.Visible = false;
         // 
         // statusBar
         // 
         statusBar.Items.AddRange(new ToolStripItem[] { statusProgress, statusInformation, statusDropdown, statusSplitButton });
         statusBar.Location = new Point(0, 444);
         statusBar.Name = "statusBar";
         statusBar.RenderMode = ToolStripRenderMode.Professional;
         statusBar.Size = new Size(800, 22);
         statusBar.TabIndex = 2;
         // 
         // statusProgress
         // 
         statusProgress.Alignment = ToolStripItemAlignment.Right;
         statusProgress.Margin = new Padding(11, 3, 1, 3);
         statusProgress.Name = "statusProgress";
         statusProgress.Size = new Size(100, 16);
         statusProgress.Style = ProgressBarStyle.Marquee;
         statusProgress.Click += statusProgress_Click;
         // 
         // statusInformation
         // 
         statusInformation.AutoSize = false;
         statusInformation.Margin = new Padding(4, 3, 0, 2);
         statusInformation.Name = "statusInformation";
         statusInformation.Size = new Size(101, 17);
         statusInformation.Text = "statusInformation";
         // 
         // statusDropdown
         // 
         statusDropdown.DisplayStyle = ToolStripItemDisplayStyle.Image;
         statusDropdown.Image = (Image) resources.GetObject("statusDropdown.Image");
         statusDropdown.ImageTransparentColor = Color.Magenta;
         statusDropdown.Name = "statusDropdown";
         statusDropdown.Size = new Size(29, 20);
         statusDropdown.Text = "statusDropdown";
         // 
         // statusSplitButton
         // 
         statusSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
         statusSplitButton.Image = (Image) resources.GetObject("statusSplitButton.Image");
         statusSplitButton.ImageTransparentColor = Color.Magenta;
         statusSplitButton.Name = "statusSplitButton";
         statusSplitButton.Size = new Size(32, 20);
         statusSplitButton.Text = "statusSplitButton";
         // 
         // btnTestDB
         // 
         btnTestDB.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         btnTestDB.Location = new Point(751, 11);
         btnTestDB.Name = "btnTestDB";
         btnTestDB.Size = new Size(40, 40);
         btnTestDB.TabIndex = 3;
         btnTestDB.Text = "&DB";
         btnTestDB.UseVisualStyleBackColor = true;
         btnTestDB.Click += btnTestDB_Click;
         // 
         // btnTest
         // 
         btnTest.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
         btnTest.Location = new Point(751, 104);
         btnTest.Name = "btnTest";
         btnTest.Size = new Size(40, 40);
         btnTest.TabIndex = 4;
         btnTest.Text = "PGB";
         btnTest.UseVisualStyleBackColor = true;
         btnTest.Click += btnTest_Click;
         // 
         // frmPrincipal
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(800, 466);
         Controls.Add(btnTest);
         Controls.Add(btnTestDB);
         Controls.Add(statusBar);
         Controls.Add(picLoading);
         Controls.Add(dgvResultados);
         Icon = (Icon) resources.GetObject("$this.Icon");
         KeyPreview = true;
         Name = "frmPrincipal";
         StartPosition = FormStartPosition.CenterScreen;
         Text = "Proyecto Base - UCEMA";
         Load += frmPrincipal_Load;
         KeyDown += frmPrincipal_KeyDown;
         ((System.ComponentModel.ISupportInitialize) dgvResultados).EndInit();
         ((System.ComponentModel.ISupportInitialize) picLoading).EndInit();
         statusBar.ResumeLayout(false);
         statusBar.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private DataGridView dgvResultados;
      private PictureBox picLoading;
      private StatusStrip statusBar;
      private OpenFileDialog dlgAbrirArchivo;
      private Button btnTestDB;
      private ToolStripProgressBar statusProgress;
      private ToolStripStatusLabel statusInformation;
      private ToolStripDropDownButton statusDropdown;
      private ToolStripSplitButton statusSplitButton;
      private Button btnTest;
   }
}