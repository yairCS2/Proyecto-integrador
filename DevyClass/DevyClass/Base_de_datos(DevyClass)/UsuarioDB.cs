using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DevyClass.Base_de_datos_DevyClass_
{
    internal class UsuarioDB
    {

        public void ProbarConexion()
        {
            Conexion conexion = new Conexion();
            MySqlConnection con = conexion.ObtenerConexion();
            con.Open();


            con.Close();
        }
    }
} 
