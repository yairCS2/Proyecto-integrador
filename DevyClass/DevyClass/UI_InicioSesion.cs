using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevyClass
{
    public partial class UI_InicioSesion : Form
    {
        public UI_InicioSesion()
        {
            InitializeComponent();
        }

        private void UI_InicioSesion_Load(object sender, EventArgs e)
        {

        }

        private void btninicia_Click(object sender, EventArgs e)
        {
            Form1 accederF1 = new Form1();  
            this.Hide();
            accederF1.Show();
        }
    }
}
