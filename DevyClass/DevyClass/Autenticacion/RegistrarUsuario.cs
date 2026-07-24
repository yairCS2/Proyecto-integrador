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
    internal class RegistrarUsuario
    {
        public bool RegistarUsuario(string usuario, string correo, string fecha, string contrasenia)
        {
            Conexion conexion = new Conexion();
            try
            {
                using (MySqlConnection con = conexion.ObtenerConexion())
                {
                    con.Open();
                    string consulta = "INSERT INTO usuarios (username, Correo, fecha, contrasena, referencia_tipo, ultimo_nivel) VALUES (@nombre,@correo,@fecha,@contrasenia, 2, 1);";
                    MySqlCommand enviar = new MySqlCommand(consulta, con);
                    enviar.Parameters.AddWithValue("@nombre", usuario);
                    enviar.Parameters.AddWithValue("@correo", usuario);
                    enviar.Parameters.AddWithValue("@fecha", usuario);
                    enviar.Parameters.AddWithValue("@contrasenia", usuario);
                    
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
                    MessageBox.Show(ex.Message);
                    return false;

                }

            }


        }
    }
}
