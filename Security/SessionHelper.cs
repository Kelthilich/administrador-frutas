using System;
using System.Web;
using System.Web.SessionState;
using frutas.DTOs;
using frutas.Models;

namespace frutas.Security
{
    /// <summary>
    /// Helper para manejo de sesiones de usuario
    /// Proporciona m�todos para gestionar la sesi�n del usuario actual
    /// </summary>
    public static class SessionHelper
    {
        #region Constantes de sesi�n

        private const string SESSION_USER_KEY = "CurrentUser";
        private const string SESSION_USER_ID_KEY = "CurrentUserId";
        private const string SESSION_USERNAME_KEY = "CurrentUsername";
        private const string SESSION_USER_ROLE_KEY = "CurrentUserRole";
        private const string SESSION_LOGIN_TIME_KEY = "LoginTime";

        #endregion

        #region Propiedades p�blicas

        /// <summary>
        /// Obtiene el usuario actual de la sesi�n
        /// </summary>
        public static UsuarioDto UsuarioActual
        {
            get
            {
                return HttpContext.Current?.Session?[SESSION_USER_KEY] as UsuarioDto;
            }
        }

        /// <summary>
        /// Obtiene el ID del usuario actual
        /// </summary>
        public static int? UsuarioActualId
        {
            get
            {
                var id = HttpContext.Current?.Session?[SESSION_USER_ID_KEY];
                return id as int?;
            }
        }

        /// <summary>
        /// Obtiene el username del usuario actual
        /// </summary>
        public static string UsuarioActualUsername
        {
            get
            {
                return HttpContext.Current?.Session?[SESSION_USERNAME_KEY] as string;
            }
        }

        /// <summary>
        /// Obtiene el rol del usuario actual
        /// </summary>
        public static string UsuarioActualRol
        {
            get
            {
                return HttpContext.Current?.Session?[SESSION_USER_ROLE_KEY] as string;
            }
        }

        /// <summary>
        /// Verifica si el usuario est� logueado
        /// </summary>
        public static bool EstaLogueado
        {
            get
            {
                return UsuarioActual != null && UsuarioActualId.HasValue;
            }
        }

        /// <summary>
        /// Verifica si el usuario actual es administrador
        /// </summary>
        public static bool EsAdministrador
        {
            get
            {
                return EstaLogueado && UsuarioActualRol == "Administrador";
            }
        }

        /// <summary>
        /// Verifica si el usuario actual es moderador o administrador
        /// </summary>
        public static bool EsModerador
        {
            get
            {
                return EstaLogueado && (UsuarioActualRol == "Moderador" || UsuarioActualRol == "Administrador");
            }
        }

        /// <summary>
        /// Obtiene el tiempo transcurrido desde el login
        /// </summary>
        public static TimeSpan? TiempoSesion
        {
            get
            {
                var loginTime = HttpContext.Current?.Session?[SESSION_LOGIN_TIME_KEY] as DateTime?;
                return loginTime.HasValue ? DateTime.Now - loginTime.Value : (TimeSpan?)null;
            }
        }

        #endregion

        #region M�todos de sesi�n

        /// <summary>
        /// Establece la sesi�n del usuario
        /// </summary>
        /// <param name="usuario">DTO del usuario</param>
        public static void EstablecerSesion(UsuarioDto usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var session = HttpContext.Current?.Session;
            if (session != null)
            {
                session[SESSION_USER_KEY] = usuario;
                session[SESSION_USER_ID_KEY] = usuario.Id;
                session[SESSION_USERNAME_KEY] = usuario.Username;
                session[SESSION_USER_ROLE_KEY] = usuario.Rol;
                session[SESSION_LOGIN_TIME_KEY] = DateTime.Now;

                // Configurar timeout de sesi�n (8 horas)
                session.Timeout = 480;
            }
        }

        /// <summary>
        /// Cierra la sesi�n del usuario
        /// </summary>
        public static void CerrarSesion()
        {
            var session = HttpContext.Current?.Session;
            if (session != null)
            {
                session.Clear();
                session.Abandon();
            }
        }

        /// <summary>
        /// Actualiza la informaci�n del usuario en la sesi�n
        /// </summary>
        /// <param name="usuario">DTO del usuario actualizado</param>
        public static void ActualizarUsuario(UsuarioDto usuario)
        {
            if (EstaLogueado && usuario != null && usuario.Id == UsuarioActualId)
            {
                EstablecerSesion(usuario);
            }
        }

        /// <summary>
        /// Actualiza la sesi�n con los datos m�s recientes del usuario
        /// </summary>
        /// <param name="usuario">DTO del usuario actualizado</param>
        public static void ActualizarSesion(UsuarioDto usuario)
        {
            if (EstaLogueado && usuario != null && usuario.Id == UsuarioActualId)
            {
                var session = HttpContext.Current?.Session;
                if (session != null)
                {
                    // Mantener el tiempo de login original
                    var loginTime = session[SESSION_LOGIN_TIME_KEY];
                    
                    // Actualizar datos del usuario
                    session[SESSION_USER_KEY] = usuario;
                    session[SESSION_USERNAME_KEY] = usuario.Username;
                    session[SESSION_USER_ROLE_KEY] = usuario.Rol;
                    
                    // Restaurar tiempo de login
                    session[SESSION_LOGIN_TIME_KEY] = loginTime;
                }
            }
        }

        /// <summary>
        /// Verifica si la sesi�n ha expirado
        /// </summary>
        /// <returns>True si la sesi�n ha expirado</returns>
        public static bool SesionExpirada()
        {
            var tiempoSesion = TiempoSesion;
            return !tiempoSesion.HasValue || tiempoSesion.Value.TotalHours > 8;
        }

        #endregion

        #region M�todos de autorizaci�n

        /// <summary>
        /// Verifica si el usuario puede editar un registro
        /// </summary>
        /// <param name="usuarioCreadorId">ID del usuario que cre� el registro</param>
        /// <returns>True si puede editar</returns>
        public static bool PuedeEditar(int? usuarioCreadorId)
        {
            if (!EstaLogueado) return false;
            
            // Los administradores pueden editar todo
            if (EsAdministrador) return true;
            
            // Los usuarios normales solo pueden editar sus propios registros
            return usuarioCreadorId.HasValue && usuarioCreadorId.Value == UsuarioActualId;
        }

        /// <summary>
        /// Verifica si el usuario puede eliminar un registro
        /// </summary>
        /// <param name="usuarioCreadorId">ID del usuario que cre� el registro</param>
        /// <returns>True si puede eliminar</returns>
        public static bool PuedeEliminar(int? usuarioCreadorId)
        {
            if (!EstaLogueado) return false;
            
            // Los administradores pueden eliminar todo
            if (EsAdministrador) return true;
            
            // Los moderadores pueden eliminar registros de usuarios normales
            if (EsModerador) return true;
            
            // Los usuarios normales solo pueden eliminar sus propios registros
            return usuarioCreadorId.HasValue && usuarioCreadorId.Value == UsuarioActualId;
        }

        /// <summary>
        /// Verifica si el usuario puede ver los logs
        /// </summary>
        /// <returns>True si puede ver logs</returns>
        public static bool PuedeVerLogs()
        {
            return EsAdministrador || EsModerador;
        }

        /// <summary>
        /// Verifica si el usuario puede gestionar otros usuarios
        /// </summary>
        /// <returns>True si puede gestionar usuarios</returns>
        public static bool PuedeGestionarUsuarios()
        {
            return EsAdministrador;
        }

        #endregion

        #region M�todos de informaci�n de request

        /// <summary>
        /// Obtiene la IP del cliente
        /// </summary>
        /// <returns>IP del cliente</returns>
        public static string ObtenerIPCliente()
        {
            var context = HttpContext.Current;
            if (context?.Request == null) return "Unknown";

            string ip = context.Request.Headers["CF-Connecting-IP"] ?? // Cloudflare
                       context.Request.Headers["X-Forwarded-For"] ??     // Proxy
                       context.Request.Headers["X-Real-IP"] ??           // Nginx
                       context.Request.UserHostAddress ??               // IIS
                       "Unknown";

            // Si viene de un proxy, tomar la primera IP
            if (ip.Contains(","))
            {
                ip = ip.Split(',')[0].Trim();
            }

            return ip;
        }

        /// <summary>
        /// Obtiene el User Agent del cliente
        /// </summary>
        /// <returns>User Agent del cliente</returns>
        public static string ObtenerUserAgent()
        {
            var context = HttpContext.Current;
            return context?.Request?.UserAgent ?? "Unknown";
        }

        /// <summary>
        /// Obtiene informaci�n del navegador
        /// </summary>
        /// <returns>Informaci�n del navegador</returns>
        public static string ObtenerInfoNavegador()
        {
            var context = HttpContext.Current;
            if (context?.Request?.Browser == null) return "Unknown";

            var browser = context.Request.Browser;
            return $"{browser.Browser} {browser.Version} ({browser.Platform})";
        }

        /// <summary>
        /// Obtiene la URL de referencia
        /// </summary>
        /// <returns>URL de referencia</returns>
        public static string ObtenerUrlReferencia()
        {
            var context = HttpContext.Current;
            return context?.Request?.UrlReferrer?.ToString() ?? string.Empty;
        }

        #endregion

        #region M�todos de validaci�n de sesi�n

        /// <summary>
        /// Valida que la sesi�n sea v�lida y no haya expirado
        /// </summary>
        /// <returns>True si la sesi�n es v�lida</returns>
        public static bool ValidarSesion()
        {
            if (!EstaLogueado) return false;
            if (SesionExpirada()) 
            {
                CerrarSesion();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Renueva la sesi�n actualizando el tiempo de login
        /// </summary>
        public static void RenovarSesion()
        {
            if (EstaLogueado)
            {
                var session = HttpContext.Current?.Session;
                if (session != null)
                {
                    session[SESSION_LOGIN_TIME_KEY] = DateTime.Now;
                }
            }
        }

        #endregion

        #region M�todos de depuraci�n

        /// <summary>
        /// Obtiene informaci�n de depuraci�n de la sesi�n actual
        /// </summary>
        /// <returns>Informaci�n de la sesi�n</returns>
        public static string ObtenerInfoSesion()
        {
            if (!EstaLogueado) return "No hay sesi�n activa";

            var info = $"Usuario: {UsuarioActualUsername} (ID: {UsuarioActualId})\n" +
                      $"Rol: {UsuarioActualRol}\n" +
                      $"Tiempo de sesi�n: {TiempoSesion?.ToString(@"hh\:mm\:ss")}\n" +
                      $"IP: {ObtenerIPCliente()}\n" +
                      $"User Agent: {ObtenerUserAgent()}";

            return info;
        }

        #endregion
    }
}