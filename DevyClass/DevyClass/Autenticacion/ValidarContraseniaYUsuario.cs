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
    internal class ValidarContraseniaYUsuario
    {
        public bool UsuarioyContraseniaCorrectos(string usuario,string contrasena)
        {
            try
            {
                Conexion conexion = new Conexion();
                using(MySqlConnection con = conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM usuarios WHERE usuarios.username = @usuario AND usuarios.contrasena = @contrasena;";
                    MySqlCommand enviar = new MySqlCommand(sql,con);
                    enviar.Parameters.AddWithValue("@usuario", usuario);
                    enviar.Parameters.AddWithValue("@contrasena", contrasena);
                    //guardamos si encontro la consulta con un onjeto o variable referenciada
                    MySqlDataReader reader = enviar.ExecuteReader();
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
