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
    // Formulario del Nivel 1. Contiene un panel donde se van mostrando
    // las preguntas (pregunta1, Pregunta2, Pregunta3) y al final la pantalla
    // de victoria (Ganaste). El usuario puede avanzar y regresar entre preguntas.
    public partial class Nivel1 : Form
    {
        private DatosUsuario UsuarioActual; // Usuario que esta jugando el nivel.
        private Func<UserControl>[] preguntas; // Lista de pantallas del nivel (se crean bajo demanda).
        private int indiceActual = 0; // Indica en que pantalla esta el usuario.
        Nivel1RepuestasCorrectas UsuarioPreguntas = new Nivel1RepuestasCorrectas(); // Estado compartido del nivel.

        public Nivel1(DatosUsuario usuario)
        {
            this.StartPosition = FormStartPosition.CenterScreen; // La ventana se centra en la pantalla.
            InitializeComponent();
            UsuarioActual = usuario;
            // Evita que los botones "siguiente" y "atras" capturen el foco con el teclado.
            gunaButton1.TabStop = false;
            gunaButton2.TabStop = false;

            // Se registran las pantallas del nivel en orden:
            // 0 = pregunta1, 1 = Pregunta2, 2 = Pregunta3, 3 = Ganaste (pantalla final).
            preguntas = new Func<UserControl>[]
            {
                    () => new pregunta1(UsuarioPreguntas),
                    () => new Pregunta2(),
                    () => new Pregunta3(),
                    () => new Ganaste()
            };

            // Muestra la primera pantalla (la pregunta 1).
            CambiarUC(preguntas[indiceActual]());
        }

        // Cambia la pantalla dentro del panel: limpia el panel y agrega el UserControl nuevo.
        public void CambiarUC(UserControl nuevoUC)
        {
            panelPregunta.Controls.Clear();
            nuevoUC.Dock = DockStyle.Fill; // El control nuevo ocupa todo el panel.
            panelPregunta.Controls.Add(nuevoUC);
        }


        private void Nivel1_Load(object sender, EventArgs e)
        {

        }

        // Boton "Atras": regresa a la pregunta anterior.
        private void gunaButton1_Click(object sender, EventArgs e)
        {

            if (indiceActual > 0) // No deja retroceder si ya esta en la primera pantalla.
            {
                indiceActual--;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso -= 25; // Reduce el progreso en 25%.
            }
        }

        // Boton "Siguiente": avanza a la siguiente pantalla.
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (indiceActual < preguntas.Length - 1) // No avanza si ya esta en la ultima.
            {

                indiceActual++;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso += 25; // Aumenta el progreso en 25%.
            }
            if (indiceActual == 3) gunaButton5.Visible = true; // Al llegar a "Ganaste", muestra el boton de guardar.
        }

        // Boton "Guardar progreso": guarda en la BD que el nivel 1 fue completado.
        private void gunaButton5_Click(object sender, EventArgs e)
        {
            UsuarioActual.UltimoNivel = 1; // Marca el ultimo nivel alcanzado como 1.
            ConsultasUsuario consultas = new ConsultasUsuario();
            consultas.ActualizarUsuario(UsuarioActual); // Guarda el cambio en la base de datos.
            this.Hide();
            UI_MenuPrincipal.AbrirMenu(UsuarioActual); // Reutiliza el menu abierto y muestra el progreso actualizado.
        }

        // Boton "Menu principal": regresa al menu.
        private void gunaButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            UI_MenuPrincipal.AbrirMenu(UsuarioActual);
        }

        // Boton "Cerrar sesion": limpia los datos y vuelve al inicio.
        private void gunaButton7_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos();
            UI_InicioSesion iniciar = new UI_InicioSesion();
            this.Close();
            iniciar.Show();
        }

        // Boton "Ajustes": abre el formulario de ajustes.
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
