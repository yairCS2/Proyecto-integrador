namespace DevyClass
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            Guna.UI.Animation.Animation animation6 = new Guna.UI.Animation.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lbmenu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.transicionMenu = new Guna.UI.WinForms.GunaTransition(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRendimiento = new Guna.UI.WinForms.GunaButton();
            this.btnTemario = new Guna.UI.WinForms.GunaButton();
            this.btnLogros = new Guna.UI.WinForms.GunaButton();
            this.btnAjustes = new Guna.UI.WinForms.GunaButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelMenu);
            this.panel1.Controls.Add(this.panelLogo);
            this.panel1.Controls.Add(this.label2);
            this.transicionMenu.SetDecoration(this.panel1, Guna.UI.Animation.DecorationType.None);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 516);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.btnRendimiento);
            this.panelMenu.Controls.Add(this.btnTemario);
            this.panelMenu.Controls.Add(this.btnLogros);
            this.panelMenu.Controls.Add(this.btnAjustes);
            this.transicionMenu.SetDecoration(this.panelMenu, Guna.UI.Animation.DecorationType.None);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 76);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(185, 213);
            this.panelMenu.TabIndex = 8;
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.lbmenu);
            this.panelLogo.Controls.Add(this.pictureBox2);
            this.transicionMenu.SetDecoration(this.panelLogo, Guna.UI.Animation.DecorationType.None);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(2);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(185, 76);
            this.panelLogo.TabIndex = 5;
            // 
            // lbmenu
            // 
            this.lbmenu.AutoSize = true;
            this.transicionMenu.SetDecoration(this.lbmenu, Guna.UI.Animation.DecorationType.None);
            this.lbmenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbmenu.Location = new System.Drawing.Point(65, 28);
            this.lbmenu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbmenu.Name = "lbmenu";
            this.lbmenu.Size = new System.Drawing.Size(88, 18);
            this.lbmenu.TabIndex = 0;
            this.lbmenu.Text = "DevyClass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.transicionMenu.SetDecoration(this.label2, Guna.UI.Animation.DecorationType.None);
            this.label2.Location = new System.Drawing.Point(2, 493);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "DevyClass 1.1.0";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox1);
            this.transicionMenu.SetDecoration(this.panel2, Guna.UI.Animation.DecorationType.None);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(187, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 516);
            this.panel2.TabIndex = 3;
            // 
            // transicionMenu
            // 
            this.transicionMenu.AnimationType = Guna.UI.Animation.AnimationType.Scale;
            this.transicionMenu.Cursor = null;
            animation6.AnimateOnlyDifferences = true;
            animation6.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation6.BlindCoeff")));
            animation6.LeafCoeff = 0F;
            animation6.MaxTime = 1F;
            animation6.MinTime = 0F;
            animation6.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation6.MosaicCoeff")));
            animation6.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation6.MosaicShift")));
            animation6.MosaicSize = 0;
            animation6.Padding = new System.Windows.Forms.Padding(0);
            animation6.RotateCoeff = 0F;
            animation6.RotateLimit = 0F;
            animation6.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation6.ScaleCoeff")));
            animation6.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation6.SlideCoeff")));
            animation6.TimeCoeff = 0F;
            animation6.TransparencyCoeff = 0F;
            this.transicionMenu.DefaultAnimation = animation6;
            this.transicionMenu.AllAnimationsCompleted += new System.EventHandler(this.transicionMenu_AllAnimationsCompleted);
            // 
            // pictureBox1
            // 
            this.transicionMenu.SetDecoration(this.pictureBox1, Guna.UI.Animation.DecorationType.None);
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(67, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnRendimiento
            // 
            this.btnRendimiento.AnimationHoverSpeed = 0.07F;
            this.btnRendimiento.AnimationSpeed = 0.03F;
            this.btnRendimiento.BackColor = System.Drawing.Color.Transparent;
            this.btnRendimiento.BaseColor = System.Drawing.SystemColors.ControlLight;
            this.btnRendimiento.BorderColor = System.Drawing.Color.Black;
            this.transicionMenu.SetDecoration(this.btnRendimiento, Guna.UI.Animation.DecorationType.None);
            this.btnRendimiento.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRendimiento.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRendimiento.FocusedColor = System.Drawing.Color.Empty;
            this.btnRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRendimiento.ForeColor = System.Drawing.Color.Black;
            this.btnRendimiento.Image = global::DevyClass.Properties.Resources.crecimiento;
            this.btnRendimiento.ImageSize = new System.Drawing.Size(50, 50);
            this.btnRendimiento.Location = new System.Drawing.Point(0, 13);
            this.btnRendimiento.Margin = new System.Windows.Forms.Padding(2);
            this.btnRendimiento.Name = "btnRendimiento";
            this.btnRendimiento.OnHoverBaseColor = System.Drawing.Color.White;
            this.btnRendimiento.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnRendimiento.OnHoverForeColor = System.Drawing.Color.Black;
            this.btnRendimiento.OnHoverImage = null;
            this.btnRendimiento.OnPressedColor = System.Drawing.Color.Black;
            this.btnRendimiento.Radius = 20;
            this.btnRendimiento.Size = new System.Drawing.Size(185, 50);
            this.btnRendimiento.TabIndex = 6;
            this.btnRendimiento.Text = " Rendimiento";
            this.btnRendimiento.Click += new System.EventHandler(this.btnRendimiento_Click);
            // 
            // btnTemario
            // 
            this.btnTemario.AnimationHoverSpeed = 0.07F;
            this.btnTemario.AnimationSpeed = 0.03F;
            this.btnTemario.BackColor = System.Drawing.Color.Transparent;
            this.btnTemario.BaseColor = System.Drawing.SystemColors.ControlLight;
            this.btnTemario.BorderColor = System.Drawing.Color.Black;
            this.transicionMenu.SetDecoration(this.btnTemario, Guna.UI.Animation.DecorationType.None);
            this.btnTemario.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTemario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTemario.FocusedColor = System.Drawing.Color.Empty;
            this.btnTemario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemario.ForeColor = System.Drawing.Color.Black;
            this.btnTemario.Image = global::DevyClass.Properties.Resources.libro_abierto;
            this.btnTemario.ImageSize = new System.Drawing.Size(40, 40);
            this.btnTemario.Location = new System.Drawing.Point(0, 63);
            this.btnTemario.Margin = new System.Windows.Forms.Padding(2);
            this.btnTemario.Name = "btnTemario";
            this.btnTemario.OnHoverBaseColor = System.Drawing.Color.White;
            this.btnTemario.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnTemario.OnHoverForeColor = System.Drawing.Color.Black;
            this.btnTemario.OnHoverImage = null;
            this.btnTemario.OnPressedColor = System.Drawing.Color.Black;
            this.btnTemario.Radius = 20;
            this.btnTemario.Size = new System.Drawing.Size(185, 50);
            this.btnTemario.TabIndex = 1;
            this.btnTemario.Text = "    Temario";
            this.btnTemario.Click += new System.EventHandler(this.btnTemario_Click);
            // 
            // btnLogros
            // 
            this.btnLogros.AnimationHoverSpeed = 0.07F;
            this.btnLogros.AnimationSpeed = 0.03F;
            this.btnLogros.BackColor = System.Drawing.Color.Transparent;
            this.btnLogros.BaseColor = System.Drawing.SystemColors.ControlLight;
            this.btnLogros.BorderColor = System.Drawing.Color.Black;
            this.transicionMenu.SetDecoration(this.btnLogros, Guna.UI.Animation.DecorationType.None);
            this.btnLogros.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLogros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogros.FocusedColor = System.Drawing.Color.Empty;
            this.btnLogros.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogros.ForeColor = System.Drawing.Color.Black;
            this.btnLogros.Image = global::DevyClass.Properties.Resources.trofeo;
            this.btnLogros.ImageSize = new System.Drawing.Size(40, 40);
            this.btnLogros.Location = new System.Drawing.Point(0, 113);
            this.btnLogros.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogros.Name = "btnLogros";
            this.btnLogros.OnHoverBaseColor = System.Drawing.Color.White;
            this.btnLogros.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnLogros.OnHoverForeColor = System.Drawing.Color.Black;
            this.btnLogros.OnHoverImage = null;
            this.btnLogros.OnPressedColor = System.Drawing.Color.Black;
            this.btnLogros.Radius = 20;
            this.btnLogros.Size = new System.Drawing.Size(185, 50);
            this.btnLogros.TabIndex = 5;
            this.btnLogros.Text = "    Logros";
            // 
            // btnAjustes
            // 
            this.btnAjustes.AnimationHoverSpeed = 0.07F;
            this.btnAjustes.AnimationSpeed = 0.03F;
            this.btnAjustes.BackColor = System.Drawing.Color.Transparent;
            this.btnAjustes.BaseColor = System.Drawing.SystemColors.ControlLight;
            this.btnAjustes.BorderColor = System.Drawing.Color.Black;
            this.transicionMenu.SetDecoration(this.btnAjustes, Guna.UI.Animation.DecorationType.None);
            this.btnAjustes.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAjustes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAjustes.FocusedColor = System.Drawing.Color.Empty;
            this.btnAjustes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjustes.ForeColor = System.Drawing.Color.Black;
            this.btnAjustes.Image = global::DevyClass.Properties.Resources.configuraciones;
            this.btnAjustes.ImageSize = new System.Drawing.Size(40, 40);
            this.btnAjustes.Location = new System.Drawing.Point(0, 163);
            this.btnAjustes.Margin = new System.Windows.Forms.Padding(2);
            this.btnAjustes.Name = "btnAjustes";
            this.btnAjustes.OnHoverBaseColor = System.Drawing.Color.White;
            this.btnAjustes.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAjustes.OnHoverForeColor = System.Drawing.Color.Black;
            this.btnAjustes.OnHoverImage = null;
            this.btnAjustes.OnPressedColor = System.Drawing.Color.Black;
            this.btnAjustes.Radius = 20;
            this.btnAjustes.Size = new System.Drawing.Size(185, 50);
            this.btnAjustes.TabIndex = 7;
            this.btnAjustes.Text = "    Ajustes";
            // 
            // pictureBox2
            // 
            this.transicionMenu.SetDecoration(this.pictureBox2, Guna.UI.Animation.DecorationType.None);
            this.pictureBox2.Image = global::DevyClass.Properties.Resources.DevyPngSinFondo1;
            this.pictureBox2.Location = new System.Drawing.Point(11, 11);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(61, 51);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 516);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.transicionMenu.SetDecoration(this, Guna.UI.Animation.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbmenu;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI.WinForms.GunaButton btnTemario;
        private Guna.UI.WinForms.GunaButton btnAjustes;
        private Guna.UI.WinForms.GunaButton btnRendimiento;
        private Guna.UI.WinForms.GunaButton btnLogros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelLogo;
        private Guna.UI.WinForms.GunaTransition transicionMenu;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

