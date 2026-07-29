using DevyClass.Formularios_UI_niveles.Modulo_1;
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
    public partial class Nivel1 : Form
    {
        private Func<UserControl>[] preguntas;
        private int indiceActual = 0;
        Nivel1RepuestasCorrectas UsuarioPreguntas = new Nivel1RepuestasCorrectas();

        public Nivel1()
        {
            InitializeComponent();
            gunaButton1.TabStop = false;
            gunaButton2.TabStop = false;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            preguntas = new Func<UserControl>[]
            {
                    () => new pregunta1(UsuarioPreguntas),
                    () => new Pregunta2()
            };

            CambiarUC(preguntas[indiceActual]());
        }

        public void CambiarUC(UserControl nuevoUC)
        {
            panelPregunta.Controls.Clear();
            nuevoUC.Dock = DockStyle.Fill;
            panelPregunta.Controls.Add(nuevoUC);
        }


        private void Nivel1_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (indiceActual > 0)
            {
                indiceActual--;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso -= 25;
                progressBar1.Value = indiceActual;
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            
            if (indiceActual < preguntas.Length - 1)
            {
                
                indiceActual++;
                CambiarUC(preguntas[indiceActual]());
                UsuarioPreguntas.progreso+=25;
                progressBar1.Value = UsuarioPreguntas.progreso;
            }
        }
    }
}
