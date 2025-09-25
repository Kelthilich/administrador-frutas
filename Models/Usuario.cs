using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frutas.Models
{
    /// <summary>
    /// Entidad que representa un usuario del sistema
    /// Maneja la autenticación y autorización
    /// </summary>
    [Table("Usuarios")]
    public class Usuario : BaseEntity
    {
        /// <summary>
        /// Nombre de usuario único para login
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string Username { get; set; }

        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        /// <summary>
        /// Hash de la contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Salt utilizado para el hash de la contraseña
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Salt { get; set; }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Rol del usuario en el sistema
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Usuario";

        /// <summary>
        /// Fecha del último login
        /// </summary>
        public DateTime? UltimoLogin { get; set; }

        /// <summary>
        /// Número de intentos fallidos de login
        /// </summary>
        public int IntentosFallidos { get; set; } = 0;

        /// <summary>
        /// Indica si la cuenta está bloqueada
        /// </summary>
        public bool CuentaBloqueada { get; set; } = false;

        /// <summary>
        /// Fecha hasta la cual la cuenta está bloqueada
        /// </summary>
        public DateTime? FechaBloqueo { get; set; }
    }
}