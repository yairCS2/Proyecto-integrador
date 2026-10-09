using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevyClass.Formularios_UI_niveles.Modulo_1;

namespace DevyClass.Formularios_UI_niveles.Modulo_1
{
    // Pantalla de la pregunta 1 del nivel 1 (un UserControl que se muestra dentro del panel de Nivel1).
    // El usuario elige una de las opciones y esa eleccion se guarda en Nivel1RepuestasCorrectas.
    public partial class pregunta1 : UserControl
    {
        // Colores antiguos de las opciones (ahora los maneja Tema.Opcion /
        // Tema.OpcionElegida: tarjeta blanca y seleccion en azul claro).

        private Nivel1RepuestasCorrectas UsuarioPregunta; // Estado compartido del nivel.

        public pregunta1(Nivel1RepuestasCorrectas usuario)
        {
            InitializeComponent();
            UsuarioPregunta = usuario;

            // Fase 2: estilo Duolingo (mismo escalado que Nivel1 para llenar el panel).
            PrepararEstilo();

            // Si el usuario ya habia elegido una opcion antes, se resalta para que se vea seleccionada.
            PintarSeleccion();
        }

        /// <summary>
        /// Corrige los acentos, agranda el diseno al tamano del panel de Nivel1
        /// y convierte las tres respuestas en tarjetas tipo Duolingo.
        /// </summary>
        private void PrepararEstilo()
        {
            label2.Text = "Ejercicio 1 · Opción múltiple";
            label1.Text = "¿Cuál de estas opciones describe mejor un algoritmo?";
            this.BackColor = Tema.Blanco;

            Tema.Titulo(label1, 13F);   // la pregunta
            Tema.Titulo(label2, 11F);   // el rotulo del ejercicio

            this.PerformLayout();

            // OJO: aqui NO se escala. El escalado lo hace Nivel1.CambiarUC() despues
            // de insertar este control en panelPregunta, porque los botones tienen
            // Anchor=Bottom|Right y WinForms los desplaza al aplicar Dock=Fill.
            // Escalar aqui y alla duplicaria el factor.
            Tema.EstilizarRaiz(this);

            Tema.Opcion(gunaButton2);   // "Un tipo de dato"
            Tema.Opcion(gunaButton3);   // "Una secuencia ordenada de pasos..."
            Tema.Opcion(gunaButton8);   // "Un lenguaje de programacion"
        }

        // Pinta las tres opciones: la elegida se marca con azul claro y las demas
        // quedan como tarjeta blanca con borde (antes: azules solidos en los dos casos).
        private void PintarSeleccion()
        {
            Tema.OpcionElegida(gunaButton8, UsuarioPregunta.Pregunta1Res == 1);
            Tema.OpcionElegida(gunaButton3, UsuarioPregunta.Pregunta1Res == 2);
            Tema.OpcionElegida(gunaButton2, UsuarioPregunta.Pregunta1Res == 3);
        }

        private void pregunta1_Load(object sender, EventArgs e)
        {
            // Evita que los botones capturen el foco con el teclado.
            gunaButton8.TabStop = false;
            gunaButton3.TabStop = false;
            gunaButton2.TabStop = false;

        }

        // Opcion 1 (incorrecta): solo guarda la eleccion del usuario.
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 1;
            PintarSeleccion();
        }

        // Opcion 2 (la correcta): guarda la eleccion del usuario.
        // El acierto se calcula despues en Ganaste.CalcularAciertos().
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 2;
            PintarSeleccion();
        }

        // Opcion 3 (incorrecta): solo guarda la eleccion del usuario.
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 3;
            PintarSeleccion();
        }
    }
}
