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

namespace DevyClass.Formularios_UI_niveles.Modulo_1
{
    public partial class Modulo : Form
    {
        private DatosUsuario UsuarioActual;
        public Modulo(DatosUsuario usuario)
        {
            // usuario.UltimoNivel % 10 : esta exprecion representa el nivel en que esta.
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            // inicializa el formulario y establece el usuario actual, la barra de progreso y el nivel de progreso según el último nivel del usuario.
            UsuarioActual = usuario;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            if (usuario.UltimoNivel < 10)
            {
                progressBar1.Value = (usuario.UltimoNivel % 10) * 10 ?? 0;
                lblNivelProgreso.Text = $"{usuario.UltimoNivel % 10 ?? 0} de 10 niveles completados";
            }
            else progressBar1.Value = 100;


            PictureBox[] lblNiveles = { lblNivel1, lblNivel2, lblNivel3, lblNivel4, lblNivel5,
                             lblNivel6, lblNivel7, lblNivel8, lblNivel9, lblNivel10 };

            for (int i = 0; i < lblNiveles.Length; i++)
            {
                int nivel = i + 1;

                if (nivel <= usuario.UltimoNivel)
                {
                    // Nivel completado
                    lblNiveles[i].Image = (nivel >= 8)
                        ? Properties.Resources.estrella
                        : Properties.Resources.Estrella_plata;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                else if (nivel == usuario.UltimoNivel + 1)
                {
                    // Nivel jugable
                    lblNiveles[i].Image = Properties.Resources.Jugar;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                // else se queda con el candado por default
            }

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Modulo_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Nivel1 nivel = new Nivel1(UsuarioActual);
            this.Hide();
            nivel.Show();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion accederUI = new UI_InicioSesion();
            this.Hide();
            accederUI.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {    
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
            this.Hide();
        }
    }
}
