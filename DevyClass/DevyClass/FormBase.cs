using System.Drawing;
using System.Windows.Forms;

namespace DevyClass
{
    public class FormBase : Form
    {
        public FormBase()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Icon = Properties.Resources.DevyPngSinFondo;
        }
    }
}
