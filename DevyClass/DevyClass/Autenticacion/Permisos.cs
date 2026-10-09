using System.Windows.Forms;
using DevyClass.UsuarioDB;

namespace DevyClass.Autenticacion
{
    // Control de acceso por rol.
    //
    // ANTES: los formularios de administracion (UI_Administrador, UI_AgregarUsuario,
    // UI_EliminarUsuario y UI_GestionarNiveles) eran "public" y NO validaban nada.
    // La unica proteccion era que el boton del menu se ocultaba para los usuarios
    // normales, lo cual no impide nada: cualquier ruta de codigo puede abrir el panel.
    //
    // AHORA: cada formulario de administracion llama a ExigirAdministrador() y se
    // cierra si el usuario no tiene el rol de administrador.
    internal static class Permisos
    {
        // Valores de la tabla tipo_usuario.
        public const int TIPO_ADMINISTRADOR = 1;
        public const int TIPO_USUARIO_NORMAL = 2;

        /// <summary>
        /// Indica si el usuario es administrador. Un usuario null no es administrador.
        /// </summary>
        public static bool EsAdministrador(DatosUsuario usuario)
        {
            return usuario != null && usuario.ReferenciaTipo == TIPO_ADMINISTRADOR;
        }

        /// <summary>
        /// Exige rol de administrador para abrir un formulario.
        /// Devuelve true si el usuario puede continuar; si no, avisa y cierra el formulario.
        /// Se invoca desde el evento Shown, porque en el constructor el formulario
        /// todavia no esta visible y Close() no lo cerraria bien.
        /// </summary>
        public static bool ExigirAdministrador(Form formulario, DatosUsuario usuario)
        {
            if (EsAdministrador(usuario))
            {
                return true;
            }

            MessageBox.Show(
                formulario,
                "Esta seccion es unicamente para administradores.",
                "Acceso denegado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            formulario.Close();
            return false;
        }
    }
}