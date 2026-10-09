# DevyClass

> Juego educativo de programación para escritorio, con estética tipo Duolingo.
> Desarrollado en **C# / Windows Forms** sobre **MySQL**.
> Proyecto integrador — teaches programming fundamentals through quizzes and XP progression.

```
┌──────────────────────────────────────────────┐
│           ¿Qué es DevyClass?                 │
│  App de escritorio que enseña conceptos      │
│  básicos de programación con retos           │
│  (opción múltiple, verdadero/falso, ordenar  │
│  pasos), progreso guardado y cuenta de       │
│  administrador para gestionar usuarios       │
│  y niveles.                                  │
└──────────────────────────────────────────────┘
```

---

## 📑 Índice

1. [¿Para qué sirve?](#-para-qué-sirve)
2. [Funciones](#-funciones)
3. [Tecnologías usadas](#-tecnologías-usadas)
4. [Requisitos previos](#-requisitos-previos)
5. [Manual de instalación](#-manual-de-instalación)
6. [Cuentas de prueba](#-cuentas-de-prueba)
7. [Estructura del proyecto](#-estructura-del-proyecto)
8. [Base de datos](#-base-de-datos)
9. [Cómo funciona por dentro](#-cómo-funciona-por-dentro)
10. [Solución de problemas](#-solución-de-problemas)
11. [Problemas conocidos / pendientes](#-problemas-conocidos--pendientes)
12. [Avisos de seguridad](#-avisos-de-seguridad)
13. [Créditos y licencias](#-créditos-y-licencias)

---

## 🎯 ¿Para qué sirve?

**DevyClass** es un *juego de aprender a programar*. En lugar de leer teoría, el
usuario resuelve retos cortos y gana experiencia (XP), tal como en Duolingo.

La app cubre tres necesidades:

| Necesidad | Cómo lo resuelve DevyClass |
|---|---|
| **Enseñar programación de forma divertida** | Los conceptos se practican con retos cronometrados en pantalla, con corrección inmediata. |
| **Saber cuánto ha avanzado el usuario** | Barra de progreso, porcentaje de niveles completados, XP acumulada y mapa de niveles con estrellas y candados. |
| **Administrar usuarios y contenido** | Panel de administrador con alta/baja de cuentas y CRUD de niveles y módulos desde la interfaz gráfica. |

**Estado actual:** el juego tiene **1 módulo funcional** (*Pensamiento algorítmico*)
con el **Nivel 1 completo** (3 preguntas + pantalla de victoria). El resto de
niveles y módulos están planificados: la barra lateral ya muestra las tarjetas,
pero el juego aún no se puede jugar de principio a fin.

---

## ✨ Funciones

### Para el usuario final

| Función | Detalle |
|---|---|
| **Inicio de sesión** | Valida usuario y contraseña contra MySQL. Botón para mostrar/ocultar la contraseña y acceso con `Enter`. |
| **Registro** | Campos obligatorios, confirmación de contraseña y política de contraseña fuerte: **mínimo 8 caracteres, al menos una letra, un número y un símbolo**. Incluye generador de contraseña aleatoria. |
| **Menú principal** | Barra lateral (Inicio · usuario · cerrar sesión), tarjeta de progreso, 4 tarjetas de módulos y botón de administrador. Muestra una frase motivacional aleatoria en cada visita. |
| **Progreso visible** | Barra de progreso, `%` de niveles completados, XP acumulada y nivel actual. Todo se recalcula al entrar. |
| **Mapa de niveles** | 10 posiciones por módulo: estrella dorada (completado), estrella plateada, botón *Jugar* (siguiente nivel) o candado (bloqueado). |
| **Nivel 1 (3 retos)** | 1) Opción múltiple: *"¿Cuál describe mejor un algoritmo?"* · 2) Verdadero/Falso · 3) Ordenar los pasos de una receta con desplegables. |
| **Pantalla de victoria** | Al terminar, muestra cuántos aciertos hizo de 3. |
| **Navegación entre pantallas** | `Siguiente ▶` (no deja avanzar sin responder) y `◀ Anterior` (regresa y retrocede el progreso). `Finalizar` guarda el avance. |
| **Ajustes** | Cambiar nombre de usuario y contraseña, con aviso si el nombre ya existe. Cerrar sesión. |

### Para el administrador

| Función | Detalle |
|---|---|
| **Panel de administración** | Tabla con **todos** los usuarios (con JOIN a tipo y nivel) y buscador en vivo mientras se escribe. |
| **Agregar usuario** | El admin elige el tipo (Admin o Usuario), el nivel inicial (0–50), la fecha de nacimiento, y puede generar una contraseña o borrar todos los campos con un clic. |
| **Eliminar usuario** | Tabla con buscador, confirmación *Sí/No* antes de borrar. |
| **Gestionar niveles** | CRUD completo: agregar, editar y eliminar niveles, con módulo asociado, XP necesaria y XP otorgada. |
| **Botón de administrador** | Solo aparece en el menú si el usuario es tipo Administrador (`referencia_tipo = 1`). |

### Técnicas / de diseño

- **Tema visual centralizado** (`Tema.cs`): paleta, tipografía, botones, tarjetas,
  barras de progreso, escalado responsive y centrado automático.
- **Guarda protege al diseñador**: abrir un formulario en Visual Studio nunca
  modifica su layout (`LicenseManager.UsageMode == Designtime`).
- **Cierre seguro**: el botón `X` de la ventana devuelve al menú en lugar de
  cerrar el programa.
- **Todas las consultas SQL usan parámetros** (`@nombre`, `@id`…), lo que
  **evita inyección SQL** en la capa de datos.

---

## 🛠 Tecnologías usadas

| Capa | Tecnología |
|---|---|
| Lenguaje | C# (Visual Basic disabled), proyecto clásico `.csproj` **no-SDK** |
| Interfaz | Windows Forms (`System.Windows.Forms`) |
| Framework | **.NET Framework 4.7.2** |
| Base de datos | **MySQL 8.0** (funciona igual con MariaDB 10.4+) |
| Driver | `MySql.Data` **9.7.0** |
| Controles UI | **Guna.UI** (botones, tarjetas, barras de progreso, transiciones) |
| IDE | Visual Studio 2022 (17.13+) o **Visual Studio 2026** |
| Control de versiones | Git |

### Librerías de terceros incluidas (`DevyClass/libs/`)

| DLL | Versión | ¿Se usa? |
|---|---|---|
| `Guna.UI.dll` | (sin versión registrada) | ✅ **Sí** — botones, groupbox, progressbar, transiciones |
| `Klik.Windows.Forms.EntryLib.V2.2005.dll` | 2.0.0.0 | ⚠️ Solo un `ELCalendar` decorativo en el menú, sin lógica. **Muerta** |
| `Bunifu_UI_v1.5.3.dll` | 1.5.3 | ❌ **No se usa en absoluto** — solo está referenciada en el `.csproj` |

> Las tres van incluidas en el repositorio para que el proyecto compile al
> clonarlo. Las dos últimas son librerías **comerciales con licencia** y
> ambas son candidatas a eliminarse del proyecto.

---

## 📋 Requisitos previos

| Requisito | Versión | Notas |
|---|---|---|
| Windows | 10 u 11 | WinForms: no corre en Linux/macOS |
| Visual Studio | 2022 **17.13+** o 2026 | Hace falta la carga de trabajo **".NET desktop"**. El archivo de solución es `.slnx`, un formato nuevo |
| .NET Framework | **4.7.2** | Viene con Windows 10/11 |
| MySQL Server | 8.0 (o MariaDB 10.4+) | Debe estar **corriendo** antes de abrir la app |

---

## 📖 Manual de instalación

### Paso 1 — Clonar el repositorio

```bash
git clone https://github.com/yairCS2/Proyecto-integrador.git
cd Proyecto-integrador
```

No hace falta instalar nada más de NuGet: la carpeta `packages/` y las DLLs de
`libs/` ya vienen en el repositorio, así que **el proyecto compila sin internet**.

### Paso 2 — Instalar MySQL

Instala **MySQL Server 8.0** (o MariaDB) desde
<https://dev.mysql.com/downloads/installer/> o
<https://mariadb.org/server/>.

Durante la instalación:
- Anota la contraseña de `root`.
- En Windows, **marca "Add MySQL to PATH"**.
- Deja el puerto **3306**.

### Paso 3 — Crear la base de datos (importante)

El script crea la base `DevyClassBD` con sus 4 tablas y datos de prueba:

> ⚠️ **Este script hace `DROP DATABASE`**. Si ya tienes la base con datos reales,
> respalda primero: `mysqldump -u root -p DevyClassBD > respaldo.sql`

**Opción A — consola de MySQL** (agrega el `bin` de MySQL al PATH):

```bash
mysql -u root -p < database/crear_base_datos.sql
```

**Opción B — MySQL Workbench:** abre el archivo `database/crear_base_datos.sql`
y presiona el rayo ⚡ (*Execute*).

**Opción C — HeidiSQL / phpMyAdmin:** abre el archivo y ejecútalo.

Deberías ver al final:

```
Resultado: Base de datos DevyClassBD creada correctamente.
```

### Paso 4 — Abrir el proyecto

```bash
cd DevyClass
```

Haz **doble clic en `DevyClass.slnx`**, o desde Visual Studio:
*Archivo → Abrir → Proyecto o solución*.

> Si tu Visual Studio no abre `.slnx`, actualízalo a la versión 17.13 o superior.

### Paso 5 — Compilar

En Visual Studio presiona **Ctrl+Shift+B**, o desde la consola:

```bash
"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" ^
  DevyClass\DevyClass.slnx /t:Build /p:Configuration=Debug
```

Salida esperada:

```
DevyClass -> ...\DevyClass\DevyClass\bin\Debug\DevyClass.exe
```

Se genera `DevyClass/DevyClass/bin/Debug/DevyClass.exe`.

### Paso 6 — Ejecutar

Presiona **F5** en Visual Studio, o haz doble clic en:

```
DevyClass\DevyClass\bin\Debug\DevyClass.exe
```

### Paso 7 — Iniciar sesión

Usa una de las [cuentas de prueba](#-cuentas-de-prueba), o registra una nueva
desde la pantalla de inicio.

**¡Listo!** 🎉 Para ejecutarlo en otra máquina, copia la carpeta
`bin/Debug/` completa (incluye las DLL de MySql.Data y Guna.UI).

---

## 👤 Cuentas de prueba

Creadas por `database/crear_base_datos.sql`:

| Usuario | Contraseña | Tipo | Puede entrar al panel admin |
|---|---|---|---|
| `admin1` | `1234` | Administrador | ✅ Sí |
| `admin2` | `2345` | Administrador | ✅ Sí |
| `usuario` | `1234` | Usuario normal | ❌ No (el botón queda oculto) |

> ⚠️ Estas contraseñas están en **texto plano** en la base de datos. Cámbialas
> antes de entregar o presentar el proyecto.

---

## 📂 Estructura del proyecto

```
Proyecto-integrador/
├── README.md                       ← este archivo
├── .gitignore
├── database/
│   └── crear_base_datos.sql         ← script de instalación de la BD
└── DevyClass/
    ├── DevyClass.slnx               ← solución (formato XML, VS 17.13+)
    ├── libs/                        ← DLL de UI de terceros (Guna, Bunifu, Klik)
    ├── packages/                    ← NuGet restaurado (15 paquetes, versionado)
    └── DevyClass/                   ← el proyecto de C#
        ├── DevyClass.csproj
        ├── packages.config
        ├── App.config               ← binding redirects (sin connection strings)
        ├── Program.cs               ← punto de entrada: abre UI_InicioSesion
        ├── Tema.cs                  ← 🆕 design system (paleta, botones, escalas)
        │
        ├── ConexionBD/
        │   └── Conexion.cs          ← cadena de conexión MySQL
        │
        ├── UsuarioDB/               ← capa de acceso a datos
        │   ├── DatosUsuario.cs      ← modelo de usuario
        │   ├── ConsultasUsuario.cs  ← CRUD de la tabla usuarios
        │   └── ConsultasNivel.cs    ← CRUD de la tabla niveles
        │
        ├── Autenticacion/
        │   ├── ValidarContraseniaYUsuario.cs   ← login
        │   └── RegistrarUsuario.cs            
        │
        ├── Formularios_UI/          ← pantallas principales
        │   ├── UI_InicioSesion.cs        ← login
        │   ├── UI_Registro.cs            ← alta de cuenta
        │   ├── UI_MenuPrincipal.cs       ← menú + progreso
        │   ├── UI_Administrador.cs       ← panel admin
        │   ├── UI_AgregarUsuario.cs
        │   ├── UI_EliminarUsuario.cs
        │   ├── UI_GestionarNiveles.cs
        │   └── UI_Ajustes.cs             ← perfil y cerrar sesión
        │
        ├── Formularios_UI_niveles/
        │   └── Modulo 1/              ← único módulo implementado
        │       ├── Modulo.cs                 ← mapa de niveles
        │       ├── Nivel1.cs                 ← contenedor del quiz
        │       ├── Nivel1RepuestasCorrectas.cs  ← DTO de respuestas
        │       ├── pregunta1.cs / Pregunta2.cs / Pregunta3.cs
        │       └── Ganaste.cs                ← pantalla de victoria
        │
        ├── Properties/               ← AssemblyInfo, Resources, Settings
        └── Resources/                ← ~100 imágenes (PNG)
```


---

## 🗄 Base de datos

Base: **`DevyClassBD`** — 4 tablas InnoDB, charset `utf8mb4`.

```
tipo_usuario            niveles                        modulo
┌──────────────┐        ┌─────────────────────────┐    ┌────────────────────┐
│ id_tipo (PK) │        │ id_nivel (PK)           │    │ id_modulo (PK)     │
│ tipo         │        │ nombre                   │    │ modulo             │
└──────────────┘        │ xp_necesaria             │    └────────────────────┘
       ▲                │ xp_otorgada               │             ▲
       │                │ referencia_modulo (FK) ───┼─────────────┘
       │                └─────────────────────────┘
       │                                 ▲
┌──────┴───────────────────┐             │
│ usuarios                 │             │
├──────────────────────────┤             │
│ id_usuarios (PK)         │             │
│ username                 │             │
│ correo                   │             │
│ fecha                    │             │
│ contrasena  ⚠️ TEXTO PLANO             │
│ referencia_tipo (FK) ────┘             │
│ ultimo_nivel    (FK) ─────────────────┘
└──────────────────────────┘
```

### Catálogos

| Tabla | Valores |
|---|---|
| `tipo_usuario` | `1 = Admin`, `2 = Usuario` |
| `modulo` | `1 = Pensamiento algoritmico`, `2 = Estructura de control` |
| `niveles` | 5 niveles del módulo 1, cada uno con `xp_necesaria` y `xp_otorgada` |

### Consultas principales

**Usuarios** (`UsuarioDB/ConsultasUsuario.cs`):

| Método | SQL |
|---|---|
| `ObtenerUsuarioPorUsername` | `SELECT * FROM usuarios WHERE username = @username` |
| `RegistrarUsuario` | `INSERT INTO Usuarios (...)` |
| `EditarUsuarioCompleto` | `UPDATE usuarios SET username, contrasena WHERE id_usuarios = @id` |
| `ActualizarUsuario` | `UPDATE usuarios SET ... (todas las columnas)` |
| `EliminarUsuario` | `DELETE FROM usuarios WHERE id_usuarios = @id` |
| `ObtenerTodosLosUsuarios` | `SELECT ... FROM usuarios LEFT JOIN niveles JOIN tipo_usuario` |
| `ExisteUsername` | `SELECT COUNT(*) FROM usuarios WHERE username = @username AND id_usuarios != @id` |

**Niveles** (`UsuarioDB/ConsultasNivel.cs`):

| Método | SQL |
|---|---|
| `ObtenerTodosLosNiveles` | `SELECT ... FROM niveles LEFT JOIN modulo ORDER BY id_nivel` |
| `ObtenerModulos` | `SELECT id_modulo, modulo FROM modulo ORDER BY id_modulo` |
| `AgregarNivel` / `ActualizarNivel` / `EliminarNivel` | `INSERT` / `UPDATE` / `DELETE` sobre `niveles` |

**Login** (`Autenticacion/ValidarContraseniaYUsuario.cs`):

```sql
SELECT * FROM usuarios WHERE usuarios.username = @usuario AND usuarios.contrasena = @contrasena;
```

> **Detalle importante:** `RegistrarUsuario` convierte el nivel `0` en `NULL`,
> porque la clave foránea `ultimo_nivel → niveles.id_nivel` rechaza el `0`
> (error 1452 de MySQL). Por eso un usuario sin niveles tiene `ultimo_nivel = NULL`.

---

## 🧠 Cómo funciona por dentro

### Flujo de arranque

```
Program.Main()
   └─► UI_InicioSesion  ──► login OK ──► UI_MenuPrincipal.AbrirMenu(usuario)
                                    └──► "Regístrate" ──► UI_Registro
                                                            └──► alta OK ──► UI_MenuPrincipal
```

`UI_MenuPrincipal.AbrirMenu(...)` es una **fábrica estática**: si el menú ya está
abierto lo reutiliza en vez de apilar una ventana nueva.

### Los dos tipos de usuario

`DatosUsuario.ReferenciaTipo` decide todo:

- `1` → **Administrador**: el menú muestra el botón de panel.
- `2` → **Usuario normal**: el botón se oculta.

> ⚠️ La comprobación **solo oculta el botón**. Las clases de administración son
> `public` y no validan el rol por sí mismas. Ver [Avisos de seguridad](#-avisos-de-seguridad).

### Motor de niveles

Los niveles se registran como **tipos de C#**, no como filas de la base:

```csharp
// UI_MenuPrincipal.cs
private Type[] Niveles = { typeof(Nivel1) };   // por ahora solo el 1

int indice = UsuarioActual.UltimoNivel ?? 0;
if (indice >= Niveles.Length) {
    MessageBox.Show("Has completado todos los niveles disponibles por ahora.");
    return;
}
var pantalla = Activator.CreateInstance(Niveles[indice], UsuarioActual) as Form;
```

Agregar un nivel nuevo = crear su formulario y añadir su `typeof(...)` al arreglo.

### Motor del quiz

`Nivel1` monta las pantallas con **fábricas** (`Func<UserControl>[]`), así que
cada pregunta se construye al mostrarse en lugar de todas de golpe:

```csharp
preguntas = new Func<UserControl>[]
{
    () => new pregunta1(UsuarioPreguntas),
    () => new Pregunta2(UsuarioPreguntas),
    () => new Pregunta3(),
    () => new Ganaste(UsuarioPreguntas)
};
```

`Nivel1RepuestasCorrectas` es el objeto **compartido por referencia** que
transporta las respuestas entre las pantallas, y `Ganaste` recalcula el puntaje
final con `CalcularAciertos(...)`.

### Progreso y XP

Todo se deriva de **un solo entero**: `usuarios.ultimo_nivel` (niveles completados).

```csharp
int completados = UsuarioActual.UltimoNivel ?? 0;
progressBar1.Value      = Math.Min(100, completados * 2);   // 2% por nivel
lblPorcentajeNiveles.Text = $"{Math.Min(100, completados * 2)}%";
lblExperiencia.Text      = $"{completados * 20} XP";          // 20 XP por nivel
```

> Las columnas `xp_necesaria` y `xp_otorgada` de la tabla `niveles` existen y se
> editan desde el panel, pero **la app todavía no las usa**: la XP se calcula en
> código. Ahí está el trabajo pendiente más claro del proyecto.

Al pulsar `Finalizar`:

```csharp
UsuarioActual.UltimoNivel = Math.Max(UsuarioActual.UltimoNivel ?? 0, 1);
new ConsultasUsuario().ActualizarUsuario(UsuarioActual);
UI_MenuPrincipal.AbrirMenu(UsuarioActual);
```

### `Tema.cs` — el sistema de diseño

Es la pieza más grande del proyecto (≈845 líneas) y centraliza toda la
apariencia: paleta Duolingo (`#58CC02` verde, `#1CB0F6` azul), estilos de botón
con radio 14, tarjetas con borde dibujado, barras de progreso propias, grid con
encabezado verde, escalado de fuente al redimensionar y centrado automático del
contenido en cada `Resize`.

---

## 🔧 Solución de problemas

### ❌ «No se puede abrir el archivo `.slnx`»

Tu Visual Studio es anterior a 17.13. Actualízalo, o abre directamente
`DevyClass/DevyClass/DevyClass.csproj`.

### ❌ «Error de referencia: no se encontró Guna.UI»

Las DLL de `libs/` no llegaron al clon. Verifica que existan:

```
DevyClass/libs/Guna.UI.dll
DevyClass/libs/Bunifu_UI_v1.5.3.dll
DevyClass/libs/Klik.Windows.Forms.EntryLib.V2.2005.dll
```

Si faltan, restitúyelas con `git checkout -- DevyClass/libs/` o vuelve a clonar.

### ❌ «Unable to connect to any of the specified MySQL hosts»

MySQL no está corriendo. Verifica el servicio:

```powershell
Get-Service | Where-Object Name -like '*mysql*'
```

### ❌ «Access denied for user 'root'@'localhost' (using password: YES)»

**Esta es la causa #1 de fallos**, porque hay **dos** cadenas de conexión
escritas en el código (`DevyClass/DevyClass/ConexionBD/Conexion.cs`):

```csharp
private string cadena  = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=;";    // sin contraseña
private string cadena2 = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=1234;"; // respaldo
```

La app intenta la primera y, si falla, cae en la segunda. Si tu contraseña es
otra, edita **`Pwd=`** en las dos líneas y recompila:

```csharp
private string cadena  = "Server=localhost;Database=DevyClassBD;Uid=root;Pwd=tuClave;";
```

> Ambas claves están fijadas en el código fuente. Moverlas a `App.config` o a
> variables de entorno es una de las mejoras pendientes.

### ❌ «Unknown database 'DevyClassBD'»

No ejecutaste el script. Ve al [Paso 3](#paso-3--crear-la-base-de-datos-importante).

### ❌ «Table 'devyclassbd.usuarios' doesn't exist» pero la base sí existe

Ojo con las **mayúsculas en Linux**; no aplica a Windows. En Windows MySQL no
distingue mayúsculas, pero el nombre de la base es `devyclassbd` (minúsculas) en
la instalación real. Funciona igual porque la conexión no distingue mayúsculas
en Windows.

### ❌ El panel de administrador no aparece

Solo se muestra si `referencia_tipo = 1`. Verifica en la BD:

```sql
SELECT username, referencia_tipo FROM usuarios;
```

### ❌ El botón «Finalizar» no guarda el avance

Solo guarda si completas el Nivel 1 hasta la pantalla de victoria. Además usa
`Math.Max(..., 1)`, así que en la práctica el nivel nunca retrocede de 1.

### ❌ Los niveles 2–10 dicen «próximamente»

Es lo esperado: `UI_MenuPrincipal.Niveles` solo contiene `typeof(Nivel1)`.

### ℹ️ Herramienta de diagnóstico incluida

`Conexion.verificarConexion()` abre la BD y avisa si funciona. **Ojo:** su botón
(`gunaButton1_Click`) nunca se conectó en el diseñador, así que no aparece en la
interfaz. Para probarla, ábrela desde el depurador de Visual Studio.

---

## 📌 Problemas conocidos / pendientes

**Funcionalidad incompleta**

- [ ] Solo el **Nivel 1** es jugable; los niveles 2–10 son placeholders.
- [ ] La barra lateral muestra **4 módulos**, pero la BD solo tiene **2 filas**
      en `modulo`, y solo el módulo 1 tiene contenido.
- [ ] El texto dice «0/50 Niveles» pero la BD tiene 5 niveles y el mapa muestra 10.
- [ ] La **XP no se guarda**: se calcula como `completados * 20` y las columnas
      `xp_necesaria` / `xp_otorgada` no se leen.
- [ ] Botón **«Eliminar Cuenta»** en Ajustes: está diseñado con estilo de peligro
      pero **no tiene evento asignado**, así que no hace nada.
- [ ] Botón **«Logros»** vacío; `btnTemario`, `btnRendimiento` y el botón
      «Inicio» de la barra lateral tampoco hacen nada.
- [ ] Autenticación: los formularios de administración son `public` y **no
      validan el rol**; solo se oculta el botón.

**Código muerto que conviene borrar**

- `Autenticacion/RegistrarUsuario.cs` — **nunca se instancia** y además tiene un
  bug: sus 4 parámetros se filling todos con `usuario`.
- `Bunifu_UI_v1.5.3.dll` — referenciada pero **sin un solo uso**.
- `ELCalendar` (Klik) — control decorativo sin código asociado.
- En `Nivel1RepuestasCorrectas`: `RespuestasCorrectas`, `Pregunta4Res` y
  `progreso` se escriben pero **nunca se leen**.
- `using Mysqlx.Notice;` en el menú principal (no tiene nada que ver).
- `<Folder Include="LogicaC#\" />` en el `.csproj` apunta a una carpeta inexistente.

**Detalles menores**

- En `UI_MenuPrincipal.cs` hay `"Pensamiento\\nalgorítmico"` con una barra invertida
  literal que se ve en pantalla; debería ser un salto de línea.
- El versionado está desalineado: el menú dice `DevyClass 1.1.0` pero
  `AssemblyVersion` es `1.0.0.0`.
- En el diseñador, el botón de usuario tiene hardcodeado `gunaButton8.Text = "Alexis Flores"`.
  En ejecución lo sobrescribe `gunaButton8.Text = UsuarioActual.Username`, pero conviene
  limpiarlo para no exponer nombres al traducir el formulario.
- Nombres de handlers que no corresponden a su control (p. ej. `pictureBox1_Click`
  es el botón del ojo, no una imagen), y el typo `rdbUsuarioNromal`.
- Handlers `Paint`, `Load` y `TextChanged` vacíos esparcidos por todos los formularios.

---

## 🔐 Avisos de seguridad

Este proyecto es un **prototipo académico**. Antes de usarlo con datos reales:

1. **Las contraseñas se guardan en texto plano.** No hay ningún hash en todo el
   código: el login las compara directamente en SQL. Cualquiera con acceso a la
   base de datos ve todas las contraseñas.
   *Arreglo sugerido:* usar `BCrypt.Net-Next` o `Rfc2898DeriveBytes` (PBKDF2).

2. **Las credenciales de MySQL están fijadas en el código** (`Conexion.cs`), con
   usuario `root` — el superusuario de la base. Lo ideal es un usuario
   dedicado con permisos mínimos, y leer la cadena desde `App.config`.

3. **El acceso al panel de administrador no está protegido**: se decide
   únicamente *ocultando un botón*. Cualquier formulario `public` es alcanzable.

4. **El buscador de los grids** arma el `RowFilter` por concatenación de
   cadenas, escapando solo las comillas simples.

5. No hay límite de intentos en el login, ni registro de auditoría en el borrado
   de usuarios.

---

## 📄 Créditos y licencias

**Proyecto:** DevyClass — juego educativo de programación.
Proyecto integrador © 2026.

**Librerías de terceros con licencia comercial** (incluidas en `DevyClass/libs/`):

- [Guna.UI](https://gunaui.com/) — © GunaySoft (licencia comercial)
- [Bunifu UI](https://bunifuframework.com/) — © Bunifu (licencia comercial)
- [Klik EntryLib](https://www.kliksoftware.com/) — © Klik (licencia comercial)

**Paquetes NuGet** (versión `net472`, gestionados con `packages.config`):

| Paquete | Versión |
|---|---|
| MySql.Data | 9.7.0 |
| BouncyCastle.Cryptography | 2.6.2 |
| Google.Protobuf | 3.32.0 |
| K4os.Compression.LZ4 | 1.3.8 |
| K4os.Compression.LZ4.Streams | 1.3.8 |
| K4os.Hash.xxHash | 1.0.8 |
| Microsoft.Bcl.AsyncInterfaces | 5.0.0 |
| System.Buffers | 4.5.1 |
| System.Configuration.ConfigurationManager | 8.0.0 |
| System.IO.Pipelines | 5.0.2 |
| System.Memory | 4.5.5 |
| System.Numerics.Vectors | 4.5.0 |
| System.Runtime.CompilerServices.Unsafe | 6.0.0 |
| System.Threading.Tasks.Extensions | 4.5.4 |
| ZstdSharp.Port | 0.8.6 |

> 10 de estos 15 son dependencias transitivas de `MySql.Data`.

**Comando para verificar que el proyecto compila:**

```powershell
"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" `
  DevyClass\DevyClass.slnx /t:Build /p:Configuration=Debug
```
