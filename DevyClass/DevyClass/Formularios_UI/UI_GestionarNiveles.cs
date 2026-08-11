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

namespace DevyClass
{
    // Formulario del administrador para GESTIONAR los niveles del juego.
    // Permite ver todos los niveles en una tabla, buscar, editar el seleccionado,
    // agregar niveles nuevos y eliminar los existentes.
    public partial class UI_GestionarNiveles : Form
    {
        private DatosUsuario UsuarioActual; // Administrador que entro al formulario.
        private DataTable tablaNiveles;     // Tabla con los niveles (se llena desde la BD).
        private DataTable tablaModulos;     // Tabla con los modulos (para el ComboBox).
        private bool huboResultados = true; // Evita que la alerta de busqueda salte en cada tecla.

        public UI_GestionarNiveles(DatosUsuario usuario)
        {
            InitializeComponent();
            UsuarioActual = usuario;
            CargarModulos(); // Llena el ComboBox de modulos.
            CargarNiveles(); // Llena la tabla de niveles.
        }

        // Llena el ComboBox con los modulos disponibles de la base de datos.
        private void CargarModulos()
        {
            try
            {
                ConsultasNivel dao = new ConsultasNivel();
                tablaModulos = dao.ObtenerModulos();

                cboModulo.DataSource = tablaModulos;
                cboModulo.DisplayMember = "modulo";  // Lo que se ve.
                cboModulo.ValueMember = "id_modulo"; // El valor que se guarda.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los módulos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carga todos los niveles de la base de datos en el DataGridView.
        private void CargarNiveles()
        {
            try
            {
                ConsultasNivel dao = new ConsultasNivel();
                tablaNiveles = dao.ObtenerTodosLosNiveles();

                dgvNiveles.DataSource = tablaNiveles;

                // Configuracion de la tabla: solo una fila completa seleccionable y no editable.
                dgvNiveles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvNiveles.MultiSelect = false;
                dgvNiveles.ReadOnly = true;
                dgvNiveles.AllowUserToAddRows = false;

                // Se renombran las columnas para nombres entendibles.
                dgvNiveles.Columns["id_nivel"].HeaderText = "ID";
                dgvNiveles.Columns["nombre"].HeaderText = "Nivel";
                dgvNiveles.Columns["xp_necesaria"].HeaderText = "XP necesaria";
                dgvNiveles.Columns["xp_otorgada"].HeaderText = "XP otorgada";
                dgvNiveles.Columns["nombre_modulo"].HeaderText = "Módulo";
                // La columna del id del modulo no se muestra (solo sirve para editar).
                dgvNiveles.Columns["referencia_modulo"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los niveles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Al hacer clic en una fila, se cargan sus datos en las cajas de texto.
        private void dgvNiveles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNiveles.SelectedRows.Count == 0) return;

            DataGridViewRow fila = dgvNiveles.SelectedRows[0];

            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtXpNecesaria.Text = fila.Cells["xp_necesaria"].Value?.ToString();
            txtXpOtorgada.Text = fila.Cells["xp_otorgada"].Value?.ToString();

            // Se selecciona el modulo correspondiente en el ComboBox (si tiene uno).
            if (fila.Cells["referencia_modulo"].Value != DBNull.Value && fila.Cells["referencia_modulo"].Value != null)
            {
                cboModulo.SelectedValue = Convert.ToInt32(fila.Cells["referencia_modulo"].Value);
            }
        }

        // Boton "Guardar cambios": actualiza el nivel seleccionado en la tabla.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvNiveles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un nivel de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            int idNivel = Convert.ToInt32(dgvNiveles.SelectedRows[0].Cells["id_nivel"].Value);

            try
            {
                ConsultasNivel dao = new ConsultasNivel();
                int filasAfectadas = dao.ActualizarNivel(
                    idNivel,
                    txtNombre.Text.Trim(),
                    int.Parse(txtXpNecesaria.Text),
                    int.Parse(txtXpOtorgada.Text),
                    (int)cboModulo.SelectedValue
                );

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Nivel actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarNiveles(); // Se recarga la tabla para ver los cambios.
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el nivel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton "Agregar": inserta un nivel nuevo con los datos de las cajas.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                ConsultasNivel dao = new ConsultasNivel();
                int filasAfectadas = dao.AgregarNivel(
                    txtNombre.Text.Trim(),
                    int.Parse(txtXpNecesaria.Text),
                    int.Parse(txtXpOtorgada.Text),
                    (int)cboModulo.SelectedValue
                );

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Nivel agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarNiveles();
                    LimpiarCampos(); // Se limpian las cajas para poder agregar otro.
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el nivel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton "Eliminar": borra el nivel seleccionado (con confirmacion).
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvNiveles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un nivel de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idNivel = Convert.ToInt32(dgvNiveles.SelectedRows[0].Cells["id_nivel"].Value);
            string nombreNivel = dgvNiveles.SelectedRows[0].Cells["nombre"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                $"¿Seguro que deseas eliminar el nivel \"{nombreNivel}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (respuesta != DialogResult.Yes) return;

            try
            {
                ConsultasNivel dao = new ConsultasNivel();
                int filasAfectadas = dao.EliminarNivel(idNivel);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Nivel eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarNiveles();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró el nivel a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton "Limpiar": vacia las cajas de texto y quita la seleccion de la tabla.
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Valida que los campos obligatorios esten llenos y que las XP sean numeros.
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del nivel es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtXpNecesaria.Text, out _))
            {
                MessageBox.Show("La XP necesaria debe ser un número.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtXpOtorgada.Text, out _))
            {
                MessageBox.Show("La XP otorgada debe ser un número.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboModulo.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un módulo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Limpia las cajas de texto y deja el ComboBox en su primera opcion.
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtXpNecesaria.Clear();
            txtXpOtorgada.Clear();
            if (cboModulo.Items.Count > 0) cboModulo.SelectedIndex = 0;
            dgvNiveles.ClearSelection();
        }

        // Cuadro de busqueda: filtra los niveles por ID en la tabla.
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (tablaNiveles == null) return;

            string filtro = txtBuscar.Text.Trim().Replace("'", "''"); // evita romper el RowFilter si escribe un apóstrofe

            // RowFilter aplica un filtro tipo SQL a la tabla en memoria.
            // Se convierte la columna id_nivel a texto para poder buscar por partes del ID.
            tablaNiveles.DefaultView.RowFilter = $"Convert(id_nivel, 'System.String') LIKE '%{filtro}%'";
            dgvNiveles.DataSource = tablaNiveles.DefaultView;

            // Si no se encontro ningun nivel y antes si habia resultados, se avisa.
            // El flag "huboResultados" evita que la alerta salte en cada tecla mientras se escribe.
            if (tablaNiveles.DefaultView.Count == 0 && huboResultados)
            {
                MessageBox.Show("No se encontró ningún nivel con ese ID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Se actualiza el estado: ¿la busqueda actual encontro resultados?
            huboResultados = tablaNiveles.DefaultView.Count > 0;
        }

        // Icono "Regresar": vuelve al panel de administrador.
        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            UI_Administrador admin = new UI_Administrador(UsuarioActual);
            this.Hide();
            admin.Show();
        }
    }
}
