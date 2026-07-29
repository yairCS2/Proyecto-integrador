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
    public class ConsultasUsuario
    {
        Conexion conexion = new Conexion();

        public int EditarUsuarioCompleto(int idUsuario, string nuevoUsername, string nuevaContrasena)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

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

        public DatosUsuario ObtenerUsuarioPorUsername(string username)
        {
            DatosUsuario usuario = null;

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM usuarios WHERE username = @username";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
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

        public int RegistrarUsuario(string usuario, string correo, DateTime fechaNacimiento, string contrasena, int tipo, int nivel)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

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

        public int EliminarUsuario(int idUsuario)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "DELETE FROM usuarios WHERE id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerTodosLosUsuarios()
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"SELECT usuarios.id_usuarios, usuarios.username, usuarios.correo, usuarios.fecha, 
                          tipo_usuario.tipo, niveles.id_nivel, niveles.nombre
                          FROM usuarios 
                          JOIN niveles ON usuarios.ultimo_nivel = niveles.id_nivel
                          JOIN tipo_usuario ON usuarios.referencia_tipo = tipo_usuario.id_tipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public bool ExisteUsername(string username, int idExcluir)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

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
    }
}