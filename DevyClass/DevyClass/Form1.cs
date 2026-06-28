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
            UI_InicioSesion accederUI = new UI_InicioSesion();

            this.Hide();
            accederUI.Show();
            
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
        
        private void paP3_MouseLeave(object sender, EventArgs e)
        {
            paP3.BackgroundImage=null;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;


        }

        private void paP3_MouseEnter(object sender, EventArgs e)
        {
            paP3.BackgroundImage = Properties.Resources.candado_Tarjeta;

            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnContinuarP1_Click(object sender, EventArgs e)
        {
            Niveles_Modulo1 accederNM1 = new Niveles_Modulo1();
            this.Hide();
            accederNM1.Show();
        }
    }
}
