using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.Formularios_UI_niveles.Modulo_1;
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
            // Las cuatro reciben el estado para que se pueda puntuar el nivel.
            preguntas = new Func<UserControl>[]
            {
                    () => new pregunta1(UsuarioPreguntas),
                    () => new Pregunta2(UsuarioPreguntas),
                    () => new Pregunta3(),
                    () => new Ganaste(UsuarioPreguntas)
            };

            // Fase 2: pantalla completa y estilo Duolingo.
            // OJO: el orden importa. PrepararEstilo() escala "panel3" con 1.7, y cada
            // UserControl de pregunta se escala a si mismo con 1.7 en su constructor.
            // Si la primera pregunta se cargara ANTES de esta llamada, su contenido
            // recibiria las dos escalas (1.7 x 1.7 = 2.89): la pregunta se veia
            // gigante y sus textos se salian del panel, mientras que Pregunta2,
            // Pregunta3 y Ganaste (que se crean despues, al pulsar "Siguiente")
            // salian normales. Escalando primero el marco y cargando despues el
            // contenido, todas las pantallas quedan escaladas igual.
            PrepararEstilo();

            // Muestra la primera pantalla (la pregunta 1).
            CambiarUC(preguntas[indiceActual]());
        }

        /// <summary>
        /// Escala el diseno (800x489) al tamano de la pantalla, lo centra y aplica
        /// el tema. La "X" abandona el nivel y vuelve al menu.
        /// </summary>
        private void PrepararEstilo()
        {
            lbusuario.Text = "Módulo 1";
            // OJO: el tamaño importa. Esta etiqueta se dibuja en Y=21 con 17 px de
            // alto, justo encima de label3 (Y=38). Con Tema.Titulo() sin tamaño la
            // fuente crecia a 14 pt (~24 px) y ambas se montaban una sobre otra.
            // A 10 pt cabe en su caja y deja intacto el titulo de abajo.
            Tema.Titulo(lbusuario, 10F);
            Tema.Titulo(label3, 15F);

            // Fondo blanco y cabecera gris clara (antes, el gris del sistema).
            panel3.BackColor = Tema.Blanco;
            panel4.BackColor = Tema.FondoGris;

            this.PerformLayout();

            // panel3 tiene Dock=Top: se desancla, se agranda y se centra.
            Tema.Escalar(panel3, 1.7F);
            Tema.Aplicar(this, panel3, "DevyClass - Nivel 1",
                         centrar: true, accionCerrar: VolverAlMenu);

            Tema.Boton(gunaButton1, Tema.Rol.Secundario);   // ◀ Anterior
            Tema.Boton(gunaButton2, Tema.Rol.Primario);     // Siguiente ▶
            Tema.Boton(gunaButton5, Tema.Rol.Amarillo);     // Finalizar (aparece al final)
        }

        /// <summary> La "X" abandona el nivel y regresa al menu principal. </summary>
        private void VolverAlMenu()
        {
            UI_MenuPrincipal.AbrirMenu(UsuarioActual);
            this.Close();
        }

        // Cambia la pantalla dentro del panel: limpia el panel y agrega el UserControl nuevo.
        public void CambiarUC(UserControl nuevoUC)
        {
            // Se disponen los controles anteriores (Controls.Clear() no los libera:
            // con imagenes de fondo se acumulaban objetos no gestionados).
            foreach (Control control in panelPregunta.Controls)
            {
                control.Dispose();
            }
            panelPregunta.Controls.Clear();

            nuevoUC.Dock = DockStyle.Fill; // El control nuevo ocupa todo el panel.

            // Antes de insertar: se suelta el anclaje de los hijos. Los botones vienen
            // con Anchor=Bottom|Right y, al aplicar Dock=Fill, WinForms agranda el
            // control de 585x285 a 994x484 moviendo los hijos anclados por el delta.
            Tema.AnclarArribaIzquierda(nuevoUC);

            panelPregunta.Controls.Add(nuevoUC);

            // Escalado DESPUES de insertar, con el panel ya en su tamaño definitivo:
            // asi cada pantalla (pregunta1, Pregunta2, Pregunta3 y Ganaste) queda
            // escalada exactamente una vez y en su sitio. Escalar dentro del
            // constructor del UserControl lo hacia antes de que existiera el panel y
            // la pregunta 1 acabava con doble escala (1.7 x 1.7 = 2.89).
            Tema.EscalarHijos(nuevoUC, 1.7F);
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
            }

            // "Finalizar" solo tiene sentido en la ultima pantalla (antes quedaba
            // visible al retroceder desde "Ganaste").
            gunaButton5.Visible = (indiceActual == preguntas.Length - 1);
        }

        // Boton "Siguiente": avanza a la siguiente pantalla.
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (indiceActual >= preguntas.Length - 1) return; // Ya esta en la ultima pantalla.

            // No se avanza sin responder: antes se podia terminar el nivel sin contestar nada.
            if (!RespuestaActualLista()) return;

            indiceActual++;
            CambiarUC(preguntas[indiceActual]());

            // Al llegar a "Ganaste" se muestra el boton de guardar.
            gunaButton5.Visible = (indiceActual == preguntas.Length - 1);
        }

        // Comprueba que la pantalla actual tenga respuesta y, si es la pregunta 3,
        // guarda el resultado de la ordenacion en el estado compartido.
        private bool RespuestaActualLista()
        {
            switch (indiceActual)
            {
                case 0: // Pregunta 1
                    if (UsuarioPreguntas.Pregunta1Res == 0)
                    {
                        MessageBox.Show("Selecciona una opcion para continuar.",
                            "Pregunta sin responder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    return true;

                case 1: // Pregunta 2 (verdadero o falso)
                    if (UsuarioPreguntas.Pregunta2Res == 0)
                    {
                        MessageBox.Show("Responde si es verdadero o falso para continuar.",
                            "Pregunta sin responder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    return true;

                case 2: // Pregunta 3 (ordenar bloques)
                    Pregunta3 p3 = panelPregunta.Controls.OfType<Pregunta3>().FirstOrDefault();
                    if (p3 == null || !p3.OrdenValido())
                    {
                        MessageBox.Show("Asigna un numero distinto a cada bloque (1, 2 y 3).",
                            "Pregunta sin responder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    // 1 = orden correcto, 2 = orden incorrecto.
                    UsuarioPreguntas.Pregunta3Res = p3.OrdenCorrecto() ? 1 : 2;
                    return true;

                default: // "Ganaste" no requiere respuesta.
                    return true;
            }
        }

        // Boton "Guardar progreso": guarda en la BD que el nivel 1 fue completado.
        private void gunaButton5_Click(object sender, EventArgs e)
        {
            // UltimoNivel = niveles completados. Se guarda el maximo entre lo que tenia
            // y 1: antes se ponia 1 siempre, con lo que un usuario en el nivel 5
            // perdia todo su avance al repetir el nivel 1.
            UsuarioActual.UltimoNivel = Math.Max(UsuarioActual.UltimoNivel ?? 0, 1);

            try
            {
                ConsultasUsuario consultas = new ConsultasUsuario();
                consultas.ActualizarUsuario(UsuarioActual); // Guarda el cambio en la base de datos.
            }
            catch (Exception)
            {
                // Si la BD falla no se cierra el nivel, para no perder el avance en pantalla.
                MessageBox.Show("No se pudo guardar el progreso en la base de datos. Inténtalo de nuevo.",
                    "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Se cierra el nivel (antes se ocultaba con Hide() y cada partida dejaba
            // una instancia oculta acumulandose). Primero se reabre el menu y luego
            // se cierra este formulario.
            UI_MenuPrincipal.AbrirMenu(UsuarioActual); // Reutiliza el menu abierto y muestra el progreso actualizado.
            this.Close();
        }
    }
}
