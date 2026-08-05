using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.Formularios_UI;
using DevyClass.UsuarioDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevyClass
{
    public partial class UI_Administrador : Form
    {
        private DataTable tablaUsuarios;
        private DatosUsuario UsuarioActual;
        public UI_Administrador(DatosUsuario usuario)
        {
            InitializeComponent();
            UsuarioActual = usuario;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();
                tablaUsuarios = dao.ObtenerTodosLosUsuarios();

                dgvUsuarios.DataSource = tablaUsuarios;

                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;

                dgvUsuarios.Columns["id_usuarios"].HeaderText = "ID";
                dgvUsuarios.Columns["username"].HeaderText = "Usuario";
                dgvUsuarios.Columns["correo"].HeaderText = "Correo";
                dgvUsuarios.Columns["fecha"].HeaderText = "Fecha nacimiento";
                dgvUsuarios.Columns["tipo"].HeaderText = "Tipo";
                dgvUsuarios.Columns["id_nivel"].HeaderText = "Nivel";
                dgvUsuarios.Columns["nombre"].HeaderText = "Nombre del nivel";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UI_Administrador_Load(object sender, EventArgs e)
        {


        }
        private void MostrarPanel(Panel panel)
        {
            panelBienvenida.Visible = false;
            panelGestionarUsuarios.Visible = false;

            panel.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarPanel(panelBienvenida);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelGestionarUsuarios);
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario adduser = new UI_AgregarUsuario(UsuarioActual);
            adduser.Show();
        }

     
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(UsuarioActual);
            this.Close();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal();
            this.Close();
            accederF1.Close();
        }

        private void btnAgregarUsuario_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario accederAU = new UI_AgregarUsuario(UsuarioActual);
            this.Hide();
            accederAU.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UI_EliminarUsuario eliminar = new UI_EliminarUsuario(UsuarioActual);
            this.Close();
            eliminar.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UI_EliminarUsuario eliminar = new UI_EliminarUsuario(UsuarioActual);
            this.Close();
            eliminar.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario accederAU = new UI_AgregarUsuario(UsuarioActual);
            this.Hide();
            accederAU.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tablaUsuarios == null) return;

            string filtro = txtBuscar.Text.Trim().Replace("'", "''"); // evita romper el RowFilter si escribe un apóstrofe

            tablaUsuarios.DefaultView.RowFilter = $"username LIKE '%{filtro}%'";
            dgvUsuarios.DataSource = tablaUsuarios.DefaultView;
        }
    } // Fin form
}
