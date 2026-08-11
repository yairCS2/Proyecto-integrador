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
    // Formulario del administrador para AGREGAR un usuario nuevo manualmente.
    // Permite elegir el tipo (admin o normal), el nivel inicial y generar contrasena aleatoria.
    public partial class UI_AgregarUsuario : Form
    {
        bool OjoRegistro; // Controla si las contrasenas se muestran u ocultan.
        private DatosUsuario UsuarioActual; // Administrador que agrega al usuario.
        public UI_AgregarUsuario(DatosUsuario usuario)
        {
            UsuarioActual = usuario;
            InitializeComponent();
            OjoRegistro = false;
            // Las contrasenas inician ocultas con puntos.
            txtcontrasegura.PasswordChar = '•';
            txtconfirmarcontra.PasswordChar = '•';
            OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
            rdbUsuarioNromal.Checked = true; // Por defecto el tipo seleccionado es "usuario normal".
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

        private void AgregarUsuario_Load(object sender, EventArgs e)
        {

        }

        // Boton "Salir": cierra el form y regresa al panel de administrador.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            UI_Administrador accederF1 = new UI_Administrador(UsuarioActual);
            this.Close();
            accederF1.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Icono "Regresar": vuelve al panel de administrador.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_Administrador a = new UI_Administrador(UsuarioActual);
            this.Close();
            a.Show();
        }

        private void rdbUsuarioNromal_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Icono "Generar contrasena": rellena ambos campos con una contrasena aleatoria.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtcontrasegura.Text = nueva;
            txtconfirmarcontra.Text = nueva;
        }

        // Evento del icono del ojo: muestra u oculta las contrasenas.
        private void OjoContrasenia_Click(object sender, EventArgs e)
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

        // Boton "Agregar usuario": valida los campos e inserta el usuario en la base de datos.
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
            int tipo = rbAdministrador.Checked ? 1 : 2; // 1 = Admin, 2 = Normal (segun el radio seleccionado).
            int nivel = int.Parse(txtNivelInicial.Text.Trim()); // Nivel en el que empezara el usuario.

            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();

                // Se inserta el usuario con los datos capturados.
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

        // Icono "Limpiar campos": pregunta y borra el contenido de todas las cajas de texto.
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

        // Recorre todos los controles del formulario y limpia los TextBox.
        // txtNivelInicial no se borra, se le pone "0".
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

                // Si el control contiene otros controles dentro (como un panel), se recorre de nuevo.
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
