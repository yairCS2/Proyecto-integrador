using DevyClass.Autenticacion;
using DevyClass.Base_de_datos_DevyClass_;
using DevyClass.UsuarioDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevyClass.UsuarioDB.DatosUsuario;

namespace DevyClass
{
    // Formulario de inicio de sesion: primera pantalla que ve el usuario.
    // Permite ingresar usuario y contrasena para entrar a la aplicacion,
    // o ir al formulario de registro si aun no tiene cuenta.
    public partial class UI_InicioSesion : Form
    {
        bool OjoRegistro; // Controla si la contrasena se ve o se oculta (mostrar/ocultar caracteres).
        public UI_InicioSesion()
        {
         
            InitializeComponent();
            OjoRegistro = false;
            // La contrasena se muestra con puntos "•" por defecto (oculta).
            txtcontrasenia.PasswordChar = '•';
            // El icono del ojo inicia en "cerrado" (contrasena oculta).
            OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
            // La tecla Enter activa el boton "Iniciar sesion" (mejor accesibilidad).
            this.AcceptButton = btninicia;

            // Fase 2: pantalla completa y estilo Duolingo.
            PrepararEstilo();
        }

        /// <summary>
        /// Unifica la columna de acceso: textos corregidos, campos del mismo ancho,
        /// boton principal de tamano completo y tarjeta de seguridad. Se ejecuta
        /// antes de Tema.Aplicar para que el bloque se centre con su tamaño final.
        /// </summary>
        private void PrepararEstilo()
        {
            // --- Textos (marca, acentos y enlaces que antes estaban duplicados) ---
            lbtitulo.Text = "DevyClass";
            lbdescripcion.Text = "Aprende programación a tu propio ritmo";
            lbtituloseguridad.Text = "Tu información está protegida";
            lbinfoseguridad.Text = "Nos tomamos en serio la seguridad de tus datos. Nunca compartiremos tu información.";
            lbnousuario.Text = "¿No tienes usuario?";
            linkLbregistro.Text = "Regístrate";
            btninicia.Text = "Iniciar sesión";

            Tema.Titulo(lbtitulo);
            Tema.Campo(lbusuario);
            Tema.Campo(lbcontraseña);
            lbdescripcion.ForeColor = Tema.TextoSuave;

            // La tarjeta de seguridad: sin fondo azur, texto que envuelve y tonos verdes.
            lbtituloseguridad.BackColor = Color.Transparent;
            lbtituloseguridad.ForeColor = Tema.VerdeOscuro;
            lbtituloseguridad.Font = Tema.Fuente(9F, FontStyle.Bold);
            lbtituloseguridad.SetBounds(78, 7, 168, 15);
            lbinfoseguridad.BackColor = Color.Transparent;
            lbinfoseguridad.ForeColor = Tema.TextoMedio;
            lbinfoseguridad.AutoSize = false;
            lbinfoseguridad.Font = Tema.Fuente(8F, FontStyle.Regular);
            lbinfoseguridad.SetBounds(78, 25, 168, 44);

            // --- Columna unica: etiquetas y campos comparten el mismo borde izquierdo ---
            const int x = 76;
            const int anchoCampo = 253;
            // La caja de contrasena se deja mas estrecha para que el icono del ojo
            // quede A SU DERECHA y no encima del campo (antes se superponian).
            const int anchoOjo = 48;
            const int huecoOjo = 8;
            const int anchoContrasena = anchoCampo - anchoOjo - huecoOjo;

            lbusuario.Left = x;
            lbcontraseña.Left = x;
            txtusuario.SetBounds(x, 186, anchoCampo, 30);
            txtcontrasenia.SetBounds(x, 234, anchoContrasena, 30);
            OjoContrasenia.SetBounds(x + anchoContrasena + huecoOjo, 234, anchoOjo, 30);

            // Boton principal al ancho de la columna y con altura generosa.
            btninicia.SetBounds(x, 302, anchoCampo, 44);

            lbnousuario.SetBounds(x, 360, lbnousuario.Width, lbnousuario.Height);
            pnlinfo.SetBounds(x, 392, anchoCampo, 74);

            // --- Controles sin logica: se ocultan para no prometer funciones inexistentes ---
            // (Recordarme, "Olvidaste tu contraseña" y etiquetas vacias no hacen nada.)
            Tema.Ocultar(chcRecordarme, linkLabel1, lbolvidarcontra, label1);

            this.PerformLayout();

            // --- Pantalla completa, centrado, tipografias y colores generales ---
            // El diseno original mide ~271x443 px: en un monitor de 1920 se ve pequeño,
            // así que se agranda antes de centrarlo (tamaño y tipografía a la vez).
            Tema.EscalarHijos(this, 1.4F);
            Tema.Aplicar(this, null, "DevyClass - Iniciar sesión");

            // Boton principal estilo Duolingo (verde) y tarjeta verde.
            Tema.Boton(btninicia, Tema.Rol.Primario);
            Tema.Tarjeta(pnlinfo, Tema.VerdeClaro, Tema.Verde);

            // El alto definitivo del campo lo decide la fuente (mas bajo que el
            // PictureBox): se deja el ojo con esa misma altura para que ambos
            // queden en la misma fila. El ancho no se toca para que el icono
            // siga terminando en el borde derecho de la columna.
            OjoContrasenia.Top = txtcontrasenia.Top;
            OjoContrasenia.Height = txtcontrasenia.Height;

            // "¿No tienes usuario?" y "Regístrate" forman un solo bloque: se centran
            // juntos en la columna y a la misma altura. Se hace aqui porque las
            // medidas ya estan escaladas por Tema.EscalarHijos.
            int separacion = 12;
            int anchoBloque = lbnousuario.Width + separacion + linkLbregistro.Width;
            int inicio = lbnousuario.Left + (btninicia.Width - anchoBloque) / 2;

            lbnousuario.Left = inicio;
            linkLbregistro.Left = inicio + lbnousuario.Width + separacion;
            linkLbregistro.Top = lbnousuario.Top + (lbnousuario.Height - linkLbregistro.Height) / 2;
        }

        private void UI_InicioSesion_Load(object sender, EventArgs e)
        {
            // Se ejecuta cuando la ventana se carga. (Vacio, no hace nada por ahora.)
        }

        // Evento del boton "Iniciar sesion".
        private void btninicia_Click(object sender, EventArgs e)
        {
            

            try
            {
                string nombre = txtusuario.Text;
                string contrasenia = txtcontrasenia.Text;
                // Valida que ningun campo este vacio.
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contrasenia))
                {
                    MessageBox.Show("Complete el usuario y la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }

            // Se valida el usuario y contrasena contra la base de datos.
            ValidarContraseniaYUsuario validar = new ValidarContraseniaYUsuario();
            if (validar.UsuarioyContraseniaCorrectos(txtusuario.Text, txtcontrasenia.Text))
            {
                // Si los datos son correctos, se obtiene el usuario completo de la BD
                // y se abre el menu principal pasandole ese usuario.
                ConsultasUsuario dao = new ConsultasUsuario();
                DatosUsuario usuarioActual = dao.ObtenerUsuarioPorUsername(txtusuario.Text);
                this.Hide();
                UI_MenuPrincipal.AbrirMenu(usuarioActual);
                return;
            }
            else
            {
                // Si no coinciden, se avisa al usuario.
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         
           
        }

        // Evento del link "Registrarse": lleva al formulario de registro.
        private void linkLbregistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI_Registro R = new UI_Registro();
            this.Hide();
            R.Show();
        }

        // Evento del icono del ojo: muestra u oculta la contrasena.
        private void OjoContrasenia_Click(object sender, EventArgs e)
        {
            OjoRegistro = !OjoRegistro; // Cambia el estado.
            if (OjoRegistro)
            {
                // Mostrar contrasena: icono de ojo abierto y sin caracter de ocultamiento.
                OjoContrasenia.Image = Properties.Resources.ojo_abierto;
                txtcontrasenia.PasswordChar = default;
            }
            else
            {
                // Ocultar contrasena: icono de ojo cerrado y puntos.
                OjoContrasenia.Image = Properties.Resources.ojo_cerrado;
                txtcontrasenia.PasswordChar = '•';
            }
        }

        private void lbtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
