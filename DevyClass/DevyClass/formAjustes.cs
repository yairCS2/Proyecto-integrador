using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DevyClass
{
    public partial class formAjustes : Form
    {
        public formAjustes()
        {
            InitializeComponent();
          
            // Color de fondo general del panel1
            panel1.BackColor = ColorTranslator.FromHtml("#1A2233");
            panel2.BackColor = ColorTranslator.FromHtml("#1E2A38");
            panel3.BackColor = ColorTranslator.FromHtml("#1E2A38");





        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            Form1 accederform1 = new Form1();
            this.Hide();
            accederform1.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
