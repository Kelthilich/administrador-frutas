using System;
using System.ComponentModel.DataAnnotations;

namespace frutas.DTOs
{
    /// <summary>
    /// DTO para transferir datos de usuario sin informaci�n sensible
    /// </summary>
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO para el login de usuario
    /// </summary>
    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contrase�a es requerida")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "La contrase�a debe tener entre 8 y 128 caracteres")]
        public string Password { get; set; }

        public bool RecordarUsuario { get; set; } = false;
    }

    /// <summary>
    /// DTO para el registro de usuario
    /// </summary>
    public class RegistroDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contrase�a es requerida")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "La contrase�a debe tener entre 8 y 128 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmaci�n de contrase�a es requerida")]
        [Compare("Password", ErrorMessage = "Las contrase�as no coinciden")]
        public string ConfirmarPassword { get; set; }

        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder 100 caracteres")]
        public string NombreCompleto { get; set; }
    }

    /// <summary>
    /// DTO para cambio de contrase�a
    /// </summary>
    public class CambioPasswordDto
    {
        [Required(ErrorMessage = "La contrase�a actual es requerida")]
        public string PasswordActual { get; set; }

        [Required(ErrorMessage = "La nueva contrase�a es requerida")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "La contrase�a debe tener entre 8 y 128 caracteres")]
        public string NuevaPassword { get; set; }

        [Required(ErrorMessage = "La confirmaci�n de contrase�a es requerida")]
        [Compare("NuevaPassword", ErrorMessage = "Las contrase�as no coinciden")]
        public string ConfirmarNuevaPassword { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de autenticaci�n
    /// </summary>
    public class AuthResponseDto
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; } // Para API
        public DateTime? FechaExpiracion { get; set; }
    }
}