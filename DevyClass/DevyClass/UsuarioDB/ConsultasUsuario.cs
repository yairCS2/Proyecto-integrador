using DevyClass.UsuarioDB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevyClass.UsuarioDB.DatosUsuario;

namespace DevyClass.Base_de_datos_DevyClass_
{
    // Clase de acceso a datos (DAO): contiene todas las consultas SQL que se le hacen
    // a la tabla "usuarios" de la base de datos. Todos los formularios la usan.
    public class ConsultasUsuario
    {
        // Conexion compartida por todos los metodos de esta clase.
        Conexion conexion = new Conexion();

        // Actualiza el nombre de usuario y la contrasena de un usuario por su id.
        // Devuelve cuantas filas se modificaron (1 = se actualizo, 0 = no existia).
        public int EditarUsuarioCompleto(int idUsuario, string nuevoUsername, string nuevaContrasena)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                // UPDATE: cambia username y contrasena del usuario con ese id.
                string query = "UPDATE usuarios SET username = @username, contrasena = @contrasena WHERE id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", nuevoUsername);
                    cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca un usuario por su nombre de usuario y lo devuelve como objeto DatosUsuario.
        // Devuelve null si no lo encuentra.
        public DatosUsuario ObtenerUsuarioPorUsername(string username)
        {
            DatosUsuario usuario = null;

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                // SELECT *: trae todas las columnas del usuario que coincida con el nombre.
                string query = "SELECT * FROM usuarios WHERE username = @username";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Si la consulta encontro una fila, se copian los datos al objeto DatosUsuario.
                        // Se usa IsDBNull por si alguna columna esta vacia (evita errores).
                        if (reader.Read())
                        {
                            usuario = new DatosUsuario
                            {
                                IdUsuario = reader.IsDBNull(reader.GetOrdinal("id_usuarios")) ? 0 : reader.GetInt32("id_usuarios"),
                                Username = reader.IsDBNull(reader.GetOrdinal("username")) ? null : reader.GetString("username"),
                                Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString("Correo"),
                                Fecha = reader.IsDBNull(reader.GetOrdinal("fecha")) ? DateTime.MinValue : reader.GetDateTime("fecha"),
                                Contrasena = reader.IsDBNull(reader.GetOrdinal("contrasena")) ? null : reader.GetString("contrasena"),
                                ReferenciaTipo = reader.IsDBNull(reader.GetOrdinal("referencia_tipo")) ? 0 : reader.GetInt32("referencia_tipo"),
                                UltimoNivel = reader.IsDBNull(reader.GetOrdinal("ultimo_nivel")) ? (int?)null : reader.GetInt32("ultimo_nivel")
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        // Inserta un usuario nuevo en la base de datos.
        // Devuelve cuantas filas se insertaron (1 = exito, 0 = no se inserto).
        // Es el metodo que usan realmente los formularios de registro y de agregar usuario.
        public int RegistrarUsuario(string usuario, string correo, DateTime fechaNacimiento, string contrasena, int tipo, int nivel)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                // INSERT: agrega una fila nueva a la tabla usuarios con todos sus datos.
                string query = @"INSERT INTO Usuarios (username, correo, fecha, contrasena, referencia_tipo, ultimo_nivel)
                          VALUES (@Usuario, @Correo, @FechaNac, @Contrasena, @tipo, @Nivel)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@FechaNac", fechaNacimiento);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena); 
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@Nivel", nivel);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Elimina un usuario de la base de datos por su id.
        // Devuelve cuantas filas se eliminaron (1 = eliminado, 0 = no existia).
        public int EliminarUsuario(int idUsuario)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                // DELETE: borra la fila cuyo id coincida.
                string query = "DELETE FROM usuarios WHERE id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Devuelve TODOS los usuarios en una DataTable (tabla en memoria).
        // Se usa para llenar el DataGridView del administrador.
        // Hace un JOIN para mostrar el tipo de usuario y el nombre del nivel.
        public DataTable ObtenerTodosLosUsuarios()
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                // JOIN: une "usuarios" con "tipo_usuario" (para el tipo) y con "niveles"
                // (para saber el nombre del nivel actual).
                string query = @"SELECT usuarios.id_usuarios, usuarios.username, usuarios.correo, usuarios.fecha, 
                          tipo_usuario.tipo, niveles.id_nivel, niveles.nombre
                          FROM usuarios 
                          JOIN niveles ON usuarios.ultimo_nivel = niveles.id_nivel
                          JOIN tipo_usuario ON usuarios.referencia_tipo = tipo_usuario.id_tipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    // adapter.Fill llena la DataTable con el resultado de la consulta.
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        // Verifica si un nombre de usuario ya existe (excluyendo a un id concreto).
        // Devuelve true si ya esta en uso. Se usa al editar, para que el usuario
        // no tome un nombre que pertenece a otro.
        public bool ExisteUsername(string username, int idExcluir)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // COUNT(*) devuelve cuantas filas hay con ese username y otro id distinto.
                string query = "SELECT COUNT(*) FROM usuarios WHERE username = @username AND id_usuarios != @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                    long total = Convert.ToInt64(cmd.ExecuteScalar());
                    return total > 0;
                }
            }
        }

        // Actualiza TODOS los datos de un usuario a partir de un objeto DatosUsuario.
        // Devuelve true si se actualizo al menos una fila.
        public bool ActualizarUsuario(DatosUsuario usuario)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                // UPDATE: cambia todas las columnas del usuario con ese id.
                string query = @"UPDATE usuarios 
                          SET username = @username,
                              correo = @correo,
                              fecha = @fecha,
                              contrasena = @contrasena,
                              referencia_tipo = @referenciaTipo,
                              ultimo_nivel = @ultimoNivel
                          WHERE id_usuarios = @idUsuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", usuario.Username);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@fecha", usuario.Fecha);
                    cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@referenciaTipo", usuario.ReferenciaTipo);
                    // UltimoNivel es nullable: si es null se envia DBNull.Value (valor nulo en la BD).
                    cmd.Parameters.AddWithValue("@ultimoNivel", (object)usuario.UltimoNivel ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0; // true si se actualizo algo
                }
            }
        }
    }
}