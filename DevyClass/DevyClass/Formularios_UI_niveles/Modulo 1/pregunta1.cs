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

        private Nivel1RepuestasCorrectas UsuarioPregunta; // Estado compartido del nivel.
        bool PreguntaActivada = false; // Evita que se cuente mas de una vez la respuesta correcta.
        public pregunta1(Nivel1RepuestasCorrectas usuario)
        {
            InitializeComponent();
            UsuarioPregunta = usuario;

            // Si el usuario ya habia elegido una opcion antes, se pinta de negro para que se vea seleccionada.
            switch (usuario.Pregunta1Res)
            {
                case 1:
                    gunaButton3.BackColor = Color.Black;
                    PreguntaActivada = true;
                    break;
                case 2:
                    gunaButton2.BackColor = Color.Black;
                    PreguntaActivada = true;
                    break;
                case 3:
                    gunaButton8.BackColor = Color.Black;
                    PreguntaActivada = true;
                    break;
                default:
                    break;
            }
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
        }

        // Opcion 2 (la correcta): guarda la eleccion y suma una respuesta correcta la primera vez.
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 2;
            if (PreguntaActivada == false)
            {
                UsuarioPregunta.RespuestasCorrectas++; // Aumenta el contador de aciertos.
                PreguntaActivada = true; // Marca que ya se conto (asi no cuenta dos veces).
            }
            
        }

        // Opcion 3 (incorrecta): solo guarda la eleccion del usuario.
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 3;
        }
    }
}
