using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frutas.Models
{
    /// <summary>
    /// Entidad para registrar todas las operaciones del sistema
    /// Implementa auditoría completa de cambios
    /// </summary>
    [Table("Logs")]
    public class Log
    {
        /// <summary>
        /// Identificador único del log
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ID del usuario que realizó la acción
        /// </summary>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Nombre de usuario (para casos donde no hay usuario logueado)
        /// </summary>
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Acción realizada (CREATE, READ, UPDATE, DELETE, LOGIN, LOGOUT, etc.)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Accion { get; set; }

        /// <summary>
        /// Tabla afectada por la operación
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Tabla { get; set; }

        /// <summary>
        /// ID del registro afectado
        /// </summary>
        public int? RegistroId { get; set; }

        /// <summary>
        /// Detalle del estado anterior del registro (JSON)
        /// </summary>
        [Column(TypeName = "nvarchar(MAX)")]
        public string DetalleAntes { get; set; }

        /// <summary>
        /// Detalle del estado posterior del registro (JSON)
        /// </summary>
        [Column(TypeName = "nvarchar(MAX)")]
        public string DetalleDepues { get; set; }

        /// <summary>
        /// Fecha y hora de la operación
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>
        /// Dirección IP desde donde se realizó la operación
        /// </summary>
        [StringLength(45)] // IPv6 puede ser hasta 45 caracteres
        public string IP { get; set; }

        /// <summary>
        /// User Agent del navegador/cliente
        /// </summary>
        [StringLength(500)]
        public string UserAgent { get; set; }

        /// <summary>
        /// URL o endpoint que fue llamado
        /// </summary>
        [StringLength(500)]
        public string Endpoint { get; set; }

        /// <summary>
        /// Método HTTP utilizado (GET, POST, PUT, DELETE)
        /// </summary>
        [StringLength(10)]
        public string MetodoHttp { get; set; }

        /// <summary>
        /// Tiempo de ejecución de la operación en milisegundos
        /// </summary>
        public long? TiempoEjecucion { get; set; }

        /// <summary>
        /// Indica si la operación fue exitosa
        /// </summary>
        public bool Exitoso { get; set; } = true;

        /// <summary>
        /// Mensaje de error si la operación falló
        /// </summary>
        [Column(TypeName = "nvarchar(MAX)")]
        public string MensajeError { get; set; }

        /// <summary>
        /// Nivel de severidad del log (INFO, WARNING, ERROR, CRITICAL)
        /// </summary>
        [StringLength(20)]
        public string Severidad { get; set; } = "INFO";

        /// <summary>
        /// Referencia al usuario que realizó la acción
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        /// <summary>
        /// Método estático para crear un log de información
        /// </summary>
        public static Log CrearLogInfo(string accion, string tabla, int? usuarioId = null, 
            string username = null, int? registroId = null)
        {
            return new Log
            {
                Accion = accion,
                Tabla = tabla,
                UsuarioId = usuarioId,
                Username = username,
                RegistroId = registroId,
                Severidad = "INFO",
                Exitoso = true
            };
        }

        /// <summary>
        /// Método estático para crear un log de error
        /// </summary>
        public static Log CrearLogError(string accion, string tabla, string mensajeError, 
            int? usuarioId = null, string username = null)
        {
            return new Log
            {
                Accion = accion,
                Tabla = tabla,
                UsuarioId = usuarioId,
                Username = username,
                MensajeError = mensajeError,
                Severidad = "ERROR",
                Exitoso = false
            };
        }
    }
}