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

namespace DevyClass
{
    public partial class UI_AgregarUsuario : Form
    {
        bool OjoRegistro;
        private DatosUsuario UsuarioActual;
        public UI_AgregarUsuario(DatosUsuario usuario)
        {
            UsuarioActual = usuario;
            InitializeComponent();
            OjoRegistro = false;
            txtcontrasegura.PasswordChar = '•';
            txtconfirmarcontra.PasswordChar = '•';
            OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
            rdbUsuarioNromal.Checked = true;
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

        private void AgregarUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            UI_Administrador accederF1 = new UI_Administrador(UsuarioActual);
            this.Close();
            accederF1.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_Administrador a = new UI_Administrador(UsuarioActual);
            this.Close();
            a.Show();
        }

        private void rdbUsuarioNromal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtcontrasegura.Text = nueva;
            txtconfirmarcontra.Text = nueva;
        }

        private void OjoContrasenia_Click(object sender, EventArgs e)
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

        private void btnregistro_Click(object sender, EventArgs e)
        {
            //validamos todos los campos que son obligatorios
            if ((string.IsNullOrWhiteSpace(txtUsuario.Text)) || (string.IsNullOrWhiteSpace(txtCorreo.Text)) || (string.IsNullOrWhiteSpace(txtcontrasegura.Text)) || (string.IsNullOrWhiteSpace(txtconfirmarcontra.Text)))
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
            int tipo = rbAdministrador.Checked ? 1 : 2; // 1 = Admin, 2 = Normal
            int nivel = int.Parse(txtNivelInicial.Text.Trim());

            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();

                int filasAfectadas = dao.RegistrarUsuario(
                    txtUsuario.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    dateTimePicker1.Value.Date,
                    txtcontrasegura.Text,
                    tipo,
                    nivel
                );

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Usuario agregado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // aquí normalmente no navegas a UI_MenuPrincipal como en el registro normal,
                    // sino que cierras el form o limpias los campos para agregar otro
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que deseas borrar todos los campos?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                LimpiarTextBoxes(this.Controls);
            }
        }

        private void LimpiarTextBoxes(Control.ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox txt)
                {
                    if (txt.Name == "txtNivelInicial")
                    {
                        txt.Text = "0";
                    }
                    else
                    {
                        txt.Clear();
                    }
                }

                if (ctrl.HasChildren)
                {
                    LimpiarTextBoxes(ctrl.Controls);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
