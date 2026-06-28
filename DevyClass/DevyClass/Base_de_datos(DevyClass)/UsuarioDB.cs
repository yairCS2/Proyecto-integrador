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
                        
                    //creamos una variable llamada query a la cual se le da la instruccion en lenguaje sql de lo que queremos modificar en la tabla de nuestra base de datos
                    //en pocas palabras hacer una consulta (modificar,crear,actualizar) etc..;
                    string query = "UPDATE usuarios SET NameUsuarios = @nombre WHERE Correo = @correo";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //le damos valor a los parmetros que hicimos en la sonsulta por medio de los parametros qeu recibe el metodo Editar usuario;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@nombre", nuevoNombre);


                    MessageBox.Show("Confirme si el correo es correcto para editar su usuario\nCorreo: " + correo + "\nNombre: " + nuevoNombre);
                    //ejecuta la culsuta sql que esta en cmd
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception tipoerror)
            {
                MessageBox.Show("Error al editar usuario: " + tipoerror.Message);
            }
        }
    }
}