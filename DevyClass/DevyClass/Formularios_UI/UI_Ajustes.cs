using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.UsuarioDB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DevyClass
{
    // Formulario de ajustes del usuario: permite cambiar el nombre de usuario
    // y la contrasena, y cerrar sesion. Los cambios se guardan en la base de datos.
    public partial class UI_Ajustes : Form
    {
        private DatosUsuario UsuarioActual; // Usuario que esta editando sus datos.
        public UI_Ajustes(DatosUsuario usuario)
        {
            InitializeComponent();
            // se establece todo a la medida del usuario.
            UsuarioActual = usuario;
            // Se cargan los datos actuales del usuario en las cajas de texto.
            txtContrasena.Text = UsuarioActual.Contrasena;
            txtUserName.Text = UsuarioActual.Username;
            txtConfirmarContrasena.Text = UsuarioActual.Contrasena;

            // Color de fondo general del panel1 (estilo oscuro de la app).
            panel1.BackColor = ColorTranslator.FromHtml("#1A2233");
            panel2.BackColor = ColorTranslator.FromHtml("#1E2A38");
            panel3.BackColor = ColorTranslator.FromHtml("#1E2A38");
        }

        // Boton "Regresar": vuelve al menu principal.
        private void btnregresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            UI_MenuPrincipal.AbrirMenu(UsuarioActual);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formAjustes_Load(object sender, EventArgs e)
        {
            






        }

        // Boton "Guardar cambios": valida y actualiza nombre de usuario y contrasena en la BD.
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            // Valida que ningun campo este vacio.
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text) || string.IsNullOrWhiteSpace(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valida que las contrasenas coincidan.
            if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar requisitos de la contraseña (mínimo 8 caracteres, letra, número y especial)
            string patronContrasena = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%?&#.\-])[A-Za-z\d@$!%*?&#.\-]{8,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtContrasena.Text, patronContrasena))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres e incluir letras, números y un carácter especial (@, $, !, %, *, #, etc.).", "Contraseña no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConsultasUsuario dao = new ConsultasUsuario();

            // Verifica que el nuevo nombre de usuario no pertenezca a otro usuario (se excluye al actual).
            if (dao.ExisteUsername(txtUserName.Text.Trim(), UsuarioActual.IdUsuario))
            {
                MessageBox.Show("Ese nombre de usuario ya está en uso por otro usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Pide confirmacion antes de guardar.
            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que deseas guardar los cambios?",
                "Confirmar cambios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta != DialogResult.Yes) return;

            try
            {
                // Se actualizan el nombre de usuario y la contrasena en la base de datos.
                int filasAfectadas = dao.EditarUsuarioCompleto(UsuarioActual.IdUsuario, txtUserName.Text.Trim(), txtContrasena.Text);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UsuarioActual.Username = txtUserName.Text.Trim(); // Actualiza el nombre tambien en memoria.
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
                // Error 1062 de MySQL = clave duplicada (nombre de usuario ya existente).
                if (ex.Number == 1062)
                    MessageBox.Show("Ese nombre de usuario ya está en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Error en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton "Cerrar sesion": limpia los datos y vuelve al inicio de sesion.
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            UsuarioActual.BorrarDatos(); // Elimina los datos del usuario en memoria.
            UI_InicioSesion accederUsesion = new UI_InicioSesion();
            this.Close();
            accederUsesion.Show();
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        // Icono "Generar contrasena": rellena ambos campos con una contrasena aleatoria.
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtContrasena.Text = nueva;
            txtConfirmarContrasena.Text = nueva;
        }
    }
}
