using System;
using System.Security.Cryptography;
using System.Text;

namespace frutas.Security
{
    /// <summary>
    /// Helper para manejar el hash y validación de contraseñas
    /// Implementa mejores prácticas de seguridad con salt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Longitud del salt en bytes
        /// </summary>
        private const int SaltSize = 32;

        /// <summary>
        /// Número de iteraciones para el hash
        /// </summary>
        private const int HashIterations = 10000;

        /// <summary>
        /// Longitud del hash en bytes
        /// </summary>
        private const int HashSize = 32;

        /// <summary>
        /// Genera un salt aleatorio
        /// </summary>
        /// <returns>Salt en formato Base64</returns>
        public static string GenerarSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        /// <summary>
        /// Crea un hash de contraseña usando PBKDF2
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <param name="salt">Salt en formato Base64</param>
        /// <returns>Hash de la contraseña en formato Base64</returns>
        public static string CrearHash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("El salt no puede estar vacío", nameof(salt));

            byte[] saltBytes = Convert.FromBase64String(salt);
            
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, HashIterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verifica si una contraseña coincide con el hash almacenado
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <param name="storedHash">Hash almacenado</param>
        /// <param name="salt">Salt usado para crear el hash</param>
        /// <returns>True si la contraseña es correcta</returns>
        public static bool VerificarPassword(string password, string storedHash, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(salt))
                return false;

            try
            {
                string hashToVerify = CrearHash(password, salt);
                return hashToVerify == storedHash;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Crea un hash completo (genera salt y hash en una operación)
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Tupla con el hash y el salt</returns>
        public static (string Hash, string Salt) CrearHashCompleto(string password)
        {
            string salt = GenerarSalt();
            string hash = CrearHash(password, salt);
            return (hash, salt);
        }

        /// <summary>
        /// Valida la fortaleza de una contraseña
        /// </summary>
        /// <param name="password">Contraseña a validar</param>
        /// <returns>Mensaje de validación o null si es válida</returns>
        public static string ValidarFortaleza(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "La contraseña es requerida";

            if (password.Length < 8)
                return "La contraseña debe tener al menos 8 caracteres";

            if (password.Length > 128)
                return "La contraseña no puede exceder 128 caracteres";

            bool tieneMinuscula = false;
            bool tieneMayuscula = false;
            bool tieneNumero = false;
            bool tieneEspecial = false;

            foreach (char c in password)
            {
                if (char.IsLower(c)) tieneMinuscula = true;
                else if (char.IsUpper(c)) tieneMayuscula = true;
                else if (char.IsDigit(c)) tieneNumero = true;
                else if (!char.IsLetterOrDigit(c)) tieneEspecial = true;
            }

            if (!tieneMinuscula)
                return "La contraseña debe contener al menos una letra minúscula";

            if (!tieneMayuscula)
                return "La contraseña debe contener al menos una letra mayúscula";

            if (!tieneNumero)
                return "La contraseña debe contener al menos un número";

            if (!tieneEspecial)
                return "La contraseña debe contener al menos un carácter especial";

            return null; // Contraseña válida
        }

        /// <summary>
        /// Genera una contraseña temporal aleatoria
        /// </summary>
        /// <param name="longitud">Longitud de la contraseña (mínimo 8)</param>
        /// <returns>Contraseña temporal</returns>
        public static string GenerarPasswordTemporal(int longitud = 12)
        {
            if (longitud < 8) longitud = 8;

            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";
            const string especiales = "!@#$%^&*";
            const string todos = minusculas + mayusculas + numeros + especiales;

            using (var rng = new RNGCryptoServiceProvider())
            {
                var password = new StringBuilder();
                byte[] randomBytes = new byte[4];

                // Garantizar al menos un carácter de cada tipo
                rng.GetBytes(randomBytes);
                password.Append(minusculas[randomBytes[0] % minusculas.Length]);
                
                rng.GetBytes(randomBytes);
                password.Append(mayusculas[randomBytes[0] % mayusculas.Length]);
                
                rng.GetBytes(randomBytes);
                password.Append(numeros[randomBytes[0] % numeros.Length]);
                
                rng.GetBytes(randomBytes);
                password.Append(especiales[randomBytes[0] % especiales.Length]);

                // Completar con caracteres aleatorios
                for (int i = 4; i < longitud; i++)
                {
                    rng.GetBytes(randomBytes);
                    password.Append(todos[randomBytes[0] % todos.Length]);
                }

                // Mezclar los caracteres
                return MezclarString(password.ToString());
            }
        }

        /// <summary>
        /// Mezcla los caracteres de un string aleatoriamente
        /// </summary>
        private static string MezclarString(string input)
        {
            char[] array = input.ToCharArray();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                for (int i = array.Length - 1; i > 0; i--)
                {
                    rng.GetBytes(randomBytes);
                    int j = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % (i + 1);
                    char temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            return new string(array);
        }
    }
}