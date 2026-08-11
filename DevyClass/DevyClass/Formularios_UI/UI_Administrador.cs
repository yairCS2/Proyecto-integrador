using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.Formularios_UI;
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
    // Formulario del administrador: muestra todos los usuarios en una tabla,
    // permite buscar, agregar y eliminar usuarios. Solo se puede entrar si el
    // usuario logueado es administrador (ReferenciaTipo == 1).
    public partial class UI_Administrador : Form
    {
        private DataTable tablaUsuarios; // Tabla con los usuarios (se llena desde la BD).
        private DatosUsuario UsuarioActual; // Administrador que entro al panel.
        private bool huboResultados = true; // Recuerda si la ultima busqueda encontro resultados (para no repetir la alerta).
        public UI_Administrador(DatosUsuario usuario)
        {
            InitializeComponent();
            UsuarioActual = usuario;
            CargarUsuarios(); // Llena la tabla con los usuarios al abrir el formulario.
        }

        // Carga todos los usuarios de la base de datos y los muestra en el DataGridView.
        private void CargarUsuarios()
        {
            try
            {
                ConsultasUsuario dao = new ConsultasUsuario();
                tablaUsuarios = dao.ObtenerTodosLosUsuarios();

                dgvUsuarios.DataSource = tablaUsuarios;

                // Configuracion de la tabla: solo se puede seleccionar una fila completa y no se edita.
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;

                // Se renombran las columnas para que se vean con nombres entendibles.
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

        private void UI_Administrador_Load(object sender, EventArgs e)
        {


        }
        // Muestra un panel y oculta los otros (navegacion entre vistas del form).
        private void MostrarPanel(Panel panel)
        {
            panelBienvenida.Visible = false;
            panelGestionarUsuarios.Visible = false;

            panel.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarPanel(panelBienvenida); // Al abrir, se muestra el panel de bienvenida.
        }

        // Boton "Salir": cierra el formulario del administrador.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Boton "Gestionar usuarios": cambia al panel de gestion de usuarios.
        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelGestionarUsuarios);
        }

        // Boton "Gestion de niveles": abre el formulario para ver, agregar, editar y borrar niveles.
        private void btnGestionarNiveles_Click(object sender, EventArgs e)
        {
            UI_GestionarNiveles gestionarNiveles = new UI_GestionarNiveles(UsuarioActual);
            this.Hide();
            gestionarNiveles.Show();
        }

        // Boton "Agregar usuario": abre el formulario para crear un usuario nuevo.
        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario adduser = new UI_AgregarUsuario(UsuarioActual);
            adduser.Show();
        }

     
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal(UsuarioActual);
            this.Close();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            UI_MenuPrincipal accederF1 = new UI_MenuPrincipal();
            this.Close();
            accederF1.Close();
        }

        private void btnAgregarUsuario_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Boton "Agregar usuario" (vista gestion): abre el formulario de agregar.
        private void button2_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario accederAU = new UI_AgregarUsuario(UsuarioActual);
            this.Hide();
            accederAU.Show();
        }

        // Icono "Eliminar usuario": abre el formulario para eliminar usuarios.
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UI_EliminarUsuario eliminar = new UI_EliminarUsuario(UsuarioActual);
            this.Close();
            eliminar.Show();
        }

        // Boton "Eliminar usuario": abre el formulario para eliminar usuarios.
        private void button1_Click(object sender, EventArgs e)
        {
            UI_EliminarUsuario eliminar = new UI_EliminarUsuario(UsuarioActual);
            this.Close();
            eliminar.Show();
        }

        // Icono "Agregar usuario": abre el formulario para agregar usuarios.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UI_AgregarUsuario accederAU = new UI_AgregarUsuario(UsuarioActual);
            this.Hide();
            accederAU.Show();
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

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    } // Fin form
}
