namespace DevyClass
{
    partial class UI_GestionarNiveles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_GestionarNiveles));
            this.dgvNiveles = new System.Windows.Forms.DataGridView();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.labelXpNecesaria = new System.Windows.Forms.Label();
            this.txtXpNecesaria = new System.Windows.Forms.TextBox();
            this.labelXpOtorgada = new System.Windows.Forms.Label();
            this.txtXpOtorgada = new System.Windows.Forms.TextBox();
            this.labelModulo = new System.Windows.Forms.Label();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.pictureBoxBack = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNiveles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNiveles
            // 
            this.dgvNiveles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNiveles.Location = new System.Drawing.Point(14, 150);
            this.dgvNiveles.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNiveles.Name = "dgvNiveles";
            this.dgvNiveles.RowHeadersWidth = 62;
            this.dgvNiveles.RowTemplate.Height = 28;
            this.dgvNiveles.Size = new System.Drawing.Size(520, 300);
            this.dgvNiveles.TabIndex = 0;
            this.dgvNiveles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNiveles_CellClick);
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(321, 25);
            this.labelTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(263, 37);
            this.labelTitulo.TabIndex = 1;
            this.labelTitulo.Text = "Gestión de Niveles";
            // 
            // labelBuscar
            // 
            this.labelBuscar.AutoSize = true;
            this.labelBuscar.Location = new System.Drawing.Point(14, 108);
            this.labelBuscar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBuscar.Name = "labelBuscar";
            this.labelBuscar.Size = new System.Drawing.Size(68, 13);
            this.labelBuscar.TabIndex = 3;
            this.labelBuscar.Text = "Buscar por ID:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(14, 125);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(260, 20);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(556, 90);
            this.labelNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(47, 13);
            this.labelNombre.TabIndex = 4;
            this.labelNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(556, 108);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(210, 20);
            this.txtNombre.TabIndex = 5;
            // 
            // labelXpNecesaria
            // 
            this.labelXpNecesaria.AutoSize = true;
            this.labelXpNecesaria.Location = new System.Drawing.Point(556, 138);
            this.labelXpNecesaria.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelXpNecesaria.Name = "labelXpNecesaria";
            this.labelXpNecesaria.Size = new System.Drawing.Size(73, 13);
            this.labelXpNecesaria.TabIndex = 6;
            this.labelXpNecesaria.Text = "XP necesaria:";
            // 
            // txtXpNecesaria
            // 
            this.txtXpNecesaria.Location = new System.Drawing.Point(556, 156);
            this.txtXpNecesaria.Margin = new System.Windows.Forms.Padding(2);
            this.txtXpNecesaria.Name = "txtXpNecesaria";
            this.txtXpNecesaria.Size = new System.Drawing.Size(210, 20);
            this.txtXpNecesaria.TabIndex = 7;
            // 
            // labelXpOtorgada
            // 
            this.labelXpOtorgada.AutoSize = true;
            this.labelXpOtorgada.Location = new System.Drawing.Point(556, 186);
            this.labelXpOtorgada.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelXpOtorgada.Name = "labelXpOtorgada";
            this.labelXpOtorgada.Size = new System.Drawing.Size(69, 13);
            this.labelXpOtorgada.TabIndex = 8;
            this.labelXpOtorgada.Text = "XP otorgada:";
            // 
            // txtXpOtorgada
            // 
            this.txtXpOtorgada.Location = new System.Drawing.Point(556, 204);
            this.txtXpOtorgada.Margin = new System.Windows.Forms.Padding(2);
            this.txtXpOtorgada.Name = "txtXpOtorgada";
            this.txtXpOtorgada.Size = new System.Drawing.Size(210, 20);
            this.txtXpOtorgada.TabIndex = 9;
            // 
            // labelModulo
            // 
            this.labelModulo.AutoSize = true;
            this.labelModulo.Location = new System.Drawing.Point(556, 234);
            this.labelModulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelModulo.Name = "labelModulo";
            this.labelModulo.Size = new System.Drawing.Size(45, 13);
            this.labelModulo.TabIndex = 10;
            this.labelModulo.Text = "Módulo:";
            // 
            // cboModulo
            // 
            this.cboModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(556, 252);
            this.cboModulo.Margin = new System.Windows.Forms.Padding(2);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(210, 21);
            this.cboModulo.TabIndex = 11;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Azure;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(556, 295);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(210, 34);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Azure;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Location = new System.Drawing.Point(556, 338);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(210, 34);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Azure;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Location = new System.Drawing.Point(556, 381);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(210, 34);
            this.btnEliminar.TabIndex = 14;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Azure;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Location = new System.Drawing.Point(556, 424);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(210, 34);
            this.btnLimpiar.TabIndex = 15;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // pictureBoxBack
            // 
            this.pictureBoxBack.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxBack.Image = global::DevyClass.Properties.Resources.atras;
            this.pictureBoxBack.Location = new System.Drawing.Point(11, 11);
            this.pictureBoxBack.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxBack.Name = "pictureBoxBack";
            this.pictureBoxBack.Size = new System.Drawing.Size(47, 42);
            this.pictureBoxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBack.TabIndex = 16;
            this.pictureBoxBack.TabStop = false;
            this.pictureBoxBack.Click += new System.EventHandler(this.pictureBoxBack_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DevyClass.Properties.Resources.Devy_sentado;
            this.pictureBox6.Location = new System.Drawing.Point(248, 11);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(78, 76);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 59;
            this.pictureBox6.TabStop = false;
            // 
            // UI_GestionarNiveles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 470);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBoxBack);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cboModulo);
            this.Controls.Add(this.labelModulo);
            this.Controls.Add(this.txtXpOtorgada);
            this.Controls.Add(this.labelXpOtorgada);
            this.Controls.Add(this.txtXpNecesaria);
            this.Controls.Add(this.labelXpNecesaria);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.labelBuscar);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.dgvNiveles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI_GestionarNiveles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI_GestionarNiveles";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNiveles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNiveles;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label labelXpNecesaria;
        private System.Windows.Forms.TextBox txtXpNecesaria;
        private System.Windows.Forms.Label labelXpOtorgada;
        private System.Windows.Forms.TextBox txtXpOtorgada;
        private System.Windows.Forms.Label labelModulo;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.PictureBox pictureBoxBack;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}
