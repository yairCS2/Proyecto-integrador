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
    public partial class UI_Ajustes : Form
    {
        private DatosUsuario UsuarioActual;
        public UI_Ajustes(DatosUsuario usuario)
        {
            InitializeComponent();
            // se establece todo a la medida del usuario.
            UsuarioActual = usuario;
            txtContrasena.Text = UsuarioActual.Contrasena;
            txtUserName.Text = UsuarioActual.Username;
            txtConfirmarContrasena.Text = UsuarioActual.Contrasena;

            // Color de fondo general del panel1
            panel1.BackColor = ColorTranslator.FromHtml("#1A2233");
            panel2.BackColor = ColorTranslator.FromHtml("#1E2A38");
            panel3.BackColor = ColorTranslator.FromHtml("#1E2A38");
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
        
            UI_MenuPrincipal accederform1 = new UI_MenuPrincipal(UsuarioActual);
          
            this.Hide();
            accederform1.Show();

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

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text) || string.IsNullOrWhiteSpace(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            if (dao.ExisteUsername(txtUserName.Text.Trim(), UsuarioActual.IdUsuario))
            {
                MessageBox.Show("Ese nombre de usuario ya está en uso por otro usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que deseas guardar los cambios?",
                "Confirmar cambios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta != DialogResult.Yes) return;

            try
            {
                int filasAfectadas = dao.EditarUsuarioCompleto(UsuarioActual.IdUsuario, txtUserName.Text.Trim(), txtContrasena.Text);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UsuarioActual.Username = txtUserName.Text.Trim();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
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

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            UI_InicioSesion accederUsesion = new UI_InicioSesion();
            this.Hide();
            UsuarioActual.BorrarDatos();
            accederUsesion.Show();
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string nueva = GenerarContrasenaAleatoria();
            txtContrasena.Text = nueva;
            txtConfirmarContrasena.Text = nueva;
        }
    }
}
