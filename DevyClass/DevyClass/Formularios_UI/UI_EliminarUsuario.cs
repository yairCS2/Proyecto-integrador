using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.UsuarioDB;

namespace DevyClass.Formularios_UI
{
    public partial class UI_EliminarUsuario : Form
    {
        private DataTable tablaUsuarios;
        private DatosUsuario UsuarioActual;
        public UI_EliminarUsuario(DatosUsuario usuario)
        {
            InitializeComponent();
            CargarUsuarios();
            UsuarioActual = usuario;
        }

        private void CargarUsuarios()
        {
            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();
                tablaUsuarios = dao.ObtenerTodosLosUsuarios();

                dgvUsuarios.DataSource = tablaUsuarios;

                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;

                dgvUsuarios.Columns["id_usuarios"].HeaderText = "ID";
                dgvUsuarios.Columns["username"].HeaderText = "Usuario";
                dgvUsuarios.Columns["correo"].HeaderText = "Correo";
                dgvUsuarios.Columns["fecha"].HeaderText = "Fecha nacimiento";
                dgvUsuarios.Columns["tipo"].HeaderText = "Tipo";
                dgvUsuarios.Columns["id_nivel"].HeaderText = "Nivel";
                dgvUsuarios.Columns["nombre"].HeaderText = "Nombre del nivel";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UI_EliminarUsuario_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_usuarios"].Value);
            string nombreUsuario = dgvUsuarios.SelectedRows[0].Cells["username"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                $"¿Seguro que deseas eliminar al usuario \"{nombreUsuario}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    ConsultasUsuario dao = new ConsultasUsuario();
                    int filasAfectadas = dao.EliminarUsuario(idUsuario);

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Usuario eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
                        // aquí recargas el DataGridView para que ya no aparezca
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tablaUsuarios == null) return;

            string filtro = txtBuscar.Text.Trim().Replace("'", "''"); // evita romper el RowFilter si escribe un apóstrofe

            tablaUsuarios.DefaultView.RowFilter = $"username LIKE '%{filtro}%'";
            dgvUsuarios.DataSource = tablaUsuarios.DefaultView;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_Administrador Administrador = new UI_Administrador(UsuarioActual);
            this.Hide();
            Administrador.Show();
        }
    }
}
