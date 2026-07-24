using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevyClass.Autenticacion;

namespace DevyClass
{
    public partial class UI_Registro : Form
    {
        public UI_Registro()
        {
            InitializeComponent();


        }

        private void UI_Registro_Load(object sender, EventArgs e)
        {
           
        }

      
       

        private void txtusuario_Enter(object sender, EventArgs e)
        {
           

        }

        private void txtusuario_Leave(object sender, EventArgs e)
        {
            

        }

        private void caja_Entrar(object sender, EventArgs e)
        {

        }

        private void txtcontrasegura_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {
                

        }

        private void btnregistro_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtusuario.Text;
                string correo = txtcorreo.Text;
                string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string contrasenia = txtcontrasegura.Text;

                if (usuario == "" || correo == "" || fecha == "" || contrasenia == "")
                {
                    MessageBox.Show("llene tododos los campos porfavor");
                    return;
                }

                RegistrarUsuario R = new RegistrarUsuario();
                R.RegistarUsuario(usuario, correo, fecha, contrasenia);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


        }
    }
}
