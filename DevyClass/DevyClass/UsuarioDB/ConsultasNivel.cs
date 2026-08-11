using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace DevyClass.Base_de_datos_DevyClass_
{
    // Clase de acceso a datos (DAO) de la tabla "niveles".
    // Contiene las consultas para listar, agregar, editar y eliminar niveles.
    // La usa el formulario UI_GestionarNiveles (panel de administrador).
    public class ConsultasNivel
    {
        // Conexion compartida por todos los metodos de esta clase.
        Conexion conexion = new Conexion();

        // Devuelve todos los niveles en una DataTable, unidos con el nombre de su modulo.
        // Se usa para llenar el DataGridView del formulario de gestion de niveles.
        public DataTable ObtenerTodosLosNiveles()
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                // LEFT JOIN: muestra todos los niveles, incluso si no tienen modulo asignado.
                string query = @"SELECT n.id_nivel, n.nombre, n.xp_necesaria, n.xp_otorgada,
                                        n.referencia_modulo, m.modulo AS nombre_modulo
                                 FROM niveles n
                                 LEFT JOIN modulo m ON n.referencia_modulo = m.id_modulo
                                 ORDER BY n.id_nivel";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        // Devuelve los modulos disponibles (id_modulo y nombre) para llenar el ComboBox.
        public DataTable ObtenerModulos()
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "SELECT id_modulo, modulo FROM modulo ORDER BY id_modulo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        // Inserta un nivel nuevo en la base de datos.
        // Devuelve cuantas filas se insertaron (1 = exito, 0 = no se inserto).
        public int AgregarNivel(string nombre, int xpNecesaria, int xpOtorgada, int referenciaModulo)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"INSERT INTO niveles (nombre, xp_necesaria, xp_otorgada, referencia_modulo)
                                 VALUES (@nombre, @xpNecesaria, @xpOtorgada, @modulo)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@xpNecesaria", xpNecesaria);
                    cmd.Parameters.AddWithValue("@xpOtorgada", xpOtorgada);
                    cmd.Parameters.AddWithValue("@modulo", referenciaModulo);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Actualiza los datos de un nivel existente por su id.
        // Devuelve cuantas filas se modificaron (1 = actualizado, 0 = no existia).
        public int ActualizarNivel(int idNivel, string nombre, int xpNecesaria, int xpOtorgada, int referenciaModulo)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE niveles
                                 SET nombre = @nombre,
                                     xp_necesaria = @xpNecesaria,
                                     xp_otorgada = @xpOtorgada,
                                     referencia_modulo = @modulo
                                 WHERE id_nivel = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@xpNecesaria", xpNecesaria);
                    cmd.Parameters.AddWithValue("@xpOtorgada", xpOtorgada);
                    cmd.Parameters.AddWithValue("@modulo", referenciaModulo);
                    cmd.Parameters.AddWithValue("@id", idNivel);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Elimina un nivel de la base de datos por su id.
        // Devuelve cuantas filas se eliminaron (1 = eliminado, 0 = no existia).
        public int EliminarNivel(int idNivel)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "DELETE FROM niveles WHERE id_nivel = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idNivel);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
