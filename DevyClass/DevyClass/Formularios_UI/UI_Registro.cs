using DevyClass.Autenticacion;
using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.UsuarioDB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DevyClass
{
    // Formulario de registro de nuevos usuarios.
    // Valida los campos, crea el usuario en la base de datos y entra directo al menu principal.
    public partial class UI_Registro : Form
    {
        bool OjoRegistro; // Controla si las contrasenas se muestran o se ocultan.
        public UI_Registro()
        {
            InitializeComponent();
            OjoRegistro = false;
            // Las contrasenas inician ocultas con puntos.
            OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
            txtcontrasegura.PasswordChar = '•';
            txtconfirmarcontra.PasswordChar = '•';
            // El selector de fecha se muestra en formato corto (dia/mes/ano).
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

        // Genera una contrasena aleatoria que cumple los requisitos (letra, numero y especial).
        private string GenerarContrasenaAleatoria(int longitud = 8)
        {
            const string letras = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";
            const string numeros = "0123456789";
            const string especiales = "@$!%*?&#.-";
            const string todos = letras + numeros + especiales;

            Random rnd = new Random();
            var chars = new List<char>();

            // Aseguramos al menos uno de cada tipo (para que sí pase tu regex)
            chars.Add(letras[rnd.Next(letras.Length)]);
            chars.Add(numeros[rnd.Next(numeros.Length)]);
            chars.Add(especiales[rnd.Next(especiales.Length)]);

            // Rellenamos el resto de forma aleatoria
            for (int i = chars.Count; i < longitud; i++)
                chars.Add(todos[rnd.Next(todos.Length)]);

            // Revolvemos el orden para que no siempre queden letra-número-especial al inicio
            return new string(chars.OrderBy(c => rnd.Next()).ToArray());
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

        // Evento del boton "Registrarse".
        private void btnregistro_Click(object sender, EventArgs e)
        {
            //validamos todos los campos que son obligatorios
            if ((string.IsNullOrWhiteSpace(txtusuario.Text)) || (string.IsNullOrWhiteSpace(txtcorreo.Text)) || (string.IsNullOrWhiteSpace(txtcontrasegura.Text)) || (string.IsNullOrWhiteSpace(txtconfirmarcontra.Text)))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //validamos que las contraseñas coinsidan
            if (txtcontrasegura.Text != txtconfirmarcontra.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar requisitos de la contraseña (mínimo 8 caracteres, letra, número y especial)
            string patronContrasena = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%?&#.\-])[A-Za-z\d@$!%*?&#.\-]{8,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtcontrasegura.Text, patronContrasena))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres e incluir letras, números y un carácter especial (@, $, !, %, *, #, etc.).", "Contraseña no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Insertamos a la base de datos 
            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();

                // Se inserta el usuario: tipo 2 (normal) y nivel inicial 0.
                int filasAfectadas = dao.RegistrarUsuario(
                    txtusuario.Text.Trim(),
                    txtcorreo.Text.Trim(),
                    dateTimePicker1.Value.Date,
                    txtcontrasegura.Text,
                    2, // tipo usuario normal
                    0  // nivel inicial
                );

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("¡Usuario registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Se carga al usuario recien creado y se entra al menu principal.
                    DatosUsuario usuarioActual = dao.ObtenerUsuarioPorUsername(txtusuario.Text);
                    this.Hide();
                    UI_MenuPrincipal.AbrirMenu(usuarioActual);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
                // Error 1062 de MySQL = clave duplicada (usuario o correo ya existente).
                if (ex.Number == 1062)
                    MessageBox.Show("El nombre de usuario o correo ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Error en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_Enter()
        {
           
        }

        // Evento del icono del ojo: muestra u oculta las contrasenas.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OjoRegistro = !OjoRegistro;
            if (OjoRegistro)
            {
                // Mostrar contrasenas.
                OjoContrasenia.Image = Properties.Resources.ojo_abierto;
                txtcontrasegura.PasswordChar = default;
                txtconfirmarcontra.PasswordChar = default;
            }
            else
            {
                // Ocultar contrasenas.
                OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
                txtcontrasegura.PasswordChar = '•';
                txtconfirmarcontra.PasswordChar = '•';
            }
        }

        // Evento del link "Ya tengo cuenta": vuelve al inicio de sesion.
        private void linklbinicio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI_InicioSesion inicia = new UI_InicioSesion();
            this.Hide();
            inicia.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        // Evento del icono de "generar contrasena": rellena ambos campos con una contrasena aleatoria.
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtcontrasegura.Text = nueva;
            txtconfirmarcontra.Text = nueva;
        }
    }
}
