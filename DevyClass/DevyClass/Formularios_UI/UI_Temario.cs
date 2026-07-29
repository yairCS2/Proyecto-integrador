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
    public partial class UI_Temario : Form
    {
        public UI_Temario()
        {
            InitializeComponent();
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederform1 = new UI_MenuPrincipal();
            this.Hide();
            accederform1.Show();
        }
    }
}
