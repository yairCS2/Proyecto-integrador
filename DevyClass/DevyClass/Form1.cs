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
    }
}
