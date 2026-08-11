using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevyClass.Base_de_datos_DevyClass_;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace DevyClass.Autenticacion
{
    // Clase encargada de registrar un usuario nuevo en la base de datos.
    // NOTA: esta clase esta duplicada en ConsultasUsuario.RegistrarUsuario (que es la que se usa de verdad
    // desde los formularios UI_Registro y UI_AgregarUsuario).
    internal class RegistrarUsuario
    {
        // Inserta un usuario nuevo y devuelve true si se registro correctamente.
        public bool RegistarUsuario(string usuario, string correo, string fecha, string contrasenia)
        {
            Conexion conexion = new Conexion();
            try
            {
                // using: se asegura de cerrar la conexion automaticamente aunque ocurra un error.
                using (MySqlConnection con = conexion.ObtenerConexion())
                {
                    con.Open();
                    // Consulta SQL que inserta al usuario.
                    // referencia_tipo = 2 (usuario normal) y ultimo_nivel = 1 (empieza en el nivel 1).
                    string consulta = "INSERT INTO usuarios (username, Correo, fecha, contrasena, referencia_tipo, ultimo_nivel) VALUES (@nombre,@correo,@fecha,@contrasenia, 2, 1);";
                    MySqlCommand enviar = new MySqlCommand(consulta, con);
                    // Se agregan los valores a los parametros @nombre, @correo, etc.
                    // OJO: aqui hay un error conocido: todos los campos se llenan con "usuario".
                    // Deberian usarse "usuario", "correo", "fecha" y "contrasenia" respectivamente.
                    enviar.Parameters.AddWithValue("@nombre", usuario);
                    enviar.Parameters.AddWithValue("@correo", usuario);
                    enviar.Parameters.AddWithValue("@fecha", usuario);
                    enviar.Parameters.AddWithValue("@contrasenia", usuario);
                    
                    // ExecuteNonQuery ejecuta el INSERT y devuelve cuantas filas se insertaron.
                    int filas_afectadas = enviar.ExecuteNonQuery();
                    if (filas_afectadas > 0)
                    {
                        MessageBox.Show("Usuario registrado correctamente");
                        return true;
                    } else { MessageBox.Show("no se pudo registrar el usuario"); return false; }



                }


            }
            catch (Exception ex)
            {
                {
                    // Si falla la conexion o la consulta, mostramos el error.
                    MessageBox.Show(ex.Message);
                    return false;

                }

            }


        }
    }
}
