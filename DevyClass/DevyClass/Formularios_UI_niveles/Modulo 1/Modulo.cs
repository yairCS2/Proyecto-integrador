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

namespace DevyClass.Formularios_UI_niveles.Modulo_1
{
    public partial class Modulo : Form
    {
        private DatosUsuario UsuarioActual;
        public Modulo(DatosUsuario usuario)
        {
            UsuarioActual = usuario;
            InitializeComponent();
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(UsuarioActual);
            this.Hide();
            accederF1.Show();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
            this.Hide();
        }

        private void lbusuario_Click(object sender, EventArgs e)
        {

        }
    }
}
