-- ============================================================================
--  DevyClass — Script de creación de la base de datos
--  Motor: MySQL 8.0 (probado también en MariaDB 10.4+ / 11.x)
--
--  ⚠ ESTE SCRIPT BORRA Y RECREA LAS 4 TABLAS DEL JUEGO.
--    Si ya tienes datos, NO lo ejecutes: primero haz un respaldo con
--      mysqldump -u root -p DevyClassBD > respaldo.sql
--
--  CÓMO USARLO
--  1) Abre MySQL Workbench / MySQL Shell / la consola de MySQL.
--  2) Conéctate como root.
--  3) Ejecuta este archivo completo, o impórtalo desde Workbench.
--
--     Desde la consola:
--       mysql -u root -p < database/crear_base_datos.sql
--
--  LA APP SE CONECTA CON
--    servidor localhost · base DevyClassBD · usuario root
--    contraseña: vacía, o "1234" (hay dos cadenas de conexión de respaldo
--    en DevyClass/DevyClass/ConexionBD/Conexion.cs).
--    Si tu MySQL tiene otra contraseña, míralo en el README.md →
--    "Solución de problemas".
-- ============================================================================

DROP DATABASE IF EXISTS `DevyClassBD`;
CREATE DATABASE `DevyClassBD`
  DEFAULT CHARACTER SET utf8mb4
  DEFAULT COLLATE utf8mb4_unicode_ci;
USE `DevyClassBD`;

-- ============================================================================
-- 1) tipo_usuario — catálogo de roles
--    La app usa: 1 = Administrador, 2 = Usuario normal.
--    (ver UsuarioDB/DatosUsuario.cs → propiedad ReferenciaTipo)
-- ============================================================================
CREATE TABLE `tipo_usuario` (
  `id_tipo` INT          NOT NULL AUTO_INCREMENT,
  `tipo`    VARCHAR(50)  DEFAULT NULL,
  PRIMARY KEY (`id_tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `tipo_usuario` (`id_tipo`, `tipo`) VALUES
  (1, 'Admin'),
  (2, 'Usuario');

-- ============================================================================
-- 2) modulo — agrupador de niveles
--    La app lo lee con ConsultasNivel.ObtenerModulos() para llenar el
--    ComboBox del formulario "Gestionar niveles".
--    OJO: el módulo 1 se llama "Pensamiento algoritmico" y la barra lateral
--    muestra 4 tarjetas (1-4), pero en la base solo hay 2 filas.
-- ============================================================================
CREATE TABLE `modulo` (
  `id_modulo` INT          NOT NULL AUTO_INCREMENT,
  `modulo`    VARCHAR(50)  DEFAULT NULL,
  PRIMARY KEY (`id_modulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `modulo` (`id_modulo`, `modulo`) VALUES
  (1, 'Pensamiento algoritmico'),
  (2, 'Estructura de control');

-- ============================================================================
-- 3) niveles — los retos del juego
--    `xp_otorgada`  = recompensa al completar el nivel
--    `xp_necesaria` = requisito para desbloquearlo
--    IMPORTANTE: id_nivel debe EMPEZAR en 1, porque el registro de usuarios
--    inserta ultimo_nivel = 1 (ver Autenticacion/RegistrarUsuario.cs).
--
--    OJO: hoy la app NO lee estas columnas de XP para nada; el progreso real
--    se calcula en código (UI_MenuPrincipal.RefrescarUI → completados * 20 XP).
-- ============================================================================
CREATE TABLE `niveles` (
  `id_nivel`          INT          NOT NULL AUTO_INCREMENT,
  `nombre`            VARCHAR(50)  DEFAULT NULL,
  `xp_necesaria`      INT          DEFAULT NULL,
  `xp_otorgada`       INT          DEFAULT NULL,
  `referencia_modulo` INT          DEFAULT NULL,
  PRIMARY KEY (`id_nivel`),
  KEY `referencia_modulo` (`referencia_modulo`),
  CONSTRAINT `niveles_ibfk_1`
    FOREIGN KEY (`referencia_modulo`) REFERENCES `modulo` (`id_modulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `niveles` (`id_nivel`, `nombre`, `xp_necesaria`, `xp_otorgada`, `referencia_modulo`) VALUES
  (1, 'Variables',         0,  20, 1),
  (2, 'Tipos de variables', 20, 20, 1),
  (3, 'Entrada de datos',  40, 20, 1),
  (4, 'Operadores',        60, 20, 1),
  (5, 'Comentarios',       80, 20, 1);

-- ============================================================================
-- 4) usuarios — cuentas del juego
--
--    ⚠ SEGURIDAD: la columna `contrasena` guarda la contraseña EN TEXTO
--    PLANO. La app la compara tal cual (Autenticacion/ValidarContraseniaYUsuario.cs)
--    y no hay ningún hash en todo el proyecto. Está bien para un prototipo
--    académico, pero NO para producción.
--
--    - `ultimo_nivel` admite NULL = todavía no completó ningún nivel.
--    - `username` NO tiene restricción UNIQUE: la unicidad la revisa la app
--      con ConsultasUsuario.ExisteUsername() antes de guardar.
-- ============================================================================
CREATE TABLE `usuarios` (
  `id_usuarios`     INT          NOT NULL AUTO_INCREMENT,
  `username`        VARCHAR(50)  DEFAULT NULL,
  `correo`          VARCHAR(50)  DEFAULT NULL,
  `fecha`           DATE         DEFAULT NULL,
  `contrasena`      VARCHAR(50)  DEFAULT NULL,
  `referencia_tipo` INT          DEFAULT NULL,
  `ultimo_nivel`    INT          DEFAULT NULL,
  PRIMARY KEY (`id_usuarios`),
  KEY `referencia_tipo` (`referencia_tipo`),
  KEY `ultimo_nivel` (`ultimo_nivel`),
  CONSTRAINT `usuarios_ibfk_1`
    FOREIGN KEY (`referencia_tipo`) REFERENCES `tipo_usuario` (`id_tipo`),
  CONSTRAINT `usuarios_ibfk_2`
    FOREIGN KEY (`ultimo_nivel`) REFERENCES `niveles` (`id_nivel`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- 5) Cuentas de prueba
--    Son las mismas que usa el equipo de desarrollo.
--    ⚠ Cámbialas antes de entregar o presentar el proyecto.
--
--      admin1 / 1234  → administrador (referencia_tipo = 1)
--      admin2 / 2345  → administrador, sin niveles completados
--      usuario / 1234 → usuario normal (referencia_tipo = 2)
-- ============================================================================
INSERT INTO `usuarios`
  (`username`, `correo`, `fecha`, `contrasena`, `referencia_tipo`, `ultimo_nivel`) VALUES
  ('admin1',   'admin1@gmail.com',   '2000-01-10', '1234', 1, 1),
  ('admin2',   'admin2@gmail.com',   '2001-06-22', '2345', 1, NULL),
  ('usuario',  'usuario@gmail.com',  '2000-05-15', '1234', 2, 1);

-- ============================================================================
-- FIN — verificación
-- ============================================================================
SELECT 'Base de datos DevyClassBD creada correctamente.' AS Resultado;
SELECT `id_usuarios`, `username`, `referencia_tipo`, `ultimo_nivel` FROM `usuarios`;
SELECT `id_nivel`, `nombre`, `xp_necesaria`, `xp_otorgada`, `referencia_modulo` FROM `niveles`;