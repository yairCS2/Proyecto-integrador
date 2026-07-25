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

        public UI_MenuPrincipal(DatosUsuario usuario)
        {
            InitializeComponent();
            // se obtiene una frase motivadora aleatoria y se establece en el label correspondiente.
            lblFraseMotivadora.Text = ObtenerFraseAleatoria();
            // se establece todo a la medida del usuario.
            UsuarioActual = usuario;
            if (UsuarioActual.ReferenciaTipo == 1) gunaButton1.Visible = true;
            gunaButton8.Text = UsuarioActual.Username;
            lblPorcentajeNiveles.Text = $"{UsuarioActual.UltimoNivel * 2 ?? 0}%";
            lblNivelActual.Text = $"Haz completado {UsuarioActual.UltimoNivel ?? 0}/50 Niveles";
            lblBienvenida.Text = $"!Hola, {UsuarioActual.Username} Bienvenido!";
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

            UI_Rendimiento accederformRendimiento = new UI_Rendimiento();


            accederformRendimiento.Show();
            // hide me permite ocultar ell formulario
            // en cambio si le pongo close se cierra el formulario actual y termina el programa 
            this.Hide();

    
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            UI_InicioSesion accederUI = new UI_InicioSesion();

            this.Hide();
            accederUI.Show();
            
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogros_Click(object sender, EventArgs e)
        {
            UI_Logros accederformLogros = new UI_Logros();
            accederformLogros.Show();
            this.Hide();

        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);

            accedeerformAjustes.Show();
            this.Hide();
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
            Nivel1 nivel1 = new Nivel1();
            this.Hide();
            nivel1.Show();

        }

        private void gunaGroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Conexion C = new Conexion();
            C.verificarConecxion();
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            UI_Administrador u = new UI_Administrador(UsuarioActual);
            this.Hide();
            u.Show();
        }
    }
}
