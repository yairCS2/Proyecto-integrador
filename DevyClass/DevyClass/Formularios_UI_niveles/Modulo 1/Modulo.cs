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
    // Formulario del Modulo 1: muestra los 10 niveles del modulo como iconos
    // (estrella = completado, "jugar" = disponible, candado = bloqueado)
    // y permite entrar al nivel que le toque al usuario.
    public partial class Modulo : Form
    {
        private DatosUsuario UsuarioActual; // Usuario que esta viendo el modulo.
        public Modulo(DatosUsuario usuario)
        {
            // usuario.UltimoNivel % 10 : esta exprecion representa el nivel en que esta.
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            // inicializa el formulario y establece el usuario actual, la barra de progreso y el nivel de progreso según el último nivel del usuario.
            UsuarioActual = usuario;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            // La barra muestra el progreso dentro del modulo (cada nivel completo = 10%).
            if (usuario.UltimoNivel < 10)
            {
                progressBar1.Value = (usuario.UltimoNivel % 10) * 10 ?? 0;
                lblNivelProgreso.Text = $"{usuario.UltimoNivel % 10 ?? 0} de 10 niveles completados";
            }
            else progressBar1.Value = 100; // Si ya paso el modulo, la barra esta al 100%.


            // Los 10 iconos de nivel del modulo.
            PictureBox[] lblNiveles = { lblNivel1, lblNivel2, lblNivel3, lblNivel4, lblNivel5,
                             lblNivel6, lblNivel7, lblNivel8, lblNivel9, lblNivel10 };

            for (int i = 0; i < lblNiveles.Length; i++)
            {
                int nivel = i + 1;

                if (nivel <= usuario.UltimoNivel)
                {
                    // Nivel completado: se muestra una estrella y se puede volver a entrar.
                    lblNiveles[i].Image = (nivel >= 8)
                        ? Properties.Resources.estrella
                        : Properties.Resources.Estrella_plata;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                else if (nivel == usuario.UltimoNivel + 1)
                {
                    // Nivel jugable: es el siguiente nivel, se muestra el icono "jugar".
                    lblNiveles[i].Image = Properties.Resources.Jugar;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                // else se queda con el candado por default (niveles bloqueados)
            }

        }


        // Boton "Menu principal": regresa al menu.
        private void gunaButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            UI_MenuPrincipal.AbrirMenu(UsuarioActual);
        }

        // Boton "Ajustes": abre el formulario de ajustes.
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

        // Click en el nivel 1: abre el formulario del Nivel 1.
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Nivel1 nivel = new Nivel1(UsuarioActual);
            this.Hide();
            nivel.Show();
        }

        // Boton "Cerrar sesion": limpia los datos y vuelve al inicio.
        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion accederUI = new UI_InicioSesion();
            this.Close();
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

        // Boton "Ajustes" (secundario): abre el formulario de ajustes.
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
