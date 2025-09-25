using System;
using System.Collections.Generic;
using System.Linq;
using frutas.DTOs;
using frutas.Models;
using frutas.Repositories;
using frutas.Security;

namespace frutas.Services
{
    /// <summary>
    /// Interfaz para el servicio de logs
    /// Define las operaciones de negocio para la gesti�n de logs y auditor�a
    /// </summary>
    public interface ILogService
    {
        // Consultas
        ResponseDto<LogDto> ObtenerPorId(int id);
        ResponseDto<IEnumerable<LogDto>> ObtenerTodos(int pagina = 1, int tama�oPagina = 20);
        ResponseDto<IEnumerable<LogDto>> ObtenerConFiltros(LogFiltroDto filtro);
        ResponseDto<LogListaDto> ObtenerLogs(LogFiltroDto filtro);
        ResponseDto<IEnumerable<LogDto>> ObtenerPorUsuario(int usuarioId, int pagina = 1, int tama�oPagina = 20);
        ResponseDto<IEnumerable<LogDto>> ObtenerPorFecha(DateTime fecha);
        ResponseDto<IEnumerable<LogDto>> ObtenerErrores(int pagina = 1, int tama�oPagina = 20);

        // Estad�sticas
        ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorSeveridad();
        ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorAccion();
        ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorTabla();
        ResponseDto<int> ContarLogsPorUsuario(int usuarioId);
        ResponseDto<int> ContarErroresHoy();

        // Mantenimiento
        ResponseDto LimpiarLogsAntiguos(int diasAMantener = 90);
        ResponseDto<Dictionary<string, object>> ObtenerResumenActividad();

        // Logging p�blico para otros servicios
        void RegistrarInfo(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null);
        void RegistrarWarning(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null);
        void RegistrarError(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null, string mensajeError = null);
        void RegistrarCambio(string accion, string tabla, int registroId, string estadoAntes, string estadoDepues, int? usuarioId = null);
    }

    /// <summary>
    /// Implementaci�n del servicio de logs
    /// Contiene toda la l�gica de negocio relacionada con auditor�a y logging
    /// </summary>
    public class LogService : ILogService
    {
        private readonly LogRepository _logRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public LogService(LogRepository logRepository, IUsuarioRepository usuarioRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public LogService()
        {
            _logRepository = new LogRepository();
            _usuarioRepository = new UsuarioRepository();
        }

        #region Consultas

        public ResponseDto<LogDto> ObtenerPorId(int id)
        {
            try
            {
                var log = _logRepository.ObtenerPorId(id);
                if (log == null)
                {
                    return ResponseDto<LogDto>.Error("Log no encontrado");
                }

                var logDto = MapearALogDto(log);
                return ResponseDto<LogDto>.Exito(logDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<LogDto>.Error($"Error obteniendo log: {ex.Message}");
            }
        }

        public ResponseDto<IEnumerable<LogDto>> ObtenerTodos(int pagina = 1, int tama�oPagina = 20)
        {
            try
            {
                // Verificar permisos - solo administradores pueden ver todos los logs
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<IEnumerable<LogDto>>.Error("No tiene permisos para ver los logs del sistema");
                }

                var logs = _logRepository.ObtenerPaginado(pagina, tama�oPagina);
                var logsDto = logs.Select(MapearALogDto);

                return ResponseDto<IEnumerable<LogDto>>.Exito(logsDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<LogDto>>.Error($"Error obteniendo logs: {ex.Message}");
            }
        }

        public ResponseDto<IEnumerable<LogDto>> ObtenerConFiltros(LogFiltroDto filtro)
        {
            try
            {
                // Verificar permisos
                if (!SessionHelper.EsAdministrador)
                {
                    // Los usuarios normals solo pueden ver sus propios logs
                    filtro.UsuarioId = SessionHelper.UsuarioActualId;
                }

                // Validar par�metros
                if (filtro.Pagina < 1) filtro.Pagina = 1;
                if (filtro.Tama�oPagina < 1 || filtro.Tama�oPagina > 100) filtro.Tama�oPagina = 20;

                var logs = _logRepository.ObtenerPaginadoConFiltros(
                    filtro.Pagina,
                    filtro.Tama�oPagina,
                    filtro.UsuarioId,
                    filtro.Accion,
                    filtro.Tabla,
                    filtro.Severidad,
                    filtro.FechaDesde,
                    filtro.FechaHasta
                );

                var logsDto = logs.Select(MapearALogDto);
                return ResponseDto<IEnumerable<LogDto>>.Exito(logsDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<LogDto>>.Error($"Error obteniendo logs con filtros: {ex.Message}");
            }
        }

        public ResponseDto<IEnumerable<LogDto>> ObtenerPorUsuario(int usuarioId, int pagina = 1, int tama�oPagina = 20)
        {
            try
            {
                // Verificar permisos - solo el mismo usuario o administradores
                if (!SessionHelper.EsAdministrador && SessionHelper.UsuarioActualId != usuarioId)
                {
                    return ResponseDto<IEnumerable<LogDto>>.Error("No tiene permisos para ver los logs de este usuario");
                }

                var logs = _logRepository.ObtenerPorUsuario(usuarioId).Skip((pagina - 1) * tama�oPagina).Take(tama�oPagina);
                var logsDto = logs.Select(MapearALogDto);

                return ResponseDto<IEnumerable<LogDto>>.Exito(logsDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<LogDto>>.Error($"Error obteniendo logs del usuario: {ex.Message}");
            }
        }

        public ResponseDto<IEnumerable<LogDto>> ObtenerPorFecha(DateTime fecha)
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<IEnumerable<LogDto>>.Error("No tiene permisos para ver logs por fecha");
                }

                var logs = _logRepository.ObtenerPorFecha(fecha);
                var logsDto = logs.Select(MapearALogDto);

                return ResponseDto<IEnumerable<LogDto>>.Exito(logsDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<LogDto>>.Error($"Error obteniendo logs por fecha: {ex.Message}");
            }
        }

        public ResponseDto<IEnumerable<LogDto>> ObtenerErrores(int pagina = 1, int tama�oPagina = 20)
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<IEnumerable<LogDto>>.Error("No tiene permisos para ver logs de errores");
                }

                var logs = _logRepository.ObtenerErrores().Skip((pagina - 1) * tama�oPagina).Take(tama�oPagina);
                var logsDto = logs.Select(MapearALogDto);

                return ResponseDto<IEnumerable<LogDto>>.Exito(logsDto);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<LogDto>>.Error($"Error obteniendo logs de errores: {ex.Message}");
            }
        }

        public ResponseDto<LogListaDto> ObtenerLogs(LogFiltroDto filtro)
        {
            try
            {
                // Verificar permisos - solo administradores pueden ver todos los logs
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<LogListaDto>.Error("No tiene permisos para ver los logs del sistema");
                }

                var logs = _logRepository.ObtenerConFiltros(filtro);
                var totalRegistros = _logRepository.ContarConFiltros(filtro);

                var resultado = new LogListaDto
                {
                    Logs = logs.Select(MapearALogDto).ToList(),
                    TotalRegistros = totalRegistros,
                    PaginaActual = filtro.Pagina,
                    Tama�oPagina = filtro.Tama�oPagina
                };

                return ResponseDto<LogListaDto>.Exito(resultado);
            }
            catch (Exception ex)
            {
                return ResponseDto<LogListaDto>.Error($"Error obteniendo logs: {ex.Message}");
            }
        }

        #endregion

        #region Estad�sticas

        public ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorSeveridad()
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<Dictionary<string, int>>.Error("No tiene permisos para ver estad�sticas");
                }

                var estadisticas = new Dictionary<string, int>
                {
                    ["INFO"] = _logRepository.ContarPorSeveridad("INFO"),
                    ["WARNING"] = _logRepository.ContarPorSeveridad("WARNING"),
                    ["ERROR"] = _logRepository.ContarPorSeveridad("ERROR"),
                    ["CRITICAL"] = _logRepository.ContarPorSeveridad("CRITICAL")
                };

                return ResponseDto<Dictionary<string, int>>.Exito(estadisticas);
            }
            catch (Exception ex)
            {
                return ResponseDto<Dictionary<string, int>>.Error($"Error obteniendo estad�sticas por severidad: {ex.Message}");
            }
        }

        public ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorAccion()
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<Dictionary<string, int>>.Error("No tiene permisos para ver estad�sticas");
                }

                // Implementaci�n simplificada - en un sistema real usar�amos GROUP BY
                var estadisticas = new Dictionary<string, int>();
                var acciones = new[] { "LOGIN", "LOGOUT", "CREATE", "UPDATE", "DELETE", "VIEW" };

                foreach (var accion in acciones)
                {
                    var logs = _logRepository.ObtenerPorAccion(accion);
                    estadisticas[accion] = logs.Count();
                }

                return ResponseDto<Dictionary<string, int>>.Exito(estadisticas);
            }
            catch (Exception ex)
            {
                return ResponseDto<Dictionary<string, int>>.Error($"Error obteniendo estad�sticas por acci�n: {ex.Message}");
            }
        }

        public ResponseDto<Dictionary<string, int>> ObtenerEstadisticasPorTabla()
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<Dictionary<string, int>>.Error("No tiene permisos para ver estad�sticas");
                }

                var estadisticas = new Dictionary<string, int>();
                var tablas = new[] { "Usuarios", "Frutas", "ApiKeys", "Sistema" };

                foreach (var tabla in tablas)
                {
                    var logs = _logRepository.ObtenerPorTabla(tabla);
                    estadisticas[tabla] = logs.Count();
                }

                return ResponseDto<Dictionary<string, int>>.Exito(estadisticas);
            }
            catch (Exception ex)
            {
                return ResponseDto<Dictionary<string, int>>.Error($"Error obteniendo estad�sticas por tabla: {ex.Message}");
            }
        }

        public ResponseDto<int> ContarLogsPorUsuario(int usuarioId)
        {
            try
            {
                // Verificar permisos
                if (!SessionHelper.EsAdministrador && SessionHelper.UsuarioActualId != usuarioId)
                {
                    return ResponseDto<int>.Error("No tiene permisos para ver estad�sticas de este usuario");
                }

                int count = _logRepository.ContarPorUsuario(usuarioId);
                return ResponseDto<int>.Exito(count);
            }
            catch (Exception ex)
            {
                return ResponseDto<int>.Error($"Error contando logs del usuario: {ex.Message}");
            }
        }

        public ResponseDto<int> ContarErroresHoy()
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<int>.Error("No tiene permisos para ver estad�sticas de errores");
                }

                int count = _logRepository.ContarPorFecha(DateTime.Today);
                return ResponseDto<int>.Exito(count);
            }
            catch (Exception ex)
            {
                return ResponseDto<int>.Error($"Error contando errores de hoy: {ex.Message}");
            }
        }

        #endregion

        #region Mantenimiento

        public ResponseDto LimpiarLogsAntiguos(int diasAMantener = 90)
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto.Error("No tiene permisos para limpiar logs");
                }

                _logRepository.LimpiarLogsAntiguos(diasAMantener);

                RegistrarInfo("LOGS_CLEANED", "Logs", 
                    $"Logs anteriores a {diasAMantener} d�as fueron limpiados", 
                    SessionHelper.UsuarioActualId);

                return ResponseDto.Exito($"Logs anteriores a {diasAMantener} d�as han sido limpiados");
            }
            catch (Exception ex)
            {
                RegistrarError("CLEAN_LOGS_ERROR", "Logs", 
                    "Error limpiando logs antiguos", 
                    SessionHelper.UsuarioActualId, null, ex.Message);
                
                return ResponseDto.Error($"Error limpiando logs: {ex.Message}");
            }
        }

        public ResponseDto<Dictionary<string, object>> ObtenerResumenActividad()
        {
            try
            {
                if (!SessionHelper.EsAdministrador)
                {
                    return ResponseDto<Dictionary<string, object>>.Error("No tiene permisos para ver resumen de actividad");
                }

                var resumen = new Dictionary<string, object>
                {
                    ["TotalLogs"] = _logRepository.ContarTodos(),
                    ["LogsHoy"] = _logRepository.ContarPorFecha(DateTime.Today),
                    ["ErroresHoy"] = _logRepository.ContarErrores(),
                    ["UsuariosActivos"] = _usuarioRepository.ObtenerActivos().Count(),
                    ["UltimaActividad"] = DateTime.Now
                };

                // Actividad de la �ltima semana
                var actividadSemana = new Dictionary<string, int>();
                for (int i = 6; i >= 0; i--)
                {
                    var fecha = DateTime.Today.AddDays(-i);
                    actividadSemana[fecha.ToString("yyyy-MM-dd")] = _logRepository.ContarPorFecha(fecha);
                }
                resumen["ActividadSemana"] = actividadSemana;

                return ResponseDto<Dictionary<string, object>>.Exito(resumen);
            }
            catch (Exception ex)
            {
                return ResponseDto<Dictionary<string, object>>.Error($"Error obteniendo resumen de actividad: {ex.Message}");
            }
        }

        #endregion

        #region Logging p�blico

        public void RegistrarInfo(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null)
        {
            RegistrarLog(accion, tabla, detalle, "INFO", usuarioId, registroId);
        }

        public void RegistrarWarning(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null)
        {
            RegistrarLog(accion, tabla, detalle, "WARNING", usuarioId, registroId);
        }

        public void RegistrarError(string accion, string tabla, string detalle, int? usuarioId = null, int? registroId = null, string mensajeError = null)
        {
            RegistrarLog(accion, tabla, detalle, "ERROR", usuarioId, registroId, null, null, mensajeError, false);
        }

        public void RegistrarCambio(string accion, string tabla, int registroId, string estadoAntes, string estadoDepues, int? usuarioId = null)
        {
            RegistrarLog(accion, tabla, $"Registro {registroId} modificado", "INFO", usuarioId, registroId, estadoAntes, estadoDepues);
        }

        #endregion

        #region M�todos privados

        private LogDto MapearALogDto(Log log)
        {
            return new LogDto
            {
                Id = log.Id,
                UsuarioId = log.UsuarioId,
                Username = log.Username,
                Accion = log.Accion,
                Tabla = log.Tabla,
                RegistroId = log.RegistroId,
                DetalleAntes = log.DetalleAntes,
                DetalleDepues = log.DetalleDepues,
                Fecha = log.Fecha,
                IP = log.IP,
                UserAgent = log.UserAgent,
                Severidad = log.Severidad ?? "INFO",
                Exitoso = log.Exitoso
            };
        }

        private void RegistrarLog(string accion, string tabla, string detalle, string severidad = "INFO", 
            int? usuarioId = null, int? registroId = null, string detalleAntes = null, 
            string detalleDepues = null, string mensajeError = null, bool exitoso = true)
        {
            try
            {
                var log = new Log
                {
                    UsuarioId = usuarioId ?? SessionHelper.UsuarioActualId,
                    Username = SessionHelper.UsuarioActualUsername,
                    Accion = accion,
                    Tabla = tabla,
                    RegistroId = registroId,
                    DetalleAntes = detalleAntes,
                    DetalleDepues = detalleDepues ?? detalle,
                    Fecha = DateTime.Now,
                    IP = SessionHelper.ObtenerIPCliente(),
                    UserAgent = SessionHelper.ObtenerUserAgent(),
                    Severidad = severidad,
                    Exitoso = exitoso && severidad != "ERROR",
                    MensajeError = mensajeError
                };

                _logRepository.Agregar(log);
            }
            catch
            {
                // No hacer nada si falla el logging para evitar errores en cascada
                // En un sistema de producci�n, podr�as escribir a un archivo de log de emergencia
            }
        }

        #endregion
    }
}