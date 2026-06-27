using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace DevyClass.Base_de_datos_DevyClass_
{

    public class Conexion
    {
        private string cadenaConexion =
            "Server=localhost;Database=DevyClass;Uid=root;Pwd=TU_CONTRASEÑA;";

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
