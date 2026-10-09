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

            // Fase 2: pantalla completa y estilo Duolingo.
            PrepararEstilo();

            RefrescarUI();
        }

        /// <summary>
        /// Aplica el tema una sola vez al abrir el menu: agranda el diseno original
        /// (1364x692) al tamano de la pantalla, lo centra y pinta botones/colores.
        /// </summary>
        private void PrepararEstilo()
        {

            // Colores: la barra lateral era azul CornflowerBlue y el contenido gris
            // del sistema. Ahora: barra lateral blanca, cabecera blanca y contenido
            // gris muy claro, para que las tarjetas y botones blancos resalten.
            panel1.BackColor = Tema.Blanco;
            panel2.BackColor = Tema.FondoGris;
            panel4.BackColor = Tema.Blanco;

            this.PerformLayout();

            // El diseno original es pequeño para un monitor grande.
            Tema.EscalarHijos(this, 1.3F);

            // panel1 (barra lateral) y panel2 (contenido) se mueven juntos con la misma
            // distancia, asi el bloque queda centrado sin descuadrar nada.
            Tema.Aplicar(this, null, "DevyClass - Menú principal");

            Tema.Boton(gunaButton3, Tema.Rol.Primario);            // Continuar
            Tema.Boton(gunaButton6, Tema.Rol.Fantasma);            // Inicio
            Tema.Boton(gunaButton8, Tema.Rol.Fantasma);            // usuario
            Tema.Boton(gunaButton7, Tema.Rol.ContornoPeligroso);   // Cerrar sesión
            Tema.Boton(gunaButton1, Tema.Rol.Secundario);          // Opciones de Admin              // ✕ de la ventana

            // Tarjeta de progreso: blanca con borde redondeado. Se hace al final
            // para que la esquina redondeada se calcule con el tamano ya escalado.
            Tema.Tarjeta(panel3, Tema.Blanco, Tema.Borde);

            Tema.BarraProgreso(progressBar1); // barra verde de niveles (reemplaza la nativa)
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
            // UltimoNivel = niveles completados (NULL = 0). Se limita a 100 porque
            // Value fuera de rango lanza ArgumentOutOfRangeException.
            int completados = UsuarioActual.UltimoNivel ?? 0;
            progressBar1.Value = Math.Min(100, completados * 2);
            // Actualiza la barra verde visible (la ProgressBar nativa quedo oculta).
            Tema.BarraProgreso(progressBar1);
            // se verifica si el usuario es un administrador (ReferenciaTipo == 1) y se muestra el botón correspondiente si es así.
            if (UsuarioActual.ReferenciaTipo == 1) gunaButton1.Visible = true;
            // se muestra el nombre de usuario en el botón correspondiente.
            gunaButton8.Text = UsuarioActual.Username;
            // se muestra el porcentaje de niveles completados por el usuario.
            lblPorcentajeNiveles.Text = $"{Math.Min(100, completados * 2)}%";
            // se muestra el progreso del usuario en términos de niveles completados.
            lblNivelActual.Text = $"Haz completado {completados}/50 Niveles";
            // Bienvenida personalizada con el nombre de usuario.
            lblBienvenida.Text = $"¡Hola, {UsuarioActual.Username} Bienvenido!";
            // se muestra la experiencia acumulada del usuario (cada nivel vale 20 XP).
            lblExperiencia.Text = $"{completados * 20} XP";

            // Modulo 1 (niveles 1-10): "Pensamiento algoritmico".
            // Antes: si el usuario tenia exactamente 10 niveles no se actualizaba nada
            // (los labels se quedaban con el texto del diseñador) y el resto de modulos
            // usaba un if/else con las dos ramas iguales (codigo muerto).
            int m1 = Math.Max(0, Math.Min(completados, 10));
            lblModulo1Porcentaje.Text = $"{m1 * 10}%";
            lblModulo1NivelActual.Text = $"{m1}/10 Niveles";
            if (completados <= 10) lblModuloActual.Text = "Pensamiento\\nalgorítmico";

            // Modulo 2 (niveles 11-20): se calcula igual, con lo que no llegue se queda en 0.
            int m2 = Math.Max(0, Math.Min(completados - 10, 10));
            lblModulo2Porcentaje.Text = $"{m2 * 10}%";
            lblModulo2NivelActual.Text = $"{m2}/10 Niveles";

            // Modulo 3 (niveles 21-30) y Modulo 4 (niveles 31-40): aun no tienen contenido.
            lblModulo3Porcentaje.Text = "0%";
            lblModulo3NivelActual.Text = "0/10 Niveles";
            lblModulo4Porcentaje.Text = "0%";
            lblModulo4NivelActual.Text = "0/10 Niveles";
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
            accederUI.Show();   // primero se muestra la nueva ventana...
            this.Close();       // ...y despues se cierra esta.
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
            this.Hide(); // el menu no debe asomar detras de la ventana centrada
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
            this.Hide(); // el menu no debe asomar detras de la ventana centrada
        }

        private void gunaGroupBox4_Click(object sender, EventArgs e)
        {

        }

        // Boton "Continuar": abre el nivel que le corresponde al usuario segun su ultimo nivel.
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (UsuarioActual == null)
            {
                MessageBox.Show("Usuario no inicializado.");
                return;
            }

            // UltimoNivel = niveles completados (NULL = 0), asi que el nivel que le toca
            // jugar es el siguiente. Antes se usaba UltimoNivel directo y, como el
            // registro guardaba 1, el boton siempre decia "No hay mas niveles".
            int indice = UsuarioActual.UltimoNivel ?? 0;

            // Verifica que el indice este dentro de la lista de niveles disponibles.
            if (indice < 0 || indice >= Niveles.Length)
            {
                MessageBox.Show("Has completado todos los niveles disponibles por ahora.",
                    "Niveles", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                this.Hide(); // el menu no debe asomar detras de la ventana centrada
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
            this.Hide(); // el menu no debe asomar detras de la ventana
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
        }

        // Boton del usuario: abre los ajustes (editar nombre de usuario y contrasena).
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UI_Ajustes accedeerformAjustes = new UI_Ajustes(UsuarioActual);
            accedeerformAjustes.Show();
            this.Hide(); // el menu no debe asomar detras de la ventana centrada
        }

        // Boton "Cerrar sesion": limpia los datos del usuario y vuelve al inicio de sesion.
        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion iniciar = new UI_InicioSesion();
            iniciar.Show();     // primero se muestra la nueva ventana...
            this.Close();       // ...y despues se cierra esta.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
