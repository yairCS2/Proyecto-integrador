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
    // Formulario de inicio de sesion: primera pantalla que ve el usuario.
    // Permite ingresar usuario y contrasena para entrar a la aplicacion,
    // o ir al formulario de registro si aun no tiene cuenta.
    public partial class UI_InicioSesion : Form
    {
        bool OjoRegistro; // Controla si la contrasena se ve o se oculta (mostrar/ocultar caracteres).
        public UI_InicioSesion()
        {
            
            InitializeComponent();
            OjoRegistro = false;
            // La contrasena se muestra con puntos "•" por defecto (oculta).
            txtcontrasenia.PasswordChar = '•';
            // El icono del ojo inicia en "cerrado" (contrasena oculta).
            OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
        }

        private void UI_InicioSesion_Load(object sender, EventArgs e)
        {
            // Se ejecuta cuando la ventana se carga. (Vacio, no hace nada por ahora.)
        }

        // Evento del boton "Iniciar sesion".
        private void btninicia_Click(object sender, EventArgs e)
        {
            

            try
            {
                string nombre = txtusuario.Text;
                string contrasenia = txtcontrasenia.Text;
                // Valida que ningun campo este vacio.
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

            // Se valida el usuario y contrasena contra la base de datos.
            ValidarContraseniaYUsuario validar = new ValidarContraseniaYUsuario();
            if (validar.UsuarioyContraseniaCorrectos(txtusuario.Text, txtcontrasenia.Text))
            {
                // Si los datos son correctos, se obtiene el usuario completo de la BD
                // y se abre el menu principal pasandole ese usuario.
                ConsultasUsuario dao = new ConsultasUsuario();
                DatosUsuario usuarioActual = dao.ObtenerUsuarioPorUsername(txtusuario.Text);
                this.Hide();
                UI_MenuPrincipal.AbrirMenu(usuarioActual);
                return;
            }
            else
            {
                // Si no coinciden, se avisa al usuario.
                MessageBox.Show("usuario o contraseña incorrecta");
            }
         
           
        }

        // Evento del link "Registrarse": lleva al formulario de registro.
        private void linkLbregistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI_Registro R = new UI_Registro();
            this.Hide();
            R.Show();
        }

        // Evento del icono del ojo: muestra u oculta la contrasena.
        private void OjoContrasenia_Click(object sender, EventArgs e)
        {
            OjoRegistro = !OjoRegistro; // Cambia el estado.
            if (OjoRegistro)
            {
                // Mostrar contrasena: icono de ojo abierto y sin caracter de ocultamiento.
                OjoContrasenia.Image = Properties.Resources.ojo_abierto;
                txtcontrasenia.PasswordChar = default;
            }
            else
            {
                // Ocultar contrasena: icono de ojo cerrado y puntos.
                OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
                txtcontrasenia.PasswordChar = '•';
            }
        }

        private void lbtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
