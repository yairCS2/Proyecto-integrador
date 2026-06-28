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
        private string cadena = "Server=localhost;Database=devyclass;Uid=root;Pwd=1234;";

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadena);
        }
    }

}
