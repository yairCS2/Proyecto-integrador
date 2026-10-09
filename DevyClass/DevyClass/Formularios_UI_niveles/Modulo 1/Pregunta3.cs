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
    // Pantalla de la pregunta 3 del nivel 1: hay que ordenar los pasos del
    // algoritmo (ponerle un numero a cada bloque).
    // Validacion: Nivel1 consulta OrdenValido()/OrdenCorrecto() antes de avanzar.
    public partial class Pregunta3 : UserControl
    {
        public Pregunta3()
        {
            InitializeComponent();

            // Fase 2: estilo Duolingo (mismo escalado que Nivel1 para llenar el panel).
            PrepararEstilo();
        }

        /// <summary> Corrige textos, agranda el diseno y estiliza los bloques y selectores. </summary>
        private void PrepararEstilo()
        {
            label2.Text = "Ejercicio 3 · Ordenar bloques";
            label1.Text = "Ordena los pasos del algoritmo para hacer un sándwich";
            this.BackColor = Tema.Blanco;

            Tema.Titulo(label1, 13F);   // la consigna
            Tema.Titulo(label2, 11F);   // el rotulo del ejercicio
            Tema.Campo(label3);
            Tema.Campo(label4);
            Tema.Campo(label5);

            this.PerformLayout();

            // Mismo factor que Nivel1 (1.7): el control llena panelPregunta.
            Tema.EscalarHijos(this, 1.7F);
            Tema.EstilizarRaiz(this);

            // Los tres pasos del algoritmo, como tarjetas. Se hace al final para que
            // la esquina redondeada se calcule con el tamano ya escalado.
            Tema.Tarjeta(panel1, Tema.FondoGris, Tema.Borde);
            Tema.Tarjeta(panel2, Tema.FondoGris, Tema.Borde);
            Tema.Tarjeta(panel3, Tema.FondoGris, Tema.Borde);
        }

        // Devuelve true cuando el usuario ya asigno un numero distinto a cada bloque
        // (el estado inicial 1,1,1 no cuenta como respondido).
        public bool OrdenValido()
        {
            string cierre = domainUpDown1.Text;      // "Cerrar el sandwich"
            string pan = domainUpDown2.Text;         // "Tomar dos rebanadas de pan"
            string relleno = domainUpDown3.Text;     // "Agregar el relleno"

            return !string.IsNullOrWhiteSpace(cierre)
                && !string.IsNullOrWhiteSpace(pan)
                && !string.IsNullOrWhiteSpace(relleno)
                && cierre != pan && pan != relleno && cierre != relleno;
        }

        // Orden correcto:
        //   1) Tomar dos rebanadas de pan (domainUpDown2)
        //   2) Agregar el relleno (domainUpDown3)
        //   3) Cerrar el sandwich (domainUpDown1)
        public bool OrdenCorrecto()
        {
            return domainUpDown2.Text == "1"
                && domainUpDown3.Text == "2"
                && domainUpDown1.Text == "3";
        }

        private void Pregunta3_Load(object sender, EventArgs e)
        {

        }
    }
}
