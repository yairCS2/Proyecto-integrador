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
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            UsuarioActual = usuario;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            // UltimoNivel = niveles completados. Si es NULL nadie ha completado nada,
            // por eso se normaliza a 0 (antes: con NULL la barra se ponia al 100% y
            // el label se quedaba con el texto del diseñador "10 de 10").
            int completados = usuario.UltimoNivel ?? 0;

            // La barra muestra el progreso dentro del modulo (cada nivel completo = 10%).
            int completadosModulo = Math.Max(0, Math.Min(completados, 10));
            progressBar1.Value = completadosModulo * 10;
            lblNivelProgreso.Text = $"{completadosModulo} de 10 niveles completados";

            // Los 10 iconos de nivel del modulo.
            PictureBox[] lblNiveles = { lblNivel1, lblNivel2, lblNivel3, lblNivel4, lblNivel5,
                             lblNivel6, lblNivel7, lblNivel8, lblNivel9, lblNivel10 };

            for (int i = 0; i < lblNiveles.Length; i++)
            {
                int nivel = i + 1;

                if (nivel <= completados)
                {
                    // Nivel completado: se muestra una estrella y se puede volver a entrar.
                    lblNiveles[i].Image = (nivel >= 8)
                        ? Properties.Resources.estrella
                        : Properties.Resources.Estrella_plata;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                else if (nivel == completados + 1)
                {
                    // Nivel jugable: es el siguiente nivel, se muestra el icono "jugar".
                    lblNiveles[i].Image = Properties.Resources.Jugar;
                    lblNiveles[i].Cursor = Cursors.Hand;
                    lblNiveles[i].Enabled = true;
                }
                // else se queda con el candado por default (niveles bloqueados)

                // Los 10 niveles son clicables (antes solo el nivel 1 tenia evento
                // cableado en el diseñador: los demas niveles no hacian nada).
                // El nivel 1 se cablea en el Designer a pictureBox6_Click, por eso
                // solo se añade el manejador a los demas.
                if (nivel != 1)
                {
                    int nivelClicado = nivel;
                    lblNiveles[i].Click += (s, ev) => AbrirNivel(nivelClicado);
                }
            }

            // Fase 2: pantalla completa y estilo Duolingo.
            PrepararEstilo();
        }

        /// <summary>
        /// Escala el diseno (800x486) al tamano de la pantalla, lo centra y aplica
        /// el tema. La "X" de la esquina no sale de la aplicacion: vuelve al menu.
        /// </summary>
        private void PrepararEstilo()
        {
            // Titulos (faltaba el acento de "Modulo").
            lbusuario.Text = "Módulo 1";
            // OJO: tamaño explícito de 10 pt. Esta etiqueta vive en Y=10 con 17 px de
            // alto y label3 arranca en Y=27; con Tema.Titulo() sin tamaño la fuente
            // crecía a 14 pt (~24 px) y ambas se montaban una sobre otra.
            Tema.Titulo(lbusuario, 10F);
            Tema.Titulo(lblNivelProgreso, 12F);

            // Fondo blanco y cabecera gris clara (antes, el gris del sistema).
            panel3.BackColor = Tema.Blanco;
            panel4.BackColor = Tema.FondoGris;

            this.PerformLayout();

            // panel3 tiene Dock=Fill: se desancla, se agranda y se centra.
            Tema.Escalar(panel3, 1.7F);
            Tema.Aplicar(this, panel3, "DevyClass - Módulo 1",
                         centrar: true, accionCerrar: VolverAlMenu);

            Tema.BarraProgreso(progressBar1); // barra verde de progreso
        }

        /// <summary> La "X" regresa al menu principal (en vez de cerrar la aplicacion). </summary>
        private void VolverAlMenu()
        {
            UI_MenuPrincipal.AbrirMenu(UsuarioActual);
            this.Close();
        }

        // Abre el nivel elegido. Si esta bloqueado o aun no existe, se avisa en vez
        // de no hacer nada (antes los niveles 2-10 se ignoraban en silencio).
        private void AbrirNivel(int nivel)
        {
            int completados = UsuarioActual.UltimoNivel ?? 0;

            if (nivel > completados + 1)
            {
                MessageBox.Show(
                    $"El Nivel {nivel} está bloqueado. Termina primero los niveles anteriores.",
                    "Nivel bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (nivel != 1)
            {
                MessageBox.Show(
                    $"El Nivel {nivel} estará disponible próximamente. Por ahora solo existe el Nivel 1.",
                    "Próximamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Nivel1 n1 = new Nivel1(UsuarioActual);
            // Se cierra el selector: antes se ocultaba con Hide() y cada partida
            // dejaba una instancia de Modulo oculta (fuga de memoria). Primero se
            // abre el nivel y luego se cierra este, para no quedar sin ventanas.
            n1.Show();
            this.Close();
        }

        private void lbusuario_Click(object sender, EventArgs e)
        {

        }

        private void Modulo_Load(object sender, EventArgs e)
        {

        }

        // Click en el nivel 1: abre el formulario del Nivel 1.
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            AbrirNivel(1);
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
