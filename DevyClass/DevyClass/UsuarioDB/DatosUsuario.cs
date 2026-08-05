using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevyClass.UsuarioDB
{
    public class DatosUsuario
    {
        public DatosUsuario() { }

        // solo usename
       

        // Todos los datos del usuario.
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Correo { get; set; }
        public DateTime Fecha { get; set; }
        public string Contrasena { get; set; }
        public int ReferenciaTipo { get; set; }
        public int? UltimoNivel { get; set; }

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
