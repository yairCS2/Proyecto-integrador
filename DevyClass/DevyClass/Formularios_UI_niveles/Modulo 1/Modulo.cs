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

            gunaButton8.Text = UsuarioActual.Username;

            if (usuario.UltimoNivel >= 1)
            {
                // Nivel desbloqueado
                lblNivel1.Image = Properties.Resources.Jugar;
                lblNivel1.Cursor = Cursors.Hand;   // manita, como link
                lblNivel1.Enabled = true;
            }
            if (usuario.UltimoNivel >= 2)
            {
                // Nivel desbloqueado
                lblNivel2.Image = Properties.Resources.Jugar;
                lblNivel2.Cursor = Cursors.Hand;   // manita, como link
                lblNivel2.Enabled = true;
            }
            if (usuario.UltimoNivel >= 3)
            {
                // Nivel desbloqueado
                lblNivel3.Image = Properties.Resources.Jugar;
                lblNivel3.Cursor = Cursors.Hand;   // manita, como link
                lblNivel3.Enabled = true;
            }
            if (usuario.UltimoNivel >= 4)
            {
                // Nivel desbloqueado
                lblNivel4.Image = Properties.Resources.Jugar;
                lblNivel4.Cursor = Cursors.Hand;   // manita, como link
                lblNivel4.Enabled = true;
            }
            if (usuario.UltimoNivel >= 5)
            {
                // Nivel desbloqueado
                lblNivel5.Image = Properties.Resources.Jugar;
                lblNivel5.Cursor = Cursors.Hand;   // manita, como link
                lblNivel5.Enabled = true;
            }
            if (usuario.UltimoNivel >= 6)
            {
                // Nivel desbloqueado
                lblNivel6.Image = Properties.Resources.Jugar;
                lblNivel6.Cursor = Cursors.Hand;   // manita, como link
                lblNivel6.Enabled = true;
            }
            if (usuario.UltimoNivel >= 7)
            {
                // Nivel desbloqueado
                lblNivel7.Image = Properties.Resources.Jugar;
                lblNivel7.Cursor = Cursors.Hand;   // manita, como link
                lblNivel7.Enabled = true;
            }
            if (usuario.UltimoNivel >= 8)
            {
                // Nivel desbloqueado
                lblNivel8.Image = Properties.Resources.estrella;
                lblNivel8.Cursor = Cursors.Hand;   // manita, como link
                lblNivel8.Enabled = true;
            }
            if (usuario.UltimoNivel >= 9)
            {
                // Nivel desbloqueado
                lblNivel9.Image = Properties.Resources.estrella;
                lblNivel9.Cursor = Cursors.Hand;   // manita, como link
                lblNivel9.Enabled = true;
            }
            if (usuario.UltimoNivel >= 10)
            {
                // Nivel desbloqueado
                lblNivel10.Image = Properties.Resources.estrella;
                lblNivel10.Cursor = Cursors.Hand;   // manita, como link
                lblNivel10.Enabled = true;
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
            Nivel1 nivel = new Nivel1();
            this.Hide();
            nivel.Show();
        }
    }
}
