using DevyClass.Autenticacion;
using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.UsuarioDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevyClass.UsuarioDB.DatosUsuario;

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
            

            try
            {
                string nombre = txtusuario.Text;
                string contrasenia = txtcontraseña.Text;
                if (nombre == "" || contrasenia == "")
                {
                    MessageBox.Show("Complete todos los cambios por favor");
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }

            ValidarContraseniaYUsuario validar = new ValidarContraseniaYUsuario();
            if (validar.UsuarioyContraseniaCorrectos(txtusuario.Text, txtcontraseña.Text))
            {
                ConsultasUsuario dao = new ConsultasUsuario();
                DatosUsuario usuarioActual = dao.ObtenerUsuarioPorUsername(txtusuario.Text);
                UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(usuarioActual);
                this.Hide();
                accederF1.Show();
                return;
            }
            else
            {
                MessageBox.Show("usuario o contraseña incorrecta");
            }
         
           
        }

        private void linkLbregistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            UI_Registro R = new UI_Registro();
            R.Show();
        }
    }
}
