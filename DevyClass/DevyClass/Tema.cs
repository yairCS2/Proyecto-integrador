using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI.WinForms;

namespace DevyClass
{
    /// <summary>
    /// Sistema de diseño de la aplicacion (estilo Duolingo).
    /// Reunido en una sola clase para que todos los formularios se vean igual:
    ///  - paleta de colores
    ///  - ventana normal, con barra de titulo y sus botones (minimizar,
    ///    maximizar y cerrar). Ya no se usa pantalla completa.
    ///  - centrado de contenido (el diseno original se conserva como un "escenario")
    ///  - botones con estilo (relleno y borde, sin sombra debajo)
    ///  - tablas, campos de texto, barras de progreso y tarjetas
    /// </summary>
    public static class Tema
    {
        // ------------------------------------------------------------------
        // Paleta (colores oficiales de Duolingo)
        // ------------------------------------------------------------------
        public static readonly Color Verde        = Color.FromArgb(88, 204, 2);     // #58CC02 principal
        public static readonly Color VerdeHover   = Color.FromArgb(97, 224, 3);     // #61E003
        public static readonly Color VerdeOscuro  = Color.FromArgb(70, 163, 2);     // #46A302 borde/oscuro
        public static readonly Color VerdeClaro   = Color.FromArgb(229, 249, 208);  // #E5F9D0
        public static readonly Color Azul         = Color.FromArgb(28, 176, 246);   // #1CB0F6
        public static readonly Color AzulOscuro   = Color.FromArgb(24, 153, 214);   // #1899D6
        public static readonly Color AzulClaro    = Color.FromArgb(221, 244, 255);  // #DDF4FF
        public static readonly Color Rojo         = Color.FromArgb(255, 75, 75);    // #FF4B4B
        public static readonly Color RojoOscuro   = Color.FromArgb(225, 54, 54);    // #E13636
        public static readonly Color Amarillo     = Color.FromArgb(255, 200, 0);    // #FFC800
        public static readonly Color AmarilloOscuro = Color.FromArgb(214, 168, 0);
        public static readonly Color Morado       = Color.FromArgb(206, 130, 255);  // #CE82FF

        public static readonly Color TextoOscuro  = Color.FromArgb(60, 60, 60);     // #3C3C3C
        public static readonly Color TextoMedio   = Color.FromArgb(75, 75, 75);     // #4B4B4B
        public static readonly Color TextoSuave   = Color.FromArgb(119, 119, 119);  // #777777
        public static readonly Color Borde        = Color.FromArgb(229, 229, 229);  // #E5E5E5
        public static readonly Color Blanco       = Color.White;
        public static readonly Color Fondo        = Color.White;
        public static readonly Color FondoGris    = Color.FromArgb(247, 247, 247);  // #F7F7F7

        public enum Rol { Primario, Secundario, Peligroso, ContornoPeligroso, Amarillo, Azul, Fantasma }

        // Esquinas redondeadas (la region no cambia al redimensionar, por eso
        // solo se aplica a controles con tamaño fijo).
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void Redondear(Control c, int radio)
        {
            if (c.Width <= 0 || c.Height <= 0 || radio <= 0) return;
            c.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, c.Width + 1, c.Height + 1, radio * 2, radio * 2));
        }

        public static Font Fuente(float tam, FontStyle estilo)
        {
            return new Font("Segoe UI", tam, estilo, GraphicsUnit.Point);
        }

        // ------------------------------------------------------------------
        // Preparacion general de un formulario
        // ------------------------------------------------------------------
        /// <summary>
        /// true cuando el codigo se ejecuta dentro del diseñador de Visual Studio.
        /// En ese caso no se aplica el estilo: si no, abrir un formulario en el
        /// diseñador redimensionaria la ventana y guardaria el diseno cambiado.
        /// </summary>
        private static bool EnDiseno
        {
            get { return System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime; }
        }

        /// <param name="formulario">Formulario a preparar.</param>
        /// <param name="escenario">
        /// Panel que contiene todo el diseno (si el contenido esta dentro de un panel
        /// con Dock=Fill, se desancla y se centra como un bloque, conservando el diseno).
        /// Si es null se centran los controles hijos directos del formulario.
        /// </param>
        /// <param name="titulo">Texto de la ventana (barra de titulo y Alt+Tab).</param>
        /// <param name="centrar">Centra el contenido dentro de la ventana.</param>
        /// <param name="accionCerrar">
        /// Que hace la "X" de la barra de titulo. Si se omite, la ventana se cierra
        /// normalmente (la ultima ventana cerrada apaga la aplicacion).
        /// Modulo y Nivel la usan para volver al menu en vez de salir del programa.
        /// </param>
        public static void Aplicar(Form formulario, Control escenario = null, string titulo = null,
                                   bool centrar = true, Action accionCerrar = null)
        {
            if (EnDiseno) return;

            VentanaNormal(formulario, titulo);

            // Se estiliza ANTES de medir y centrar, para que la ventana calcule su
            // tamano con las fuentes y los colores ya en su sitio.
            EstilizarControles(formulario);   // fuentes, campos, tablas, botones genericos

            if (escenario != null || centrar)
            {
                // 1) Se fijan los anclajes de lo que se va a centrar: asi, al cambiar
                //    el tamano de la ventana, WinForms no mueve los controles por su
                //    cuenta mientras se esta midiendo.
                if (escenario != null)
                {
                    escenario.Dock = DockStyle.None;
                    escenario.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }
                else
                {
                    Desanclar(formulario);
                }

                AjustarTamano(formulario);                     // la ventana se acomoda al contenido
                Centrar(formulario, escenario);                // y el contenido queda en el centro

                // La ventana ya no ocupa toda la pantalla: al cambiar de tamano
                // (maximizar, restaurar, estirar) el contenido se vuelve a centrar.
                formulario.Resize += (s, e) =>
                {
                    if (formulario.WindowState == FormWindowState.Minimized) return;
                    Centrar(formulario, escenario);
                };
            }

            // Se suscribe ANTES que SalirSiNoQuedanVentanas: si la "X" cancela el
            // cierre, la aplicacion no debe apagarse.
            if (accionCerrar != null) AccionAlCerrar(formulario, accionCerrar);

            SalirSiNoQuedanVentanas(formulario);
        }

        /// <summary>
        /// Ventana normal de Windows: barra de titulo con los botones de minimizar,
        /// maximizar y cerrar. Ya no se usa pantalla completa.
        /// </summary>
        private static void VentanaNormal(Form formulario, string titulo)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.FormBorderStyle = FormBorderStyle.Sizable;  // barra de titulo y bordes
            formulario.ControlBox = true;
            formulario.MinimizeBox = true;
            formulario.MaximizeBox = true;
            formulario.WindowState = FormWindowState.Normal;
            formulario.BackColor = Fondo;
            formulario.ForeColor = TextoOscuro;
            if (!string.IsNullOrEmpty(titulo)) formulario.Text = titulo;
        }

        // ------------------------------------------------------------------
        // Tamano de la ventana y centrado del contenido
        // ------------------------------------------------------------------
        /// <summary> Margen (px) que se deja alrededor del contenido de la ventana. </summary>
        private const int MargenVentana = 24;

        /// <summary>
        /// Hijos directos que participan en el centrado: los que no estan con Dock
        /// (esos se acomodan solos) y los que no estan marcados como fijos u ocultos.
        /// Visible no se tiene en cuenta: antes de mostrar el formulario vale false
        /// para todos los hijos, de modo que la lista es siempre la misma y el
        /// centrado no salta al redimensionar.
        /// </summary>
        private static List<Control> HijosACentrar(Form formulario)
        {
            return formulario.Controls.Cast<Control>()
                .Where(c => c.Dock == DockStyle.None)
                .Where(c => (c.Tag as string) != "fijo" && (c.Tag as string) != "oculto")
                .ToList();
        }

        /// <summary> Rectangulo que cubre a todos los controles dados. </summary>
        private static Rectangle Rectangulo(IList<Control> controles)
        {
            return Rectangle.FromLTRB(
                controles.Min(c => c.Left),
                controles.Min(c => c.Top),
                controles.Max(c => c.Right),
                controles.Max(c => c.Bottom));
        }

        /// <summary>
        /// Fija los anclajes de los hijos directos en Top|Left: asi no se recorran
        /// solos al cambiar el tamano de la ventana y quien los mueve es el centrado.
        /// Los que estan con Dock no se tocan (se acomodan solos).
        /// </summary>
        private static void Desanclar(Form formulario)
        {
            foreach (Control c in HijosACentrar(formulario))
            {
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// Ajusta el tamano de la ventana al contenido (por ejemplo al que se escalo
        /// para pantallas grandes), sin pasarse del area de trabajo del monitor.
        /// </summary>
        private static void AjustarTamano(Form formulario)
        {
            List<Control> hijos = HijosACentrar(formulario);
            if (hijos.Count == 0) return;   // todo esta con Dock: se respeta el diseno

            Rectangle contenido = Rectangulo(hijos);
            Size deseado = new Size(contenido.Width + 2 * MargenVentana,
                                    contenido.Height + 2 * MargenVentana);

            // Nunca mas pequeno que el tamano de diseno.
            deseado.Width = Math.Max(deseado.Width, formulario.ClientSize.Width);
            deseado.Height = Math.Max(deseado.Height, formulario.ClientSize.Height);

            formulario.ClientSize = deseado;

            // Nunca mas grande que el area de trabajo: asi se siguen viendo la barra
            // de tareas y los botones de la barra de titulo. El marco (bordes +
            // barra de titulo) se descuenta con el tamano real de la ventana.
            Size marco = formulario.Size - formulario.ClientSize;
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            int maxAncho = Math.Max(320, area.Width - marco.Width);
            int maxAlto = Math.Max(240, area.Height - marco.Height);

            if (formulario.ClientSize.Width > maxAncho || formulario.ClientSize.Height > maxAlto)
            {
                formulario.ClientSize = new Size(Math.Min(formulario.ClientSize.Width, maxAncho),
                                                 Math.Min(formulario.ClientSize.Height, maxAlto));
            }
        }

        /// <summary> Centra el contenido con el metodo que toque en Aplicar(). </summary>
        private static void Centrar(Form formulario, Control escenario)
        {
            if (escenario != null) CentrarEscenario(formulario, escenario);
            else CentrarContenido(formulario);
        }

        /// <summary> Desancla el panel y lo centra en la ventana conservando su diseno. </summary>
        public static void CentrarEscenario(Form formulario, Control escenario)
        {
            escenario.Dock = DockStyle.None;
            escenario.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Size objetivo = formulario.ClientSize;
            int x = Math.Max(0, (objetivo.Width - escenario.Width) / 2);
            int y = Math.Max(0, (objetivo.Height - escenario.Height) / 2);
            escenario.Location = new Point(x, y);
        }

        /// <summary>
        /// Centra el bloque formado por los controles hijos directos (sin tocar los
        /// anclados con Dock, que se encargan solos). Todos se mueven con la misma
        /// distancia, asi el diseno no se descuadra.
        /// </summary>
        public static void CentrarContenido(Form formulario)
        {
            List<Control> hijos = HijosACentrar(formulario);
            if (hijos.Count == 0) return;

            // Al quitar el Anchor los controles dejan de seguir a los bordes de la
            // ventana: quien los vuelve a centrar es este metodo (al abrir y cada
            // vez que se cambia el tamano de la ventana).
            foreach (Control c in hijos)
            {
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            Rectangle contenido = Rectangulo(hijos);
            Size objetivo = formulario.ClientSize;
            int dx = (objetivo.Width - contenido.Width) / 2 - contenido.Left;
            int dy = (objetivo.Height - contenido.Height) / 2 - contenido.Top;

            foreach (Control c in hijos)
            {
                c.Location = new Point(c.Left + dx, c.Top + dy);
            }
        }

        /// <summary>
        /// Si al cerrar un formulario no queda ninguna ventana visible, la aplicacion
        /// termina. Evita el "proceso zombie" (app viva sin ventanas) que existia
        /// porque todas las navegaciones usaban Hide() en vez de Close().
        /// </summary>
        private static void SalirSiNoQuedanVentanas(Form formulario)
        {
            formulario.FormClosing += (s, e) =>
            {
                // e.Cancel: si AccionAlCerrar() cancelo el cierre (la "X" tenia una
                // accion propia), no hay que apagar la aplicacion.
                if (e.CloseReason != CloseReason.UserClosing || e.Cancel) return;
                bool quedaAlguna = Application.OpenForms.Cast<Form>().Any(f => f != formulario && f.Visible);
                if (!quedaAlguna) Application.Exit();
            };
        }

        /// <summary>
        /// La "X" de la barra de titulo ejecuta la accion propia del formulario
        /// (por ejemplo volver al menu) en vez de cerrar la ventana.
        /// Solo se usa cuando no hay otra ventana visible: si la hay, el cierre viene
        /// de la navegacion del programa y se deja pasar tal cual.
        /// </summary>
        private static void AccionAlCerrar(Form formulario, Action accionCerrar)
        {
            bool ejecutada = false;

            formulario.FormClosing += (s, e) =>
            {
                if (e.CloseReason != CloseReason.UserClosing || ejecutada) return;

                bool otraVentanaVisible = Application.OpenForms.Cast<Form>()
                    .Any(f => f != formulario && f.Visible);
                if (otraVentanaVisible) return;   // la navegacion ya dejo lista la ventana siguiente

                ejecutada = true;
                e.Cancel = true;   // aun no se cierra: la accion se encarga de cerrarla
                formulario.BeginInvoke((Action)(() => accionCerrar()));
            };
        }

        // ------------------------------------------------------------------
        // Recorrido general de controles (fuentes, campos, tablas, botones)
        // ------------------------------------------------------------------
        // ------------------------------------------------------------------
        // Escalado para pantallas grandes
        // ------------------------------------------------------------------
        /// <summary>
        /// Agranda un bloque de controles (posición, tamaño y tipografía) para que
        /// el diseno aproveche las pantallas grandes. Se aplica ANTES de centrar.
        /// Control.Scale() no toca las fuentes, por eso aquí el texto se escala a mano.
        /// </summary>
        /// <param name="desanclarRaiz">
        /// true (por defecto): el bloque se desancla y se coloca a mano. false: se usa
        /// cuando el hijo esta con Dock y quien lo posiciona es su propio padre.
        /// </param>
        public static void Escalar(Control raiz, float factor, bool desanclarRaiz = true)
        {
            if (EnDiseno) return;
            if (raiz == null || Math.Abs(factor - 1F) < 0.001F) return;

            // 1) Se guarda la geometria ORIGINAL de todo el arbol. Al redimensionar un
            //    padre, WinForms recoloca a los hijos con anclaje Bottom/Right; si se
            //    leyeran sus posiciones despues, saldrian desplazadas dos veces.
            var niveles = new List<NivelEscala>();
            CapturarNiveles(raiz, niveles);

            // 2) Se agranda el propio bloque.
            if (desanclarRaiz && raiz.Dock != DockStyle.None) raiz.Dock = DockStyle.None;

            if (raiz.Dock == DockStyle.None)
            {
                raiz.SetBounds(
                    (int)Math.Round(raiz.Left * factor),
                    (int)Math.Round(raiz.Top * factor),
                    (int)Math.Round(raiz.Width * factor),
                    (int)Math.Round(raiz.Height * factor));
            }
            else if (raiz.Dock == DockStyle.Top || raiz.Dock == DockStyle.Bottom)
            {
                raiz.Height = (int)Math.Round(raiz.Height * factor); // solo cambia el grosor
            }
            else if (raiz.Dock == DockStyle.Left || raiz.Dock == DockStyle.Right)
            {
                raiz.Width = (int)Math.Round(raiz.Width * factor);
            }

            EscalarFuente(raiz, factor);

            // 3) Se reescribe cada hijo con su geometria original ya escalada, nivel por
            //    nivel y de arriba hacia abajo (el padre siempre antes que los hijos,
            //    asi un redimensionamiento intermedio no arruina las posiciones).
            foreach (NivelEscala nivel in niveles)
            {
                for (int i = 0; i < nivel.Hijos.Length; i++)
                {
                    Control hijo = nivel.Hijos[i];
                    Rectangle g = nivel.Geometria[i];

                    if (hijo.Dock == DockStyle.Top || hijo.Dock == DockStyle.Bottom)
                    {
                        hijo.Height = (int)Math.Round(g.Height * factor);
                    }
                    else if (hijo.Dock == DockStyle.Left || hijo.Dock == DockStyle.Right)
                    {
                        hijo.Width = (int)Math.Round(g.Width * factor);
                    }
                    else if (hijo.Dock == DockStyle.Fill)
                    {
                        // Dock.Fill lo recalcula el padre: no hay nada que mover.
                    }
                    else
                    {
                        hijo.SetBounds(
                            (int)Math.Round(g.X * factor),
                            (int)Math.Round(g.Y * factor),
                            (int)Math.Round(g.Width * factor),
                            (int)Math.Round(g.Height * factor));
                    }

                    EscalarFuente(hijo, factor);
                }
            }
        }

        /// <summary> Un nivel del arbol: geometria original de los hijos de un contenedor. </summary>
        private sealed class NivelEscala
        {
            public Control[] Hijos;
            public Rectangle[] Geometria;
        }

        /// <summary> Guarda, de arriba hacia abajo, la geometria original de cada hijo. </summary>
        private static void CapturarNiveles(Control contenedor, List<NivelEscala> niveles)
        {
            var hijos = new List<Control>();
            var geom = new List<Rectangle>();

            foreach (Control h in contenedor.Controls)
            {
                hijos.Add(h);
                geom.Add(h.Bounds);
            }

            niveles.Add(new NivelEscala { Hijos = hijos.ToArray(), Geometria = geom.ToArray() });

            foreach (Control h in hijos)
            {
                if (h.HasChildren) CapturarNiveles(h, niveles);
            }
        }

        /// <summary> Escala todos los controles hijos directos de un contenedor. </summary>
        public static void EscalarHijos(Control contenedor, float factor)
        {
            if (EnDiseno) return;
            if (contenedor == null || Math.Abs(factor - 1F) < 0.001F) return;
            foreach (Control hijo in contenedor.Controls)
            {
                Escalar(hijo, factor, desanclarRaiz: false);
            }
        }

        /// <summary>
        /// Deja todos los controles anclados arriba-izquierda (de forma recursiva).
        ///
        /// Las pantallas de preguntas se dibujan sobre un lienzo fijo de 585x285 que
        /// luego se escala. Con Anchor=Bottom|Right, al insertar el UserControl con
        /// Dock=Fill WinForms lo agranda de 585x285 a 994x484 y desplaza a los hijos
        /// anclados por el delta: un boton de X=215 salia en X=622 y, al escalarlo,
        /// en X=1057, con el borde derecho en 1654 dentro de un panel de 992.
        /// Como el diseno usa posiciones absolutas, el anclaje no aporta nada aqui.
        /// </summary>
        public static void AnclarArribaIzquierda(Control contenedor)
        {
            if (EnDiseno) return;
            if (contenedor == null) return;
            foreach (Control c in contenedor.Controls)
            {
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                if (c.HasChildren) AnclarArribaIzquierda(c);
            }
        }

        private static void EscalarFuente(Control c, float factor)
        {
            Font f = c.Font;
            if (f == null) return;
            float nueva = Math.Max(6F, (float)Math.Round(f.Size * factor, 1));
            if (Math.Abs(nueva - f.Size) < 0.01F) return;
            c.Font = new Font(f.FontFamily, nueva, f.Style, GraphicsUnit.Point);
        }

        private static void EstilizarControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                EstilizarControl(c);
                if (c.HasChildren) EstilizarControles(c);
            }
        }

        /// <summary>
        /// Aplica el recorrido general (fuentes, colores, botones, tablas) a un
        /// contenedor cualquiera. Se usa en los UserControls de las preguntas, que
        /// se crean en tiempo de ejecucion y por eso no pasan por Aplicar().
        /// </summary>
        public static void EstilizarRaiz(Control contenedor)
        {
            if (EnDiseno) return;
            if (contenedor == null) return;
            EstilizarControl(contenedor);
            EstilizarControles(contenedor);
        }

        private static void EstilizarControl(Control c)
        {
            string marca = c.Tag as string;
            if (marca == "barra" || marca == "fijo" || marca == "no-estilo") return;

            // Fuentes: todo Segoe UI conservando el tamaño (para no descuadrar el diseno).
            Font actual = c.Font;
            if (actual != null && !actual.FontFamily.Name.StartsWith("Segoe UI"))
            {
                FontStyle estilo = actual.Style;
                c.Font = Fuente(actual.Size, estilo);
            }

            if (c is LinkLabel link)
            {
                link.LinkColor = Verde;
                link.ActiveLinkColor = VerdeOscuro;
                link.VisitedLinkColor = Verde;
                link.ForeColor = Verde;
            }
            else if (c is Label etiqueta)
            {
                if (etiqueta.ForeColor == Color.Black) etiqueta.ForeColor = TextoOscuro;
            }
            else if (c is TextBox campo)
            {
                campo.BackColor = Blanco;
                campo.ForeColor = TextoOscuro;
                campo.BorderStyle = BorderStyle.FixedSingle;
                campo.Cursor = Cursors.IBeam;
            }
            else if (c is CheckBox casilla)
            {
                casilla.ForeColor = TextoMedio;
                casilla.Cursor = Cursors.Hand;
            }
            else if (c is RadioButton radio)
            {
                radio.ForeColor = TextoMedio;
                radio.Cursor = Cursors.Hand;
            }
            else if (c is Button botonNativo)
            {
                Boton(botonNativo, Rol.Secundario);
            }
            else if (c is GunaButton botonGuna)
            {
                Boton(botonGuna, Rol.Secundario);
            }
            else if (c is DataGridView tabla)
            {
                EstilizarGrid(tabla);
            }
        }

        // ------------------------------------------------------------------
        // Botones
        // ------------------------------------------------------------------
        /// <summary> Estilo de boton nativo (System.Windows.Forms.Button). </summary>
        public static void Boton(Button b, Rol rol)
        {
            if ((b.Tag as string) == "no-estilo") return;

            Color fondo, hover, texto, borde;
            int bordeTam;

            switch (rol)
            {
                case Rol.Primario:
                    fondo = Verde; hover = VerdeHover; texto = Blanco; borde = VerdeOscuro; bordeTam = 0;
                    break;
                case Rol.Peligroso:
                    fondo = Rojo; hover = Color.FromArgb(255, 105, 105); texto = Blanco; borde = RojoOscuro; bordeTam = 0;
                    break;
                case Rol.ContornoPeligroso:
                    fondo = Blanco; hover = Color.FromArgb(255, 235, 235); texto = Rojo; borde = Rojo; bordeTam = 2;
                    break;
                case Rol.Amarillo:
                    fondo = Amarillo; hover = Color.FromArgb(255, 214, 60); texto = TextoMedio; borde = AmarilloOscuro; bordeTam = 0;
                    break;
                case Rol.Azul:
                    fondo = Azul; hover = Color.FromArgb(70, 190, 250); texto = Blanco; borde = AzulOscuro; bordeTam = 0;
                    break;
                case Rol.Fantasma:
                    fondo = FondoGris; hover = Borde; texto = TextoMedio; borde = Borde; bordeTam = 2;
                    break;
                default: // Secundario
                    fondo = Blanco; hover = VerdeClaro; texto = VerdeOscuro; borde = Verde; bordeTam = 2;
                    break;
            }

            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = bordeTam;
            b.FlatAppearance.BorderColor = borde;
            b.FlatAppearance.MouseOverBackColor = hover;
            b.FlatAppearance.MouseDownBackColor = hover;
            b.BackColor = fondo;
            b.ForeColor = texto;
            b.UseVisualStyleBackColor = false;
            b.Cursor = Cursors.Hand;
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.Font = Fuente(b.Font.Size, FontStyle.Bold);

            TextoMayusculaSiCabe(b);
        }

        /// <summary> Estilo de boton Guna (esquinas redondeadas). </summary>
        public static void Boton(GunaButton b, Rol rol)
        {
            if ((b.Tag as string) == "no-estilo") return;

            Color fondo, hover, texto, borde;
            int bordeTam;

            switch (rol)
            {
                case Rol.Primario:
                    fondo = Verde; hover = VerdeHover; texto = Blanco; borde = VerdeOscuro; bordeTam = 0;
                    break;
                case Rol.Peligroso:
                    fondo = Rojo; hover = Color.FromArgb(255, 105, 105); texto = Blanco; borde = RojoOscuro; bordeTam = 0;
                    break;
                case Rol.ContornoPeligroso:
                    fondo = Blanco; hover = Color.FromArgb(255, 235, 235); texto = Rojo; borde = Rojo; bordeTam = 2;
                    break;
                case Rol.Amarillo:
                    fondo = Amarillo; hover = Color.FromArgb(255, 214, 60); texto = TextoMedio; borde = AmarilloOscuro; bordeTam = 0;
                    break;
                case Rol.Azul:
                    fondo = Azul; hover = Color.FromArgb(70, 190, 250); texto = Blanco; borde = AzulOscuro; bordeTam = 0;
                    break;
                case Rol.Fantasma:
                    fondo = FondoGris; hover = Borde; texto = TextoMedio; borde = Borde; bordeTam = 2;
                    break;
                default: // Secundario
                    fondo = Blanco; hover = VerdeClaro; texto = VerdeOscuro; borde = Verde; bordeTam = 2;
                    break;
            }

            b.BaseColor = fondo;
            b.OnHoverBaseColor = hover;
            b.OnPressedColor = rol == Rol.Secundario || rol == Rol.ContornoPeligroso || rol == Rol.Fantasma
                ? hover
                : (rol == Rol.Primario ? VerdeOscuro : fondo);
            b.ForeColor = texto;
            b.OnHoverForeColor = texto;
            b.BorderColor = borde;
            b.BorderSize = bordeTam;
            // Sin esto, al pasar el mouse Guna usa el borde heredado del diseñador
            // (negro) y aparecia una linea gris alrededor del boton.
            b.OnHoverBorderColor = borde;
            b.Radius = 14;
            b.Animated = true;
            b.Cursor = Cursors.Hand;
            b.Font = Fuente(b.Font.Size, FontStyle.Bold);

            TextoMayusculaSiCabe(b);
        }

        /// <summary> Mayusculas estilo Duolingo, solo si el texto sigue cabiendo. </summary>
        private static void TextoMayusculaSiCabe(Button b)
        {
            b.Text = MayusculaSiCabe(b, b.Text, b.Width, b.Height);
        }

        private static void TextoMayusculaSiCabe(GunaButton b)
        {
            b.Text = MayusculaSiCabe(b, b.Text, b.Width, b.Height);
        }

        /// <summary>
        /// Pasa el texto a mayusulas SOLO si sigue cabiendo en el boton.
        /// Asi se consigue el estilo Duolingo sin que ningun rotulo se corte.
        /// </summary>
        private static string MayusculaSiCabe(Control b, string texto, int ancho, int alto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;

            string mayus = texto.ToUpper();
            if (mayus == texto) return texto;               // ya estaba en mayusulas

            Size medido = TextRenderer.MeasureText(mayus, b.Font, Size.Empty,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            if (medido.Width <= ancho - 14 && medido.Height <= alto - 8)
            {
                return mayus;
            }
            return texto;
        }

        // ------------------------------------------------------------------
        // Opciones de respuesta (tarjetas tipo Duolingo)
        // ------------------------------------------------------------------
        /// <summary> Boton de respuesta: tarjeta blanca con borde gris y hover suave. </summary>
        public static void Opcion(GunaButton b)
        {
            b.BaseColor = Blanco;
            b.OnHoverBaseColor = FondoGris;
            b.OnPressedColor = FondoGris;
            b.ForeColor = TextoOscuro;
            b.OnHoverForeColor = TextoOscuro;
            b.BorderColor = Borde;
            b.BorderSize = 2;
            b.OnHoverBorderColor = Borde;   // que el borde no cambie al pasar el mouse
            b.Radius = 14;
            b.Animated = true;
            b.Cursor = Cursors.Hand;
            // Tamanio fijo, no "el mayor entre el actual y 11". En el disenador las
            // tres respuestas venian con fuentes distintas (una a 8.25 y dos a 12), y
            // Math.Max solo subia la pequena: tras el escalado quedaban a 18.7 y 20.4
            // pt y las tarjetas se veian de tamaños diferentes.
            b.Font = Fuente(11F, FontStyle.Bold);
        }

        /// <summary> Pinta la opcion elegida (azul claro con borde azul). </summary>
        public static void OpcionElegida(GunaButton b, bool elegida)
        {
            b.BaseColor = elegida ? AzulClaro : Blanco;
            b.OnHoverBaseColor = elegida ? AzulClaro : FondoGris;
            b.BorderColor = elegida ? Azul : Borde;
            b.BorderSize = 2;
            b.OnHoverBorderColor = elegida ? Azul : Borde;
            b.ForeColor = elegida ? TextoOscuro : TextoOscuro;
        }

        // ------------------------------------------------------------------
        // Tarjetas, tablas, campos y barras
        // ------------------------------------------------------------------
        /// <summary>
        /// Tarjeta (panel con contenido): fondo y borde rectos. Las esquinas ya no
        /// se redondean (antes se recortaban con una region y se veia raro).
        /// </summary>
        public static void Tarjeta(Control panel, Color fondo, Color borde)
        {
            panel.BackColor = fondo;
            panel.Region = null;   // borra cualquier redondeo anterior del panel
            if (borde != Color.Empty)
            {
                panel.Paint += (s, e) =>
                {
                    using (Pen lapiz = new Pen(borde, 2))
                    {
                        e.Graphics.DrawRectangle(lapiz, 1, 1, panel.Width - 3, panel.Height - 3);
                    }
                };
            }
        }

        /// <summary> Tabla moderna: encabezado verde, filas claras y sin bordes feos. </summary>
        public static void EstilizarGrid(DataGridView g)
        {
            g.BackgroundColor = Blanco;
            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Borde;
            g.EnableHeadersVisualStyles = false;          // permite pintar el encabezado
            g.RowHeadersVisible = false;
            g.AllowUserToResizeRows = false;
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            g.RowTemplate.Height = 36;

            DataGridViewCellStyle encabezado = new DataGridViewCellStyle();
            encabezado.BackColor = Verde;
            encabezado.ForeColor = Blanco;
            encabezado.Font = Fuente(10F, FontStyle.Bold);
            encabezado.Alignment = DataGridViewContentAlignment.MiddleLeft;
            encabezado.Padding = new Padding(8, 0, 0, 0);
            g.ColumnHeadersDefaultCellStyle = encabezado;
            g.ColumnHeadersHeight = 44;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DataGridViewCellStyle celda = new DataGridViewCellStyle();
            celda.BackColor = Blanco;
            celda.ForeColor = TextoOscuro;
            celda.Font = Fuente(10F, FontStyle.Regular);
            celda.Alignment = DataGridViewContentAlignment.MiddleLeft;
            celda.Padding = new Padding(8, 0, 0, 0);
            g.DefaultCellStyle = celda;

            DataGridViewCellStyle alterna = new DataGridViewCellStyle(celda);
            alterna.BackColor = FondoGris;
            g.AlternatingRowsDefaultCellStyle = alterna;

            DataGridViewCellStyle seleccion = new DataGridViewCellStyle(celda);
            seleccion.BackColor = VerdeClaro;
            seleccion.ForeColor = TextoOscuro;
            g.DefaultCellStyle.SelectionBackColor = VerdeClaro;
            g.DefaultCellStyle.SelectionForeColor = TextoOscuro;

            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Verde;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Blanco;
        }

        /// <summary>
        /// Barra de progreso estilo Duolingo (fondo gris redondeado + relleno verde).
        /// Sustituye visualmente a la ProgressBar nativa, que no deja cambiar colores.
        /// Se puede llamar varias veces: la primera crea la barra, despues solo actualiza.
        /// </summary>
        public static void BarraProgreso(ProgressBar pb)
        {
            Control padre = pb.Parent;
            if (padre == null) return;

            double fraccion = 0;
            if (pb.Maximum > pb.Minimum)
            {
                fraccion = (pb.Value - pb.Minimum) / (double)(pb.Maximum - pb.Minimum);
            }

            Panel fondo = padre.Controls.Find("barra_" + pb.Name, false).FirstOrDefault() as Panel;
            if (fondo == null)
            {
                fondo = new Panel();
                fondo.Name = "barra_" + pb.Name;
                fondo.Tag = "barra";
                fondo.BackColor = Borde;
                fondo.Size = pb.Size;
                fondo.Location = pb.Location;
                fondo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                Redondear(fondo, fondo.Height / 2);

                pb.Parent.Controls.Add(fondo);
                pb.Visible = false;
            }

            Panel relleno = fondo.Controls.Find("relleno", false).FirstOrDefault() as Panel;
            if (relleno == null)
            {
                relleno = new Panel();
                relleno.Name = "relleno";
                relleno.Tag = "barra";
                relleno.BackColor = Verde;
                fondo.Controls.Add(relleno);
            }

            int anchoUtil = Math.Max(0, fondo.Width - 8);
            int ancho = (int)Math.Round(anchoUtil * Math.Max(0, Math.Min(1, fraccion)));
            if (ancho > 0 && ancho < 12) ancho = 12;   // que se note aunque sea poco

            relleno.Size = new Size(ancho, fondo.Height - 8);
            relleno.Location = new Point(4, 4);
            Redondear(relleno, Math.Max(1, relleno.Height / 2));
        }

        // ------------------------------------------------------------------
        // Utilidades varias
        // ------------------------------------------------------------------
        /// <summary> Titulo grande y en negrita. </summary>
        public static void Titulo(Label l, float tam = 0)
        {
            float t = tam > 0 ? tam : Math.Max(14F, l.Font.Size + 2F);
            l.Font = Fuente(t, FontStyle.Bold);
            l.ForeColor = TextoOscuro;
        }

        /// <summary> Etiqueta de campo (pequeña, gris oscura). </summary>
        public static void Campo(Label l)
        {
            l.Font = Fuente(Math.Max(10F, l.Font.Size), FontStyle.Regular);
            l.ForeColor = TextoMedio;
        }

        /// <summary>
        /// Oculta controles que no tienen logica (evita promesas falsas).
        /// Ademas se marcan con el Tag "oculto" para que no cuenten al centrar.
        /// </summary>
        public static void Ocultar(params Control[] controles)
        {
            foreach (Control c in controles)
            {
                if (c == null) continue;
                c.Tag = "oculto";
                c.Visible = false;
            }
        }
    }
}
