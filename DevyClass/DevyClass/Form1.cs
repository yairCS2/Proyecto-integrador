using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;

namespace DevyClass
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Console.WriteLine("Probando cosas...");

            //cambios
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        bool menuExpandido = false;

    
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //transicionMenu.Show(panel1);

            //if (menuExpandido)
            //{
            //    panel1.Width = 50;
            //    menuExpandido = false;
            //}
            //else
            //{
            //    panel1.Width = 200;
            //    menuExpandido = true;
            //}
        }

        private void btnTemario_Click(object sender, EventArgs e)
        {
            formTemario accederformTemario = new formTemario();
            accederformTemario.Show();
            this.Hide();

        }

        private void transicionMenu_AllAnimationsCompleted(object sender, EventArgs e)
        {
            
        }
        


        private void btnRendimiento_Click(object sender, EventArgs e)
        {

            formRendimiento accederformRendimiento = new formRendimiento();


            accederformRendimiento.Show();
            // hide me permite cambiar de formulario
            // en cambio si le pongo close se cierra el formulario actual y termina el programa 
            this.Hide();

    
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogros_Click(object sender, EventArgs e)
        {
            formLogros accederformLogros = new formLogros();
            accederformLogros.Show();
            this.Hide();

        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            formAjustes accedeerformAjustes = new formAjustes();

            accedeerformAjustes.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void pnP1_MouseLeave(object sender, EventArgs e)
        {
            pnP1.BackgroundImage =null;
        }

        private void pnP1_MouseEnter(object sender, EventArgs e)
        {
            pnP1.BackgroundImage = Properties.Resources.candado;
        }
    }
}
