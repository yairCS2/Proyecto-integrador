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
    public partial class Niveles_Modulo1 : Form
    {
        public Niveles_Modulo1()
        {
            InitializeComponent();
        }

        private void Niveles_Modulo1_Load(object sender, EventArgs e)
        {
            Cartel accederCartel = new Cartel();
            accederCartel.Location = new Point(this.Left + 10, this.Top + 20);
            accederCartel.Show(this);
        }
    }
}
