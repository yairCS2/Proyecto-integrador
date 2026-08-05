using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.Formularios_UI_niveles.Modulo_1;
using Guna.UI.WinForms;
using DevyClass.UsuarioDB;
using Mysqlx.Notice;

namespace DevyClass
{

    public partial class UI_MenuPrincipal : Form
    {
        private DatosUsuario UsuarioActual;

        private static readonly string[] frases = new string[]
        {
            "Cada línea de código te acerca a tu meta.",
            "Hoy es un buen día para aprender algo nuevo.",
            "El progreso, aunque lento, sigue siendo progreso.",
            "Los errores de hoy son el aprendizaje de mañana.",
            "Tu esfuerzo de hoy es tu éxito de mañana.",
            "No se trata de ser el mejor, sino de ser mejor que ayer.",
            "Cada reto es una oportunidad para crecer."
        };

        private static readonly Random rnd = new Random();

        public static string ObtenerFraseAleatoria()
        {
            int indice = rnd.Next(frases.Length);
            return frases[indice];
        }

        private Type[] Niveles =
            {
                typeof(Nivel1)
            };

        public UI_MenuPrincipal(DatosUsuario usuario)
        {
            InitializeComponent();
            // se obtiene una frase motivadora aleatoria y se establece en el label correspondiente.
            lblFraseMotivadora.Text = ObtenerFraseAleatoria();

            // se establece todo a la medida del usuario.

            // se asigna el usuario actual al formulario
            UsuarioActual = usuario;
            // Se configura la barra de progreso y los labels según el último nivel del usuario.
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = usuario.UltimoNivel * 2 ?? 0;
            // se verifica si el usuario es un administrador (ReferenciaTipo == 1) y se muestra el botón correspondiente si es así.
            if (UsuarioActual.ReferenciaTipo == 1) gunaButton1.Visible = true;
            // se muestra el nombre de usuario en el botón correspondiente.
            gunaButton8.Text = UsuarioActual.Username;
            // se muestra el porcentaje de niveles completados por el usuario.
            lblPorcentajeNiveles.Text = $"{UsuarioActual.UltimoNivel * 2 ?? 0}%";
            // se muestra el progreso del usuario en términos de niveles completados.
            lblNivelActual.Text = $"Haz completado {UsuarioActual.UltimoNivel ?? 0}/50 Niveles";
            // Bienvenida personalizada con el nombre de usuario.
            lblBienvenida.Text = $"!Hola, {UsuarioActual.Username} Bienvenido!";
            // se muestra la experiencia acumulada del usuario.
            lblExperiencia.Text = $"{(UsuarioActual.UltimoNivel) * 20 ?? 0} XP";

            // se configura la información de los módulos según el último nivel del usuario.
            if (usuario.UltimoNivel <= 10)
            {
                if(usuario.UltimoNivel != 10)
                {
                    lblModulo1Porcentaje.Text = $"{(usuario.UltimoNivel % 10) * 10 ?? 0}%";
                    lblModulo1NivelActual.Text = $"{usuario.UltimoNivel % 10 ?? 0}/10 Niveles";
                }
                lblModuloActual.Text = $"Pensamiento\nalgoritmico";
            }
            // Modulo 2
            if (usuario.UltimoNivel > 10 && usuario.UltimoNivel <= 20)
            {
                lblModulo2Porcentaje.Text = $"{(usuario.UltimoNivel % 10) * 10 ?? 0}%";
                lblModulo2NivelActual.Text = $"{usuario.UltimoNivel % 10 ?? 0}/10 Niveles";
            }
            else
            {
                lblModulo2Porcentaje.Text = $"0%";
                lblModulo2NivelActual.Text = $"0/10 Niveles";
            }

            // Modulo 3
            if (usuario.UltimoNivel > 20 && usuario.UltimoNivel <= 30)
            {
                lblModulo3Porcentaje.Text = $"0%";
                lblModulo3NivelActual.Text = $"0/10 Niveles";
            }
            else
            {
                lblModulo3Porcentaje.Text = $"0%";
                lblModulo3NivelActual.Text = $"0/10 Niveles";
            }

            // Modulo 4
            if (usuario.UltimoNivel > 30 && usuario.UltimoNivel <= 40)
            {

                lblModulo4Porcentaje.Text = $"0%";
                lblModulo4NivelActual.Text = $"0/10 Niveles";
            }
            else
            {
                lblModulo4Porcentaje.Text = $"0%";
                lblModulo4NivelActual.Text = $"0/10 Niveles";
            }

        }

        public UI_MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Console.WriteLine("Probando cosas...");

            //cambios
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


    
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //transicionMenu.Show(panel1);

            //if (menuExpandido)
            //{
            //    panel1.Width = 50;
            //    menuExpandido = false;
            //}
            //else
            //{
            //    panel1.Width = 200;
            //    menuExpandido = true;
            //}
        }

        private void btnTemario_Click(object sender, EventArgs e)
        {

        }


        private void btnRendimiento_Click(object sender, EventArgs e)
        {
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            UI_InicioSesion accederUI = new UI_InicioSesion();
            this.Close();
            accederUI.Show();
            UsuarioActual.BorrarDatos();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogros_Click(object sender, EventArgs e)
        {

        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {

        } 
        
        private void paP3_MouseLeave(object sender, EventArgs e)
        {
    



        }

        private void paP3_MouseEnter(object sender, EventArgs e)
        {


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnContinuarP1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
           
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {

        }

        private void gunaImageButton1_Click(object sender, EventArgs e)
        {
            Modulo nivel1 = new Modulo(UsuarioActual);
            nivel1.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void gunaGroupBox1_Click(object sender, EventArgs e)
        {
            Modulo nivel1 = new Modulo(UsuarioActual);
            nivel1.Show();

        }

        private void gunaGroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            int indice = UsuarioActual.UltimoNivel ?? 0;

            if (indice < 0 || indice >= Niveles.Length)
            {
                MessageBox.Show("No hay más niveles disponibles.");
                return;
            }

            Form pantallaNivel = (Form)Activator.CreateInstance(Niveles[indice]);
            pantallaNivel.Show();
            this.Hide();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Conexion C = new Conexion();
            C.verificarConecxion();
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            UI_Administrador u = new UI_Administrador(UsuarioActual);
            u.Show();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
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
