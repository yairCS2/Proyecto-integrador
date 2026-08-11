using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevyClass.Formularios_UI_niveles.Modulo_1;

namespace DevyClass
{
    // Clase principal que arranca la aplicacion.
    // Es "static" porque no se crean instancias de ella, solo se usa su metodo Main.
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicacion.
        /// Es el metodo que ejecuta Windows al abrir el programa (.exe).
        /// </summary>
        [STAThread] // Indica que la app usa "Single Thread Apartment": necesario para usar formularios (Windows Forms).
        static void Main()
        {
            // Habilita los estilos visuales del sistema (bordes, botones y temas modernos de Windows).
            Application.EnableVisualStyles();
            // Hace que el texto se dibuje con la tecnica de renderizado GDI por defecto del sistema.
            Application.SetCompatibleTextRenderingDefault(false);
            // Abre la ventana principal de la aplicacion: la pantalla de inicio de sesion.
            // El programa permanece abierto hasta que esta ventana se cierre.
            Application.Run(new UI_InicioSesion());
        }
    }
}
