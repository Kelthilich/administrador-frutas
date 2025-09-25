using System;
using System.Security.Cryptography;
using System.Text;

namespace frutas.Security
{
    /// <summary>
    /// Helper para manejar el hash y validaci�n de contrase�as
    /// Implementa mejores pr�cticas de seguridad con salt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Longitud del salt en bytes
        /// </summary>
        private const int SaltSize = 32;

        /// <summary>
        /// N�mero de iteraciones para el hash
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
        /// Crea un hash de contrase�a usando PBKDF2
        /// </summary>
        /// <param name="password">Contrase�a en texto plano</param>
        /// <param name="salt">Salt en formato Base64</param>
        /// <returns>Hash de la contrase�a en formato Base64</returns>
        public static string CrearHash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contrase�a no puede estar vac�a", nameof(password));

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("El salt no puede estar vac�o", nameof(salt));

            byte[] saltBytes = Convert.FromBase64String(salt);
            
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, HashIterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verifica si una contrase�a coincide con el hash almacenado
        /// </summary>
        /// <param name="password">Contrase�a en texto plano</param>
        /// <param name="storedHash">Hash almacenado</param>
        /// <param name="salt">Salt usado para crear el hash</param>
        /// <returns>True si la contrase�a es correcta</returns>
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
        /// Crea un hash completo (genera salt y hash en una operaci�n)
        /// </summary>
        /// <param name="password">Contrase�a en texto plano</param>
        /// <returns>Tupla con el hash y el salt</returns>
        public static (string Hash, string Salt) CrearHashCompleto(string password)
        {
            string salt = GenerarSalt();
            string hash = CrearHash(password, salt);
            return (hash, salt);
        }

        /// <summary>
        /// Valida la fortaleza de una contrase�a
        /// </summary>
        /// <param name="password">Contrase�a a validar</param>
        /// <returns>Mensaje de validaci�n o null si es v�lida</returns>
        public static string ValidarFortaleza(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "La contrase�a es requerida";

            if (password.Length < 8)
                return "La contrase�a debe tener al menos 8 caracteres";

            if (password.Length > 128)
                return "La contrase�a no puede exceder 128 caracteres";

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
                return "La contrase�a debe contener al menos una letra min�scula";

            if (!tieneMayuscula)
                return "La contrase�a debe contener al menos una letra may�scula";

            if (!tieneNumero)
                return "La contrase�a debe contener al menos un n�mero";

            if (!tieneEspecial)
                return "La contrase�a debe contener al menos un car�cter especial";

            return null; // Contrase�a v�lida
        }

        /// <summary>
        /// Genera una contrase�a temporal aleatoria
        /// </summary>
        /// <param name="longitud">Longitud de la contrase�a (m�nimo 8)</param>
        /// <returns>Contrase�a temporal</returns>
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

                // Garantizar al menos un car�cter de cada tipo
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