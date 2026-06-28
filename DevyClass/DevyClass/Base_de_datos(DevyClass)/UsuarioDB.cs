using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DevyClass.Base_de_datos_DevyClass_
{
    public class UsuarioDB
    {
        Conexion conexion = new Conexion();

        public void EditarUsuario(string correo, string nuevoNombre)
        {
            try
            {   //using (palabre reservada) incica o le dice a la condicional usa este recurso y en donde terminen las llaves cierralo automaticamente3
                //por eso usamos el nombre del objeto .open() para abrirlo y ya no es necesario poner con.close();
                using (MySqlConnection con = conexion.ObtenerConexion())
                {
                    //on.Open(); abre la communicacion entre c# y el servidor de maria db
                    con.Open();
                    //creamos una variable llamada query a la cual se le da la instruccion en lenguaje sql de lo que queremos hacer en la base de datos qeu tenemos en mariadb
                    string query = "UPDATE usuarios SET NameUsuarios = @nombre WHERE Correo = @correo";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@nombre", nuevoNombre);


                    MessageBox.Show("Correo: " + correo + "\nNombre: " + nuevoNombre);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar usuario: " + ex.Message);
            }
        }
    }
}