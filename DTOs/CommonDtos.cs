using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace frutas.DTOs
{
    /// <summary>
    /// DTO base para respuestas de la API
    /// </summary>
    /// <typeparam name="T">Tipo de datos que contiene la respuesta</typeparam>
    public class ResponseDto<T>
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
        public List<string> Errores { get; set; } = new List<string>();

        public static ResponseDto<T> Exito(T data, string mensaje = "Operaci�n exitosa")
        {
            return new ResponseDto<T>
            {
                Exitoso = true,
                Mensaje = mensaje,
                Data = data
            };
        }

        public static ResponseDto<T> Error(string mensaje, List<string> errores = null)
        {
            return new ResponseDto<T>
            {
                Exitoso = false,
                Mensaje = mensaje,
                Errores = errores ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// DTO para respuestas sin datos espec�ficos
    /// </summary>
    public class ResponseDto
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public List<string> Errores { get; set; } = new List<string>();

        public static ResponseDto Exito(string mensaje = "Operaci�n exitosa")
        {
            return new ResponseDto
            {
                Exitoso = true,
                Mensaje = mensaje
            };
        }

        public static ResponseDto Error(string mensaje, List<string> errores = null)
        {
            return new ResponseDto
            {
                Exitoso = false,
                Mensaje = mensaje,
                Errores = errores ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// DTO para actualizar perfil de usuario
    /// </summary>
    public class ActualizarPerfilDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Formato de email inv�lido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder 100 caracteres")]
        public string NombreCompleto { get; set; }
    }

    /// <summary>
    /// DTO para cambiar contrase�a
    /// </summary>
    public class CambiarPasswordDto
    {
        [Required(ErrorMessage = "La contrase�a actual es requerida")]
        public string PasswordActual { get; set; }

        [Required(ErrorMessage = "La nueva contrase�a es requerida")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contrase�a debe tener entre 8 y 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "La contrase�a debe tener al menos 8 caracteres, incluyendo may�scula, min�scula, n�mero y car�cter especial")]
        public string PasswordNueva { get; set; }

        [Required(ErrorMessage = "Debe confirmar la nueva contrase�a")]
        [Compare("PasswordNueva", ErrorMessage = "Las contrase�as no coinciden")]
        public string ConfirmarPassword { get; set; }
    }
}