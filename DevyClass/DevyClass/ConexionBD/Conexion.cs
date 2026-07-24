using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace DevyClass.Base_de_datos_DevyClass_
{

    public class Conexion
    {
        private string cadena = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=;";
        private string cadena2 = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=1234;";

        public MySqlConnection ObtenerConexion()
        {
            try
            {
                //no va a entrar en el catch por que aqui solo establece la conexion no compurueba si el servidor existe o si la contrraseña es correcta 
                var conn = new MySqlConnection(cadena);
                //cuando abrimos la conexion aqui si compueba todo y si algo esta mal ahora si lo atrapa el catch de tipo mysqlconnection 
                // porue si es un error de conexion lo que pasaba cunado no tenia el open y el close lo atrapaba la excepcion de alguna consulta en otra clase y no la de este metodo ObtenerConexion()
                //entonces por eso jamas se ejecutaba la excepcion de aqui :)
                conn.Open();
                conn.Close();
                return conn;

              
                
            }
            catch (MySqlException)
            {
                return new MySqlConnection(cadena2);
            }
        }

        public void verificarConecxion()
        {
            try
            {
                using (MySqlConnection conn = ObtenerConexion())
                {
                    conn.Open();
                    MessageBox.Show("Conexion exitosa¡¡¡");
                }

            }
            catch (Exception tipoerror)
            {
                MessageBox.Show(tipoerror.Message);

            }

        }

    }

}
