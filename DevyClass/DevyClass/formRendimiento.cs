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
    public partial class formRendimiento : Form
    {
        public formRendimiento()
        {
            InitializeComponent();
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            Form1 accederform1 = new Form1();
            this.Hide();
            accederform1.Show();
        }
    }
}
