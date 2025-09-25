using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frutas.Models
{
    /// <summary>
    /// Entidad que representa un usuario del sistema
    /// Maneja la autenticaci�n y autorizaci�n
    /// </summary>
    [Table("Usuarios")]
    public class Usuario : BaseEntity
    {
        /// <summary>
        /// Nombre de usuario �nico para login
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string Username { get; set; }

        /// <summary>
        /// Correo electr�nico del usuario
        /// </summary>
        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inv�lido")]
        public string Email { get; set; }

        /// <summary>
        /// Hash de la contrase�a del usuario
        /// </summary>
        [Required(ErrorMessage = "La contrase�a es requerida")]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Salt utilizado para el hash de la contrase�a
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
        /// Fecha del �ltimo login
        /// </summary>
        public DateTime? UltimoLogin { get; set; }

        /// <summary>
        /// N�mero de intentos fallidos de login
        /// </summary>
        public int IntentosFallidos { get; set; } = 0;

        /// <summary>
        /// Indica si la cuenta est� bloqueada
        /// </summary>
        public bool CuentaBloqueada { get; set; } = false;

        /// <summary>
        /// Fecha hasta la cual la cuenta est� bloqueada
        /// </summary>
        public DateTime? FechaBloqueo { get; set; }
    }
}