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
    public partial class UI_AgregarUsuario : Form
    {
        private DatosUsuario UsuarioActual;
        public UI_AgregarUsuario(DatosUsuario usuario)
        {
            UsuarioActual = usuario;
            InitializeComponent();
        }

        private void AgregarUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            UI_Administrador accederF1 = new UI_Administrador(UsuarioActual);
            this.Close();
            accederF1.ShowDialog();
        }
    }
}
