namespace DevyClass
{
    partial class formRendimiento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnregresar = new Guna.UI.WinForms.GunaButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(753, 454);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnregresar
            // 
            this.btnregresar.Animated = true;
            this.btnregresar.AnimationHoverSpeed = 0.07F;
            this.btnregresar.AnimationSpeed = 0.03F;
            this.btnregresar.BackColor = System.Drawing.Color.Transparent;
            this.btnregresar.BaseColor = System.Drawing.Color.Black;
            this.btnregresar.BorderColor = System.Drawing.Color.Black;
            this.btnregresar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnregresar.FocusedColor = System.Drawing.Color.Empty;
            this.btnregresar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnregresar.ForeColor = System.Drawing.Color.White;
            this.btnregresar.Image = global::DevyClass.Properties.Resources.DevyPngSinFondo_botones;
            this.btnregresar.ImageSize = new System.Drawing.Size(40, 40);
            this.btnregresar.Location = new System.Drawing.Point(645, 472);
            this.btnregresar.Name = "btnregresar";
            this.btnregresar.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnregresar.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnregresar.OnHoverForeColor = System.Drawing.Color.White;
            this.btnregresar.OnHoverImage = null;
            this.btnregresar.OnPressedColor = System.Drawing.Color.Black;
            this.btnregresar.Radius = 20;
            this.btnregresar.Size = new System.Drawing.Size(120, 44);
            this.btnregresar.TabIndex = 1;
            this.btnregresar.Text = "Regresar";
            this.btnregresar.Click += new System.EventHandler(this.btnregresar_Click);
            // 
            // formRendimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 516);
            this.Controls.Add(this.btnregresar);
            this.Controls.Add(this.dataGridView1);
            this.Name = "formRendimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formRendimiento";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI.WinForms.GunaButton btnregresar;
    }
}