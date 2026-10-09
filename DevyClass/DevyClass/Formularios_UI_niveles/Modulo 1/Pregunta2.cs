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
    // Pantalla de la pregunta 2 del nivel 1 (verdadero o falso).
    // La eleccion se guarda en Nivel1RepuestasCorrectas (1 = verdadero, 2 = falso).
    // Antes los botones no hacian nada: el nivel se podia terminar sin responder.
    public partial class Pregunta2 : UserControl
    {
        // Colores antiguos de las opciones (ahora los maneja Tema.Opcion /
        // Tema.OpcionElegida: tarjeta blanca y seleccion en azul claro).

        private Nivel1RepuestasCorrectas UsuarioPregunta; // Estado compartido del nivel (puede ser null).

        // Constructor vacio: lo usa el diseñador de Visual Studio.
        public Pregunta2() : this(null)
        {
        }

        public Pregunta2(Nivel1RepuestasCorrectas usuario)
        {
            InitializeComponent();
            UsuarioPregunta = usuario;

            // Fase 2: estilo Duolingo (mismo escalado que Nivel1 para llenar el panel).
            PrepararEstilo();

            PintarSeleccion();
        }

        /// <summary> Corrige textos, agranda el diseno y vuelve las opciones en tarjetas. </summary>
        private void PrepararEstilo()
        {
            lblBienvenida.Text = "Ejercicio 2 · Verdadero o falso";
            this.BackColor = Tema.Blanco;

            Tema.Titulo(lblFraseMotivadora, 13F);  // la frase que hay que evaluar
            Tema.Titulo(lblBienvenida, 11F);       // el rotulo del ejercicio

            this.PerformLayout();

            // Mismo factor que Nivel1 (1.7): el control llena panelPregunta.
            Tema.EscalarHijos(this, 1.7F);
            Tema.EstilizarRaiz(this);

            Tema.Opcion(gunaButton8);   // verdadero
            Tema.Opcion(gunaButton1);   // falso
        }

        // Pinta las dos opciones: la elegida con azul claro y la otra como tarjeta blanca.
        private void PintarSeleccion()
        {
            if (UsuarioPregunta == null) return;

            Tema.OpcionElegida(gunaButton8, UsuarioPregunta.Pregunta2Res == 1); // verdadero
            Tema.OpcionElegida(gunaButton1, UsuarioPregunta.Pregunta2Res == 2); // falso
        }

        // Opcion "verdadero" (la respuesta correcta: todo algoritmo debe terminar).
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            if (UsuarioPregunta == null) return;
            UsuarioPregunta.Pregunta2Res = 1;
            PintarSeleccion();
        }

        // Opcion "falso".
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (UsuarioPregunta == null) return;
            UsuarioPregunta.Pregunta2Res = 2;
            PintarSeleccion();
        }

        private void Pregunta2_Load(object sender, EventArgs e)
        {

        }

        private void lblFraseMotivadora_Click(object sender, EventArgs e)
        {

        }
    }
}
