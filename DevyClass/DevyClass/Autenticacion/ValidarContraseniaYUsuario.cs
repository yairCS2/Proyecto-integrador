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
    // Clase encargada de validar el inicio de sesion de un usuario.
    internal class ValidarContraseniaYUsuario
    {
        // Verifica en la base de datos si existe un usuario con el nombre y la contrasena dados.
        // Devuelve true si coinciden, false en caso contrario.
        public bool UsuarioyContraseniaCorrectos(string usuario,string contrasena)
        {
            try
            {
                Conexion conexion = new Conexion();
                using(MySqlConnection con = conexion.ObtenerConexion())
                {
                    con.Open();
                    // Consulta SQL: busca una fila donde el username Y la contrasena coincidan.
                    string sql = "SELECT * FROM usuarios WHERE usuarios.username = @usuario AND usuarios.contrasena = @contrasena;";
                    MySqlCommand enviar = new MySqlCommand(sql,con);
                    // Se usan parametros @usuario y @contrasena para evitar inyeccion SQL.
                    enviar.Parameters.AddWithValue("@usuario", usuario);
                    enviar.Parameters.AddWithValue("@contrasena", contrasena);
                    //guardamos si encontro la consulta con un onjeto o variable referenciada
                    MySqlDataReader reader = enviar.ExecuteReader();
                    // reader.Read() devuelve true si la consulta encontro al menos una fila.
                    if (reader.Read())
                    {
                        return true;
                    }else { return false; }
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;

            }

            
        }




    }
}
