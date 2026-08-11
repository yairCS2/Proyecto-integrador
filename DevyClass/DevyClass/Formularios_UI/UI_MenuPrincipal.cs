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

    // Formulario principal de la aplicacion (el "home").
    // Muestra el progreso del usuario, los modulos disponibles, botones de ajustes,
    // cerrar sesion y el acceso al selector de niveles. Tambien permite entrar
    // al panel de administrador si el usuario es admin.
    public partial class UI_MenuPrincipal : Form
    {
        private DatosUsuario UsuarioActual; // Usuario que entro a la sesion.

        // Frases motivadoras que se muestran al azar en el menu principal.
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

        // Devuelve una frase motivadora elegida al azar.
        public static string ObtenerFraseAleatoria()
        {
            int indice = rnd.Next(frases.Length);
            return frases[indice];
        }

        // Lista de niveles disponibles. Por ahora solo existe el Nivel 1.
        private Type[] Niveles =
            {
                typeof(Nivel1)
            };

        // Constructor principal: recibe el usuario que inicio sesion y configura toda la pantalla.
        public UI_MenuPrincipal(DatosUsuario usuario)
        {
            InitializeComponent();
            UsuarioActual = usuario;
            RefrescarUI();
        }

        // Refresca toda la informacion del menu con los datos del usuario actual.
        // Se usa al volver de un nivel para mostrar el progreso actualizado.
        public void RefrescarUI()
        {
            // se obtiene una frase motivadora aleatoria y se establece en el label correspondiente.
            lblFraseMotivadora.Text = ObtenerFraseAleatoria();

            // Se configura la barra de progreso y los labels según el último nivel del usuario.
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            // El progreso se calcula como nivel * 2 (el maximo de niveles es 50 -> 100%).
            progressBar1.Value = UsuarioActual.UltimoNivel * 2 ?? 0;
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
            // se muestra la experiencia acumulada del usuario (cada nivel vale 20 XP).
            lblExperiencia.Text = $"{(UsuarioActual.UltimoNivel) * 20 ?? 0} XP";

            // se configura la información de los módulos según el último nivel del usuario.
            // Modulo 1 (niveles 1-10): "Pensamiento algoritmico".
            if (UsuarioActual.UltimoNivel <= 10)
            {
                if (UsuarioActual.UltimoNivel != 10)
                {
                    lblModulo1Porcentaje.Text = $"{(UsuarioActual.UltimoNivel % 10) * 10 ?? 0}%";
                    lblModulo1NivelActual.Text = $"{UsuarioActual.UltimoNivel % 10 ?? 0}/10 Niveles";
                }
                lblModuloActual.Text = $"Pensamiento\nalgoritmico";
            }
            // Modulo 2 (niveles 11-20).
            if (UsuarioActual.UltimoNivel > 10 && UsuarioActual.UltimoNivel <= 20)
            {
                lblModulo2Porcentaje.Text = $"{(UsuarioActual.UltimoNivel % 10) * 10 ?? 0}%";
                lblModulo2NivelActual.Text = $"{UsuarioActual.UltimoNivel % 10 ?? 0}/10 Niveles";
            }
            else
            {
                // Si aun no llega al modulo 2, se muestra 0%.
                lblModulo2Porcentaje.Text = $"0%";
                lblModulo2NivelActual.Text = $"0/10 Niveles";
            }

            // Modulo 3 (niveles 21-30). Por ahora siempre muestra 0%.
            if (UsuarioActual.UltimoNivel > 20 && UsuarioActual.UltimoNivel <= 30)
            {
                lblModulo3Porcentaje.Text = $"0%";
                lblModulo3NivelActual.Text = $"0/10 Niveles";
            }
            else
            {
                lblModulo3Porcentaje.Text = $"0%";
                lblModulo3NivelActual.Text = $"0/10 Niveles";
            }

            // Modulo 4 (niveles 31-40). Por ahora siempre muestra 0%.
            if (UsuarioActual.UltimoNivel > 30 && UsuarioActual.UltimoNivel <= 40)
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

        // Reutiliza el menu principal abierto (si existe) y lo actualiza con el usuario actual.
        // Si no hay ninguno abierto, crea uno nuevo. Evita que se acumulen menus principales.
        public static UI_MenuPrincipal AbrirMenu(DatosUsuario usuario)
        {
            UI_MenuPrincipal menu = Application.OpenForms.OfType<UI_MenuPrincipal>().FirstOrDefault();

            if (menu == null)
            {
                menu = new UI_MenuPrincipal(usuario);
            }
            else
            {
                menu.UsuarioActual = usuario;
                menu.RefrescarUI();
            }

            menu.Show();
            menu.BringToFront();
            return menu;
        }

        // Constructor vacio (sin datos de usuario). Se usa solo en algunos casos de prueba.
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

        // Boton "Regresar": cierra la sesion y vuelve al inicio.
        private void btnregresar_Click(object sender, EventArgs e)
        {
            UI_InicioSesion accederUI = new UI_InicioSesion();
            this.Close();
            accederUI.Show();
            UsuarioActual.BorrarDatos(); // Limpia los datos del usuario en memoria.
        }


        // Boton "Salir": cierra la aplicacion.
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

        // Click en la imagen del modulo: abre el formulario Modulo (selector de niveles).
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

        // Click en el grupo del modulo 1: abre el selector de niveles.
        private void gunaGroupBox1_Click(object sender, EventArgs e)
        {
            Modulo nivel1 = new Modulo(UsuarioActual);
            nivel1.Show();

        }

        private void gunaGroupBox4_Click(object sender, EventArgs e)
        {

        }

        // Boton "Continuar": abre el nivel que le corresponde al usuario segun su ultimo nivel.
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            int indice = UsuarioActual.UltimoNivel ?? 0;

            // Verifica que el indice este dentro de la lista de niveles disponibles.
            if (indice < 0 || indice >= Niveles.Length)
            {
                MessageBox.Show("No hay más niveles disponibles.");
                return;
            }

            if (UsuarioActual == null)
            {
                MessageBox.Show("Usuario no inicializado.");
                return;
            }

            try
            {
                // Crea dinamicamente la pantalla del nivel (por ahora solo Nivel1) usando Activator.
                // Pasar UsuarioActual al constructor (Nivel1(DatosUsuario))
                var pantallaNivel = Activator.CreateInstance(Niveles[indice], UsuarioActual) as Form;
                if (pantallaNivel == null)
                {
                    MessageBox.Show("No se pudo crear la pantalla solicitada.");
                    return;
                }

                pantallaNivel.Show();
            }
            catch (MissingMethodException mex)
            {
                // Error si el nivel no tiene el constructor esperado (que reciba DatosUsuario).
                MessageBox.Show("Constructor esperado no encontrado: " + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la pantalla: " + ex.Message);
            }
        }

        // Boton del administrador: prueba la conexion a la base de datos.
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Conexion C = new Conexion();
            C.verificarConecxion();
        }

        // Boton del administrador: abre el panel de administrador.
        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            UI_Administrador u = new UI_Administrador(UsuarioActual);
            u.Show();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
        }

        // Boton del usuario: abre los ajustes (editar nombre de usuario y contrasena).
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
        }

        // Boton "Cerrar sesion": limpia los datos del usuario y vuelve al inicio de sesion.
        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion iniciar = new UI_InicioSesion();
            this.Close();
            iniciar.Show();
        }
    }
}
