using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevyClass.UsuarioDB
{
    // Clase que representa a un usuario de la aplicacion.
    // Es un "modelo" de datos: guarda en memoria la informacion del usuario
    // para pasarla entre los formularios (por ejemplo, del login al menu principal).
    public class DatosUsuario
    {
        public DatosUsuario() { }

        // solo usename
       

        // Todos los datos del usuario. Cada propiedad corresponde a una columna de la tabla "usuarios".
        public int IdUsuario { get; set; }        // Identificador unico del usuario (PK).
        public string Username { get; set; }      // Nombre de usuario con el que inicia sesion.
        public string Correo { get; set; }        // Correo electronico del usuario.
        public DateTime Fecha { get; set; }       // Fecha de nacimiento del usuario.
        public string Contrasena { get; set; }    // Contrasena del usuario (se guarda sin encriptar).
        public int ReferenciaTipo { get; set; }   // Tipo de usuario: 1 = Administrador, 2 = Normal.
        public int? UltimoNivel { get; set; }     // Ultimo nivel alcanzado (nullable: puede ser null si aun no tiene).

        // Vacia todos los datos del objeto.
        // Se usa al cerrar sesion para no dejar datos del usuario en memoria.
        public void BorrarDatos()
        {
            IdUsuario = 0;
            Username = null;
            Correo = null;
            Fecha = default(DateTime);
            Contrasena = null;
            ReferenciaTipo = 0;
            UltimoNivel = null;
        }
    }
}
