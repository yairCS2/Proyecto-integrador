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
    public partial class UI_Registro : Form
    {
        bool OjoRegistro;
        public UI_Registro()
        {
            InitializeComponent();
            OjoRegistro = true;
            OjoContrasenia.Image = Properties.Resources.ojo_abierto;
            txtcontrasegura.PasswordChar = default;
            txtconfirmarcontra.PasswordChar = default;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

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
                    DatosUsuario usuarioActual = dao.ObtenerUsuarioPorUsername(txtusuario.Text);
                    UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(usuarioActual);
                    this.Hide();
                    accederF1.Show();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OjoRegistro = !OjoRegistro;
            if (OjoRegistro)
            {
                OjoContrasenia.Image = Properties.Resources.ojo_abierto;
                txtcontrasegura.PasswordChar = default;
                txtconfirmarcontra.PasswordChar = default;
            }
            else
            {
                OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
                txtcontrasegura.PasswordChar = '•';
                txtconfirmarcontra.PasswordChar = '•';
            }
        }

        private void linklbinicio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI_InicioSesion inicia = new UI_InicioSesion();
            this.Hide();
            inicia.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtcontrasegura.Text = nueva;
            txtconfirmarcontra.Text = nueva;
        }
    }
}
