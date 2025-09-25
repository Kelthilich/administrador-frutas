using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using frutas.DTOs;

namespace frutas.Validators
{
    /// <summary>
    /// Validador para operaciones relacionadas con usuarios
    /// Contiene reglas de negocio espec�ficas para usuarios
    /// </summary>
    public static class UsuarioValidator
    {
        #region Constantes de validaci�n

        private const int MIN_USERNAME_LENGTH = 3;
        private const int MAX_USERNAME_LENGTH = 50;
        private const int MIN_PASSWORD_LENGTH = 8;
        private const int MAX_PASSWORD_LENGTH = 128;
        private const int MAX_NOMBRE_LENGTH = 100;
        private const int MAX_EMAIL_LENGTH = 100;

        // Regex para validaciones
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex UsernameRegex = new Regex(
            @"^[a-zA-Z0-9._-]+$",
            RegexOptions.Compiled);

        #endregion

        #region Validaci�n de registro

        /// <summary>
        /// Valida los datos de registro de un usuario
        /// </summary>
        public static ValidationResult ValidarRegistro(RegistroDto registro)
        {
            var result = new ValidationResult();

            // Validar username
            var usernameValidation = ValidarUsername(registro.Username);
            if (!usernameValidation.IsValid)
                result.AddErrors(usernameValidation.Errors);

            // Validar email
            var emailValidation = ValidarEmail(registro.Email);
            if (!emailValidation.IsValid)
                result.AddErrors(emailValidation.Errors);

            // Validar contrase�a
            var passwordValidation = ValidarPassword(registro.Password);
            if (!passwordValidation.IsValid)
                result.AddErrors(passwordValidation.Errors);

            // Validar confirmaci�n de contrase�a
            if (registro.Password != registro.ConfirmarPassword)
                result.AddError("Las contrase�as no coinciden");

            // Validar nombre completo
            if (!string.IsNullOrEmpty(registro.NombreCompleto))
            {
                var nombreValidation = ValidarNombreCompleto(registro.NombreCompleto);
                if (!nombreValidation.IsValid)
                    result.AddErrors(nombreValidation.Errors);
            }

            return result;
        }

        #endregion

        #region Validaci�n de login

        /// <summary>
        /// Valida los datos de login
        /// </summary>
        public static ValidationResult ValidarLogin(LoginDto login)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(login.Username))
                result.AddError("El nombre de usuario es requerido");

            if (string.IsNullOrWhiteSpace(login.Password))
                result.AddError("La contrase�a es requerida");

            return result;
        }

        #endregion

        #region Validaci�n de cambio de contrase�a

        /// <summary>
        /// Valida los datos de cambio de contrase�a
        /// </summary>
        public static ValidationResult ValidarCambioPassword(CambioPasswordDto cambio)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(cambio.PasswordActual))
                result.AddError("La contrase�a actual es requerida");

            // Validar nueva contrase�a
            var passwordValidation = ValidarPassword(cambio.NuevaPassword);
            if (!passwordValidation.IsValid)
                result.AddErrors(passwordValidation.Errors);

            // Validar confirmaci�n
            if (cambio.NuevaPassword != cambio.ConfirmarNuevaPassword)
                result.AddError("Las contrase�as no coinciden");

            // Verificar que no sea la misma contrase�a
            if (cambio.PasswordActual == cambio.NuevaPassword)
                result.AddError("La nueva contrase�a debe ser diferente a la actual");

            return result;
        }

        #endregion

        #region Validaciones individuales

        /// <summary>
        /// Valida un nombre de usuario
        /// </summary>
        public static ValidationResult ValidarUsername(string username)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(username))
            {
                result.AddError("El nombre de usuario es requerido");
                return result;
            }

            username = username.Trim();

            if (username.Length < MIN_USERNAME_LENGTH)
                result.AddError($"El nombre de usuario debe tener al menos {MIN_USERNAME_LENGTH} caracteres");

            if (username.Length > MAX_USERNAME_LENGTH)
                result.AddError($"El nombre de usuario no puede exceder {MAX_USERNAME_LENGTH} caracteres");

            if (!UsernameRegex.IsMatch(username))
                result.AddError("El nombre de usuario solo puede contener letras, n�meros, puntos, guiones y guiones bajos");

            // Palabras reservadas
            var palabrasReservadas = new[] { "admin", "administrator", "root", "system", "null", "undefined" };
            if (palabrasReservadas.Contains(username.ToLower()))
                result.AddError("El nombre de usuario no est� disponible");

            return result;
        }

        /// <summary>
        /// Valida una direcci�n de email
        /// </summary>
        public static ValidationResult ValidarEmail(string email)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(email))
            {
                result.AddError("El email es requerido");
                return result;
            }

            email = email.Trim();

            if (email.Length > MAX_EMAIL_LENGTH)
                result.AddError($"El email no puede exceder {MAX_EMAIL_LENGTH} caracteres");

            if (!EmailRegex.IsMatch(email))
                result.AddError("El formato del email no es v�lido");

            // Dominios no permitidos (ejemplo)
            var dominiosNoPermitidos = new[] { "temp.com", "10minutemail.com", "guerrillamail.com" };
            var dominio = email.Split('@').LastOrDefault()?.ToLower();
            if (dominio != null && dominiosNoPermitidos.Contains(dominio))
                result.AddError("El dominio del email no est� permitido");

            return result;
        }

        /// <summary>
        /// Valida una contrase�a
        /// </summary>
        public static ValidationResult ValidarPassword(string password)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(password))
            {
                result.AddError("La contrase�a es requerida");
                return result;
            }

            if (password.Length < MIN_PASSWORD_LENGTH)
                result.AddError($"La contrase�a debe tener al menos {MIN_PASSWORD_LENGTH} caracteres");

            if (password.Length > MAX_PASSWORD_LENGTH)
                result.AddError($"La contrase�a no puede exceder {MAX_PASSWORD_LENGTH} caracteres");

            // Verificar complejidad
            bool tieneMinuscula = password.Any(char.IsLower);
            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneNumero = password.Any(char.IsDigit);
            bool tieneEspecial = password.Any(c => !char.IsLetterOrDigit(c));

            if (!tieneMinuscula)
                result.AddError("La contrase�a debe contener al menos una letra min�scula");

            if (!tieneMayuscula)
                result.AddError("La contrase�a debe contener al menos una letra may�scula");

            if (!tieneNumero)
                result.AddError("La contrase�a debe contener al menos un n�mero");

            if (!tieneEspecial)
                result.AddError("La contrase�a debe contener al menos un car�cter especial");

            // Contrase�as comunes no permitidas
            var passwordsComunes = new[] { "password", "123456", "qwerty", "abc123", "password123" };
            if (passwordsComunes.Contains(password.ToLower()))
                result.AddError("La contrase�a es demasiado com�n");

            return result;
        }

        /// <summary>
        /// Valida un nombre completo
        /// </summary>
        public static ValidationResult ValidarNombreCompleto(string nombreCompleto)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(nombreCompleto))
                return result; // Es opcional

            nombreCompleto = nombreCompleto.Trim();

            if (nombreCompleto.Length > MAX_NOMBRE_LENGTH)
                result.AddError($"El nombre completo no puede exceder {MAX_NOMBRE_LENGTH} caracteres");

            // Solo letras, espacios, acentos y algunos caracteres especiales
            var nombreRegex = new Regex(@"^[a-zA-Z��������������\s\-'\.]+$");
            if (!nombreRegex.IsMatch(nombreCompleto))
                result.AddError("El nombre completo contiene caracteres no v�lidos");

            return result;
        }

        #endregion

        #region Validaciones de rol

        /// <summary>
        /// Valida si un rol es v�lido
        /// </summary>
        public static ValidationResult ValidarRol(string rol)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(rol))
            {
                result.AddError("El rol es requerido");
                return result;
            }

            var rolesValidos = new[] { "Usuario", "Administrador", "Moderador" };
            if (!rolesValidos.Contains(rol))
                result.AddError("El rol especificado no es v�lido");

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Validador para operaciones relacionadas con frutas
    /// Contiene reglas de negocio espec�ficas para frutas
    /// </summary>
    public static class FrutaValidator
    {
        #region Constantes de validaci�n

        private const int MAX_NOMBRE_LENGTH = 100;
        private const int MAX_DESCRIPCION_LENGTH = 500;
        private const int MAX_CATEGORIA_LENGTH = 50;
        private const int MAX_PAIS_LENGTH = 50;
        private const int MAX_TEMPORADA_LENGTH = 30;
        private const decimal MIN_PRECIO = 0.01m;
        private const decimal MAX_PRECIO = 999999.99m;
        private const int MIN_STOCK = 0;
        private const int MAX_STOCK = 999999;

        #endregion

        #region Validaci�n de formulario de fruta

        /// <summary>
        /// Valida los datos de una fruta para crear/actualizar
        /// </summary>
        public static ValidationResult ValidarFrutaForm(FrutaFormDto fruta)
        {
            var result = new ValidationResult();

            // Validar nombre
            var nombreValidation = ValidarNombre(fruta.Nombre);
            if (!nombreValidation.IsValid)
                result.AddErrors(nombreValidation.Errors);

            // Validar descripci�n
            if (!string.IsNullOrEmpty(fruta.Descripcion))
            {
                var descripcionValidation = ValidarDescripcion(fruta.Descripcion);
                if (!descripcionValidation.IsValid)
                    result.AddErrors(descripcionValidation.Errors);
            }

            // Validar precio
            var precioValidation = ValidarPrecio(fruta.Precio);
            if (!precioValidation.IsValid)
                result.AddErrors(precioValidation.Errors);

            // Validar stock
            var stockValidation = ValidarStock(fruta.Stock);
            if (!stockValidation.IsValid)
                result.AddErrors(stockValidation.Errors);

            // Validar campos opcionales
            if (!string.IsNullOrEmpty(fruta.Categoria))
            {
                var categoriaValidation = ValidarCategoria(fruta.Categoria);
                if (!categoriaValidation.IsValid)
                    result.AddErrors(categoriaValidation.Errors);
            }

            if (!string.IsNullOrEmpty(fruta.PaisOrigen))
            {
                var paisValidation = ValidarPaisOrigen(fruta.PaisOrigen);
                if (!paisValidation.IsValid)
                    result.AddErrors(paisValidation.Errors);
            }

            if (!string.IsNullOrEmpty(fruta.Temporada))
            {
                var temporadaValidation = ValidarTemporada(fruta.Temporada);
                if (!temporadaValidation.IsValid)
                    result.AddErrors(temporadaValidation.Errors);
            }

            // Validar fecha de vencimiento
            if (fruta.FechaVencimiento.HasValue)
            {
                var fechaValidation = ValidarFechaVencimiento(fruta.FechaVencimiento.Value);
                if (!fechaValidation.IsValid)
                    result.AddErrors(fechaValidation.Errors);
            }

            return result;
        }

        #endregion

        #region Validaciones individuales

        /// <summary>
        /// Valida el nombre de una fruta
        /// </summary>
        public static ValidationResult ValidarNombre(string nombre)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                result.AddError("El nombre de la fruta es requerido");
                return result;
            }

            nombre = nombre.Trim();

            if (nombre.Length > MAX_NOMBRE_LENGTH)
                result.AddError($"El nombre no puede exceder {MAX_NOMBRE_LENGTH} caracteres");

            // Solo letras, espacios, acentos y algunos caracteres especiales
            var nombreRegex = new Regex(@"^[a-zA-Z��������������\s\-'\.()]+$");
            if (!nombreRegex.IsMatch(nombre))
                result.AddError("El nombre contiene caracteres no v�lidos");

            return result;
        }

        /// <summary>
        /// Valida la descripci�n de una fruta
        /// </summary>
        public static ValidationResult ValidarDescripcion(string descripcion)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(descripcion))
                return result; // Es opcional

            descripcion = descripcion.Trim();

            if (descripcion.Length > MAX_DESCRIPCION_LENGTH)
                result.AddError($"La descripci�n no puede exceder {MAX_DESCRIPCION_LENGTH} caracteres");

            return result;
        }

        /// <summary>
        /// Valida el precio de una fruta
        /// </summary>
        public static ValidationResult ValidarPrecio(decimal precio)
        {
            var result = new ValidationResult();

            if (precio < MIN_PRECIO)
                result.AddError($"El precio debe ser al menos ${MIN_PRECIO}");

            if (precio > MAX_PRECIO)
                result.AddError($"El precio no puede exceder ${MAX_PRECIO}");

            // Validar que tenga m�ximo 2 decimales
            if (decimal.Round(precio, 2) != precio)
                result.AddError("El precio no puede tener m�s de 2 decimales");

            return result;
        }

        /// <summary>
        /// Valida el stock de una fruta
        /// </summary>
        public static ValidationResult ValidarStock(int stock)
        {
            var result = new ValidationResult();

            if (stock < MIN_STOCK)
                result.AddError($"El stock no puede ser menor a {MIN_STOCK}");

            if (stock > MAX_STOCK)
                result.AddError($"El stock no puede exceder {MAX_STOCK}");

            return result;
        }

        /// <summary>
        /// Valida la categor�a de una fruta
        /// </summary>
        public static ValidationResult ValidarCategoria(string categoria)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(categoria))
                return result; // Es opcional

            categoria = categoria.Trim();

            if (categoria.Length > MAX_CATEGORIA_LENGTH)
                result.AddError($"La categor�a no puede exceder {MAX_CATEGORIA_LENGTH} caracteres");

            // Solo letras, espacios y algunos caracteres especiales
            var categoriaRegex = new Regex(@"^[a-zA-Z��������������\s\-'\.]+$");
            if (!categoriaRegex.IsMatch(categoria))
                result.AddError("La categor�a contiene caracteres no v�lidos");

            return result;
        }

        /// <summary>
        /// Valida el pa�s de origen de una fruta
        /// </summary>
        public static ValidationResult ValidarPaisOrigen(string pais)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(pais))
                return result; // Es opcional

            pais = pais.Trim();

            if (pais.Length > MAX_PAIS_LENGTH)
                result.AddError($"El pa�s de origen no puede exceder {MAX_PAIS_LENGTH} caracteres");

            // Solo letras, espacios y algunos caracteres especiales
            var paisRegex = new Regex(@"^[a-zA-Z��������������\s\-'\.]+$");
            if (!paisRegex.IsMatch(pais))
                result.AddError("El pa�s de origen contiene caracteres no v�lidos");

            return result;
        }

        /// <summary>
        /// Valida la temporada de una fruta
        /// </summary>
        public static ValidationResult ValidarTemporada(string temporada)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(temporada))
                return result; // Es opcional

            temporada = temporada.Trim();

            if (temporada.Length > MAX_TEMPORADA_LENGTH)
                result.AddError($"La temporada no puede exceder {MAX_TEMPORADA_LENGTH} caracteres");

            // Temporadas v�lidas
            var temporadasValidas = new[] { 
                "Primavera", "Verano", "Oto�o", "Invierno", "Todo el a�o",
                "Spring", "Summer", "Fall", "Winter", "All year"
            };

            if (!temporadasValidas.Any(t => string.Equals(t, temporada, StringComparison.OrdinalIgnoreCase)))
                result.AddError("La temporada especificada no es v�lida");

            return result;
        }

        /// <summary>
        /// Valida la fecha de vencimiento de una fruta
        /// </summary>
        public static ValidationResult ValidarFechaVencimiento(DateTime fechaVencimiento)
        {
            var result = new ValidationResult();

            if (fechaVencimiento <= DateTime.Now.Date)
                result.AddError("La fecha de vencimiento debe ser futura");

            // No puede ser m�s de 2 a�os en el futuro
            if (fechaVencimiento > DateTime.Now.Date.AddYears(2))
                result.AddError("La fecha de vencimiento no puede ser m�s de 2 a�os en el futuro");

            return result;
        }

        #endregion

        #region Validaciones de filtros

        /// <summary>
        /// Valida los filtros de b�squeda de frutas
        /// </summary>
        public static ValidationResult ValidarFiltros(FrutaFiltroDto filtros)
        {
            var result = new ValidationResult();

            // Validar paginaci�n
            if (filtros.Pagina < 1)
                result.AddError("La p�gina debe ser mayor a 0");

            if (filtros.Tama�oPagina < 1 || filtros.Tama�oPagina > 100)
                result.AddError("El tama�o de p�gina debe estar entre 1 y 100");

            // Validar rangos de precio
            if (filtros.PrecioMinimo.HasValue && filtros.PrecioMinimo.Value < 0)
                result.AddError("El precio m�nimo no puede ser negativo");

            if (filtros.PrecioMaximo.HasValue && filtros.PrecioMaximo.Value < 0)
                result.AddError("El precio m�ximo no puede ser negativo");

            if (filtros.PrecioMinimo.HasValue && filtros.PrecioMaximo.HasValue && 
                filtros.PrecioMinimo.Value > filtros.PrecioMaximo.Value)
                result.AddError("El precio m�nimo no puede ser mayor al precio m�ximo");

            // Validar stock m�nimo
            if (filtros.StockMinimo.HasValue && filtros.StockMinimo.Value < 0)
                result.AddError("El stock m�nimo no puede ser negativo");

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Clase helper para manejar resultados de validaci�n
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
                Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            if (errors != null)
                Errors.AddRange(errors.Where(e => !string.IsNullOrWhiteSpace(e)));
        }

        public string GetErrorsAsString()
        {
            return string.Join("; ", Errors);
        }
    }
}