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
    // Formulario del administrador para ELIMINAR usuarios.
    // Muestra la lista de usuarios, permite buscar y borrar al seleccionado.
    public partial class UI_EliminarUsuario : Form
    {
        private DataTable tablaUsuarios; // Tabla con los usuarios (se llena desde la BD).
        private DatosUsuario UsuarioActual; // Administrador que elimina.
        private bool huboResultados = true; // Recuerda si la ultima busqueda encontro resultados (para no repetir la alerta).
        public UI_EliminarUsuario(DatosUsuario usuario)
        {
            InitializeComponent();
            CargarUsuarios(); // Llena la tabla al abrir.
            UsuarioActual = usuario;
        }

        // Carga todos los usuarios de la base de datos en el DataGridView.
        private void CargarUsuarios()
        {
            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();
                tablaUsuarios = dao.ObtenerTodosLosUsuarios();

                dgvUsuarios.DataSource = tablaUsuarios;

                // Configuracion de la tabla: solo una fila completa seleccionable y no editable.
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;

                // Se renombran las columnas para nombres entendibles.
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

        // Boton "Eliminar": borra de la base de datos el usuario seleccionado en la tabla.
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            // Valida que haya una fila seleccionada.
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se obtienen el id y el nombre de la fila seleccionada.
            int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_usuarios"].Value);
            string nombreUsuario = dgvUsuarios.SelectedRows[0].Cells["username"].Value.ToString();

            // Pide confirmacion antes de borrar.
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

        // Cuadro de busqueda: filtra los usuarios por nombre en la tabla.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tablaUsuarios == null) return;

            string filtro = txtBuscar.Text.Trim().Replace("'", "''"); // evita romper el RowFilter si escribe un apóstrofe

            // RowFilter aplica un filtro tipo SQL a la tabla en memoria.
            tablaUsuarios.DefaultView.RowFilter = $"username LIKE '%{filtro}%'";
            dgvUsuarios.DataSource = tablaUsuarios.DefaultView;

            // Si no se encontro ningun usuario y antes si habia resultados, se avisa.
            // El flag "huboResultados" evita que la alerta salte en cada tecla mientras se escribe.
            if (tablaUsuarios.DefaultView.Count == 0 && huboResultados)
            {
                MessageBox.Show("No se encontró ningún usuario con ese nombre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Se actualiza el estado: ¿la busqueda actual encontro resultados?
            huboResultados = tablaUsuarios.DefaultView.Count > 0;
        }

        // Icono "Regresar": vuelve al panel de administrador.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_Administrador Administrador = new UI_Administrador(UsuarioActual);
            this.Hide();
            Administrador.Show();
        }
    }
}
