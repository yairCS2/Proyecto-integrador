using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.Formularios_UI_niveles.Modulo_1;
using DevyClass.UsuarioDB;
using Guna.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DevyClass.Formularios_UI_niveles.Modulo_1
{
    public partial class Nivel1 : Form
    {
        private DatosUsuario UsuarioActual;
        private Func<UserControl>[] preguntas;
        private int indiceActual = 0;
        Nivel1RepuestasCorrectas UsuarioPreguntas = new Nivel1RepuestasCorrectas();

        public Nivel1(DatosUsuario usuario)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            UsuarioActual = usuario;
            gunaButton1.TabStop = false;
            gunaButton2.TabStop = false;

            preguntas = new Func<UserControl>[]
            {
                    () => new pregunta1(UsuarioPreguntas),
                    () => new Pregunta2(),
                    () => new Pregunta3(),
                    () => new Ganaste()
            };

            CambiarUC(preguntas[indiceActual]());
        }

        public void CambiarUC(UserControl nuevoUC)
        {
            panelPregunta.Controls.Clear();
            nuevoUC.Dock = DockStyle.Fill;
            panelPregunta.Controls.Add(nuevoUC);
        }


        private void Nivel1_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {

            if (indiceActual > 0)
            {
                indiceActual--;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso -= 25;
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (indiceActual < preguntas.Length - 1)
            {

                indiceActual++;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso += 25;
            }
            if (indiceActual == 3) gunaButton5.Visible = true;
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            UsuarioActual.UltimoNivel = 1;
            ConsultasUsuario consultas = new ConsultasUsuario();
            consultas.ActualizarUsuario(UsuarioActual);
            this.Hide();
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

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
