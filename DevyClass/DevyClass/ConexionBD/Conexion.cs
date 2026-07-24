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
                var conn = new MySqlConnection(cadena);
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
