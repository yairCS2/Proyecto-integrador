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
    // Pantalla de victoria: se muestra al final del nivel 1 cuando el usuario
    // completa todas las preguntas. Muestra el titulo y cuantas respuestas acerto.
    public partial class Ganaste : UserControl
    {
        // Constructor vacio: lo usa el diseñador de Visual Studio.
        public Ganaste() : this(null)
        {
        }

        public Ganaste(Nivel1RepuestasCorrectas estado)
        {
            InitializeComponent();
            if (estado == null) return; // Sin estado se muestra solo el diseno.

            int aciertos = CalcularAciertos(estado);

            // Titulo + resultado, centrado en la pantalla.
            // Antes la pantalla no mostraba ningun resultado (se "ganaba" igual con 0 aciertos).
            label2.Font = new Font(label2.Font.FontFamily, 16F, label2.Font.Style);
            label2.Text = $"¡Ganaste!\nAcertaste {aciertos} de 3 respuestas";
            label2.Location = new Point(Math.Max(0, (Width - label2.Width) / 2), 18);

            // Fase 2: estilo Duolingo (mismo escalado que Nivel1 para llenar el panel).
            PrepararEstilo();
        }

        /// <summary> Titulo en verde, fondo blanco y el mismo escalado del nivel. </summary>
        private void PrepararEstilo()
        {
            this.BackColor = Tema.Blanco;
            label2.ForeColor = Tema.Verde;   // "Ganaste" en verde Duolingo

            this.PerformLayout();

            // Mismo factor que Nivel1 (1.7): el control llena panelPregunta.
            Tema.EscalarHijos(this, 1.7F);
            Tema.EstilizarRaiz(this);
        }

        // Los aciertos se recalculan desde las tres respuestas.
        // Antes se usaba un contador que solo sumaba la pregunta 1 y podia quedar
        // inflado si el usuario cambiaba su respuesta despues de acertar.
        private static int CalcularAciertos(Nivel1RepuestasCorrectas estado)
        {
            int aciertos = 0;
            if (estado.Pregunta1Res == 2) aciertos++; // Pregunta 1: la opcion 2 es la correcta.
            if (estado.Pregunta2Res == 1) aciertos++; // Pregunta 2: "verdadero".
            if (estado.Pregunta3Res == 1) aciertos++; // Pregunta 3: orden correcto.
            return aciertos;
        }
    }
}
