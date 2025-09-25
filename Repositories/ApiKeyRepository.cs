using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using frutas.Models;

namespace frutas.Repositories
{
    /// <summary>
    /// Implementación del repositorio de API Keys
    /// Proporciona acceso a datos específico para la gestión de API Keys
    /// </summary>
    public class ApiKeyRepository : BaseRepository<ApiKey>, IApiKeyRepository
    {
        public ApiKeyRepository() : base("ApiKeys")
        {
        }

        #region Implementación de métodos abstractos

        protected override ApiKey MapearDesdeReader(SqlDataReader reader)
        {
            return new ApiKey
            {
                Id = (int)reader["Id"],
                KeyValue = reader["KeyValue"].ToString(),
                UsuarioId = (int)reader["UsuarioId"],
                Nombre = reader["Nombre"].ToString(),
                Descripcion = reader["Descripcion"]?.ToString(),
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                FechaExpiracion = reader["FechaExpiracion"] as DateTime?,
                Activo = (bool)reader["Activo"],
                UltimoUso = reader["UltimoUso"] as DateTime?,
                ContadorUsos = (int)reader["ContadorUsos"],
                LimitePorHora = (int)reader["LimitePorHora"],
                Permisos = reader["Permisos"].ToString(),
                IPsPermitidas = reader["IPsPermitidas"]?.ToString(),
                UsuarioCreacion = reader["UsuarioId"] as int?,
                UsuarioModificacion = reader["UsuarioModificacion"] as int?
            };
        }

        protected override SqlParameter[] ObtenerParametrosInsertar(ApiKey apiKey)
        {
            return new[]
            {
                new SqlParameter("@KeyValue", apiKey.KeyValue),
                new SqlParameter("@UsuarioId", apiKey.UsuarioId),
                new SqlParameter("@Nombre", apiKey.Nombre),
                new SqlParameter("@Descripcion", apiKey.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@FechaCreacion", apiKey.FechaCreacion),
                new SqlParameter("@FechaExpiracion", apiKey.FechaExpiracion ?? (object)DBNull.Value),
                new SqlParameter("@Activo", apiKey.Activo),
                new SqlParameter("@ContadorUsos", apiKey.ContadorUsos),
                new SqlParameter("@LimitePorHora", apiKey.LimitePorHora),
                new SqlParameter("@Permisos", apiKey.Permisos),
                new SqlParameter("@IPsPermitidas", apiKey.IPsPermitidas ?? (object)DBNull.Value),
                new SqlParameter("@UsuarioCreacion", apiKey.UsuarioCreacion ?? (object)DBNull.Value)
            };
        }

        protected override SqlParameter[] ObtenerParametrosActualizar(ApiKey apiKey)
        {
            return new[]
            {
                new SqlParameter("@Id", apiKey.Id),
                new SqlParameter("@KeyValue", apiKey.KeyValue),
                new SqlParameter("@UsuarioId", apiKey.UsuarioId),
                new SqlParameter("@Nombre", apiKey.Nombre),
                new SqlParameter("@Descripcion", apiKey.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@FechaModificacion", apiKey.FechaModificacion ?? (object)DBNull.Value),
                new SqlParameter("@FechaExpiracion", apiKey.FechaExpiracion ?? (object)DBNull.Value),
                new SqlParameter("@Activo", apiKey.Activo),
                new SqlParameter("@UltimoUso", apiKey.UltimoUso ?? (object)DBNull.Value),
                new SqlParameter("@ContadorUsos", apiKey.ContadorUsos),
                new SqlParameter("@LimitePorHora", apiKey.LimitePorHora),
                new SqlParameter("@Permisos", apiKey.Permisos),
                new SqlParameter("@IPsPermitidas", apiKey.IPsPermitidas ?? (object)DBNull.Value),
                new SqlParameter("@UsuarioModificacion", apiKey.UsuarioModificacion ?? (object)DBNull.Value)
            };
        }

        protected override string ObtenerSqlInsertar()
        {
            return @"
                INSERT INTO ApiKeys (KeyValue, UsuarioId, Nombre, Descripcion, FechaCreacion, 
                                   FechaExpiracion, Activo, ContadorUsos, LimitePorHora, 
                                   Permisos, IPsPermitidas)
                OUTPUT INSERTED.Id
                VALUES (@KeyValue, @UsuarioId, @Nombre, @Descripcion, @FechaCreacion, 
                        @FechaExpiracion, @Activo, @ContadorUsos, @LimitePorHora, 
                        @Permisos, @IPsPermitidas)";
        }

        protected override string ObtenerSqlActualizar()
        {
            return @"
                UPDATE ApiKeys 
                SET KeyValue = @KeyValue,
                    UsuarioId = @UsuarioId,
                    Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    FechaExpiracion = @FechaExpiracion,
                    Activo = @Activo,
                    UltimoUso = @UltimoUso,
                    ContadorUsos = @ContadorUsos,
                    LimitePorHora = @LimitePorHora,
                    Permisos = @Permisos,
                    IPsPermitidas = @IPsPermitidas
                WHERE Id = @Id";
        }

        #endregion

        #region Métodos específicos de API Key

        public ApiKey ObtenerPorKeyValue(string keyValue)
        {
            string sql = "SELECT * FROM ApiKeys WHERE KeyValue = @KeyValue";
            var parametros = new[] { new SqlParameter("@KeyValue", keyValue) };
            
            var resultados = EjecutarConsulta(sql, parametros);
            foreach (var apiKey in resultados)
            {
                return apiKey; // Retorna el primero
            }
            
            return null;
        }

        public IEnumerable<ApiKey> ObtenerPorUsuario(int usuarioId)
        {
            string sql = "SELECT * FROM ApiKeys WHERE UsuarioId = @UsuarioId ORDER BY FechaCreacion DESC";
            var parametros = new[] { new SqlParameter("@UsuarioId", usuarioId) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<ApiKey> ObtenerActivas()
        {
            string sql = @"
                SELECT * FROM ApiKeys 
                WHERE Activo = 1 
                AND (FechaExpiracion IS NULL OR FechaExpiracion > GETDATE())
                ORDER BY FechaCreacion DESC";
            
            return EjecutarConsulta(sql);
        }

        public IEnumerable<ApiKey> ObtenerVencidas()
        {
            string sql = @"
                SELECT * FROM ApiKeys 
                WHERE FechaExpiracion IS NOT NULL 
                AND FechaExpiracion <= GETDATE()
                ORDER BY FechaExpiracion DESC";
            
            return EjecutarConsulta(sql);
        }

        public void RegistrarUso(string keyValue)
        {
            string sql = @"
                UPDATE ApiKeys 
                SET UltimoUso = @UltimoUso,
                    ContadorUsos = ContadorUsos + 1
                WHERE KeyValue = @KeyValue";
            
            var parametros = new[]
            {
                new SqlParameter("@KeyValue", keyValue),
                new SqlParameter("@UltimoUso", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void RevocarApiKey(int apiKeyId)
        {
            string sql = @"
                UPDATE ApiKeys 
                SET Activo = 0,
                    FechaModificacion = @FechaModificacion
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", apiKeyId),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void RevocarApiKeysPorUsuario(int usuarioId)
        {
            string sql = @"
                UPDATE ApiKeys 
                SET Activo = 0,
                    FechaModificacion = @FechaModificacion
                WHERE UsuarioId = @UsuarioId
                AND Activo = 1";
            
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", usuarioId),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public bool ValidarApiKey(string keyValue, string ip = null)
        {
            var apiKey = ObtenerPorKeyValue(keyValue);
            
            if (apiKey == null || !apiKey.EsValida())
                return false;

            // Verificar IP si está especificada
            if (!string.IsNullOrEmpty(ip) && !apiKey.IPEstaPermitida(ip))
                return false;

            // Registrar uso
            RegistrarUso(keyValue);
            
            return true;
        }

        public void LimpiarApiKeysVencidas()
        {
            string sql = @"
                UPDATE ApiKeys 
                SET Activo = 0,
                    FechaModificacion = @FechaModificacion
                WHERE FechaExpiracion IS NOT NULL 
                AND FechaExpiracion <= GETDATE()
                AND Activo = 1";
            
            var parametros = new[]
            {
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        #endregion

        #region Métodos de estadísticas

        /// <summary>
        /// Obtiene estadísticas de uso de API Keys por usuario
        /// </summary>
        public Dictionary<int, int> ObtenerEstadisticasUsoPorUsuario()
        {
            string sql = @"
                SELECT UsuarioId, SUM(ContadorUsos) as TotalUsos
                FROM ApiKeys 
                WHERE Activo = 1
                GROUP BY UsuarioId";

            var estadisticas = new Dictionary<int, int>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int usuarioId = (int)reader["UsuarioId"];
                        int totalUsos = Convert.ToInt32(reader["TotalUsos"]);
                        estadisticas[usuarioId] = totalUsos;
                    }
                }
            }

            return estadisticas;
        }

        /// <summary>
        /// Obtiene las API Keys más utilizadas
        /// </summary>
        public IEnumerable<ApiKey> ObtenerMasUtilizadas(int limite = 10)
        {
            string sql = @"
                SELECT TOP (@Limite) * 
                FROM ApiKeys 
                WHERE Activo = 1
                ORDER BY ContadorUsos DESC";

            var parametros = new[] { new SqlParameter("@Limite", limite) };
            return EjecutarConsulta(sql, parametros);
        }

        /// <summary>
        /// Verifica si el usuario ha excedido el límite por hora
        /// </summary>
        public bool VerificarLimitePorHora(string keyValue)
        {
            string sql = @"
                SELECT LimitePorHora, ContadorUsos, UltimoUso
                FROM ApiKeys 
                WHERE KeyValue = @KeyValue";

            var parametros = new[] { new SqlParameter("@KeyValue", keyValue) };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parametros);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int limitePorHora = (int)reader["LimitePorHora"];
                            DateTime? ultimoUso = reader["UltimoUso"] as DateTime?;

                            if (limitePorHora <= 0) return true; // Sin límite

                            if (!ultimoUso.HasValue) return true; // Primera vez

                            // Si han pasado más de una hora, resetear el contador
                            if (DateTime.Now.Subtract(ultimoUso.Value).TotalHours >= 1)
                            {
                                ResetearContadorHora(keyValue);
                                return true;
                            }

                            // Contar usos en la última hora
                            int usosEnUltimaHora = ContarUsosEnUltimaHora(keyValue);
                            return usosEnUltimaHora < limitePorHora;
                        }
                    }
                }
            }

            return false;
        }

        private int ContarUsosEnUltimaHora(string keyValue)
        {
            // Para simplificar, usamos el contador total
            // En una implementación real, tendríamos una tabla de logs de uso por hora
            string sql = @"
                SELECT ContadorUsos 
                FROM ApiKeys 
                WHERE KeyValue = @KeyValue
                AND UltimoUso >= DATEADD(HOUR, -1, GETDATE())";

            var parametros = new[] { new SqlParameter("@KeyValue", keyValue) };
            var resultado = EjecutarEscalar(sql, parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }

        private void ResetearContadorHora(string keyValue)
        {
            // En una implementación completa, esto resetearía un contador específico por hora
            // Por simplicidad, no implementamos reset del contador principal
        }

        #endregion
    }
}