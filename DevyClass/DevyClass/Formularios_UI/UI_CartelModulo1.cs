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

namespace DevyClass.Formularios_UI
{
    public partial class UI_CartelModulo1 : Form
    {
        private DatosUsuario UsuarioActual;
        public UI_CartelModulo1(DatosUsuario usuario)
        {
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(UsuarioActual);
            this.Hide();
            accederF1.Show();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion iniciar = new UI_InicioSesion();
            this.Hide();
            iniciar.Show();
        }
    }
}
