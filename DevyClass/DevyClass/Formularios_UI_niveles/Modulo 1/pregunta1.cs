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
    public partial class pregunta1 : UserControl
    {

        private Nivel1RepuestasCorrectas UsuarioPregunta;
        bool PreguntaActivada = false;
        public pregunta1(Nivel1RepuestasCorrectas usuario)
        {
            InitializeComponent();
            UsuarioPregunta = usuario;
            NivelActual.Text = usuario.Pregunta1Res.ToString();

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
            gunaButton8.TabStop = false;
            gunaButton3.TabStop = false;
            gunaButton2.TabStop = false;

        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 1;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 2;
            if (PreguntaActivada == false)
            {
                UsuarioPregunta.RespuestasCorrectas++;
                PreguntaActivada = true;
            }
            
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            UsuarioPregunta.Pregunta1Res = 3;
        }
    }
}
