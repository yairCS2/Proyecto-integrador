using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace DevyClass.Base_de_datos_DevyClass_
{

    // Clase encargada de crear las conexiones con la base de datos MySQL.
    // Todas las demas clases de acceso a datos (ConsultasUsuario, etc.) usan este objeto.
    public class Conexion
    {
        // Cadena de conexion principal: servidor local (localhost), base de datos DevyClassBD,
        // usuario "root" y SIN contrasena.
        private string cadena = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=;";
        // Cadena de conexion de respaldo: igual pero con contrasena "1234".
        // Se usa si la primera falla (por ejemplo, si la instalacion de MySQL tiene clave).
        private string cadena2 = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=1234;";

        // Devuelve una conexion lista para usarse.
        // Intenta con la cadena sin contrasena y, si eso falla, devuelve una con la de respaldo.
        public MySqlConnection ObtenerConexion()
        {
            try
            {
                //no va a entrar en el catch por que aqui solo establece la conexion no compurueba si el servidor existe o si la contrraseña es correcta 
                var conn = new MySqlConnection(cadena);
                //cuando abrimos la conexion aqui si compueba todo y si algo esta mal ahora si lo atrapa el catch de tipo mysqlconnection 
                // porue si es un error de conexion lo que pasaba cunado no tenia el open y el close lo atrapaba la excepcion de alguna consulta en otra clase y no la de este metodo ObtenerConexion()
                //entonces por eso jamas se ejecutaba la excepcion de aqui :)
                conn.Open();   // Abre la conexion para VERIFICAR que las credenciales son validas.
                conn.Close();  // La cierra de inmediato (solo queria comprobar); el que la use la abrira de nuevo.
                return conn;   // Devuelve la conexion (cerrada pero configurada) para que el llamador la use.

              
                
            }
            catch (MySqlException)
            {
                // Si el intento con "cadena" fallo (por la contrasena), se devuelve una conexion con "cadena2".
                return new MySqlConnection(cadena2);
            }
        }

        // Metodo de prueba: abre la conexion y muestra un mensaje si todo funciona.
        // Se usa en el boton "verificar conexion" del menu principal (solo administradores).
        public void verificarConecxion()
        {
            try
            {
                using (MySqlConnection conn = ObtenerConexion())
                {
                    conn.Open(); // Si llega aqui, la conexion se establecio correctamente.
                    MessageBox.Show("Conexion exitosa¡¡¡");
                }

            }
            catch (Exception tipoerror)
            {
                // Si algo falla al conectar, mostramos el error al usuario.
                MessageBox.Show(tipoerror.Message);

            }

        }

    }

}
