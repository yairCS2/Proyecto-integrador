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
        private DatosUsuario UsuarioActual;
        public UI_Administrador(DatosUsuario usuario)
        {
            UsuarioActual = usuario;
            InitializeComponent();
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
            this.Hide();
            accederF1.Show();

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

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    } // Fin form
}
