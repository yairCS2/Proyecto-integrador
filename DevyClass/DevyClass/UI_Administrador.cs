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
        public UI_Administrador()
        {
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
            AgregarUsuario adduser = new AgregarUsuario();
            adduser.Show();
        }

    }
}
