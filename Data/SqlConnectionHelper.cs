using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace frutas.Data
{
    /// <summary>
    /// Helper para manejar conexiones a SQL Server
    /// Proporciona métodos seguros y eficientes para acceso a datos
    /// </summary>
    public static class SqlConnectionHelper
    {
        #region Propiedades de Conexión
        /// <summary>
        /// Obtiene la cadena de conexión desde Web.config
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en Web.config");
                }
                return connectionString;
            }
        }
        #endregion

        #region Métodos de Conexión
        /// <summary>
        /// Crea una nueva conexión SQL
        /// </summary>
        /// <returns>SqlConnection configurada</returns>
        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Crea y abre una nueva conexión SQL
        /// </summary>
        /// <returns>SqlConnection abierta</returns>
        public static SqlConnection CrearConexionAbierta()
        {
            var connection = CrearConexion();
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Verifica si la conexión a la base de datos es válida
        /// </summary>
        /// <returns>True si la conexión es exitosa</returns>
        public static bool VerificarConexion()
        {
            try
            {
                using (var connection = CrearConexion())
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Métodos de Ejecución
        /// <summary>
        /// Ejecuta una consulta que no devuelve resultados
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parámetros opcionales</param>
        /// <returns>Número de filas afectadas</returns>
        public static int EjecutarComando(string sql, params SqlParameter[] parametros)
        {
            using (var connection = CrearConexionAbierta())
            using (var command = new SqlCommand(sql, connection))
            {
                if (parametros != null)
                {
                    command.Parameters.AddRange(parametros);
                }
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un valor escalar
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parámetros opcionales</param>
        /// <returns>Valor escalar</returns>
        public static object EjecutarEscalar(string sql, params SqlParameter[] parametros)
        {
            using (var connection = CrearConexionAbierta())
            using (var command = new SqlCommand(sql, connection))
            {
                if (parametros != null)
                {
                    command.Parameters.AddRange(parametros);
                }
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un DataReader
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parámetros opcionales</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader EjecutarReader(string sql, params SqlParameter[] parametros)
        {
            var connection = CrearConexionAbierta();
            var command = new SqlCommand(sql, connection);
            
            if (parametros != null)
            {
                command.Parameters.AddRange(parametros);
            }
            
            // CommandBehavior.CloseConnection cierra la conexión cuando se cierre el reader
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un DataTable
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parámetros opcionales</param>
        /// <returns>DataTable con los resultados</returns>
        public static DataTable EjecutarDataTable(string sql, params SqlParameter[] parametros)
        {
            using (var connection = CrearConexionAbierta())
            using (var command = new SqlCommand(sql, connection))
            {
                if (parametros != null)
                {
                    command.Parameters.AddRange(parametros);
                }
                
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        #endregion

        #region Métodos de Transacción
        /// <summary>
        /// Ejecuta múltiples comandos en una transacción
        /// </summary>
        /// <param name="comandos">Array de comandos SQL</param>
        /// <returns>True si todos los comandos se ejecutaron correctamente</returns>
        public static bool EjecutarTransaccion(params (string sql, SqlParameter[] parametros)[] comandos)
        {
            using (var connection = CrearConexionAbierta())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var (sql, parametros) in comandos)
                    {
                        using (var command = new SqlCommand(sql, connection, transaction))
                        {
                            if (parametros != null)
                            {
                                command.Parameters.AddRange(parametros);
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Ejecuta una acción dentro de una transacción
        /// </summary>
        /// <param name="accion">Acción a ejecutar</param>
        /// <returns>True si la transacción fue exitosa</returns>
        public static bool EjecutarEnTransaccion(Action<SqlConnection, SqlTransaction> accion)
        {
            using (var connection = CrearConexionAbierta())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    accion(connection, transaction);
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region Métodos de Utilidad
        /// <summary>
        /// Crea un parámetro SQL con valor
        /// </summary>
        /// <param name="nombre">Nombre del parámetro</param>
        /// <param name="valor">Valor del parámetro</param>
        /// <returns>SqlParameter configurado</returns>
        public static SqlParameter CrearParametro(string nombre, object valor)
        {
            return new SqlParameter(nombre, valor ?? DBNull.Value);
        }

        /// <summary>
        /// Crea un parámetro SQL con tipo específico
        /// </summary>
        /// <param name="nombre">Nombre del parámetro</param>
        /// <param name="tipo">Tipo SQL</param>
        /// <param name="valor">Valor del parámetro</param>
        /// <returns>SqlParameter configurado</returns>
        public static SqlParameter CrearParametro(string nombre, SqlDbType tipo, object valor)
        {
            return new SqlParameter(nombre, tipo) { Value = valor ?? DBNull.Value };
        }

        /// <summary>
        /// Escapar texto para prevenir SQL Injection en consultas dinámicas
        /// </summary>
        /// <param name="texto">Texto a escapar</param>
        /// <returns>Texto escapado</returns>
        public static string EscaparTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return texto.Replace("'", "''");
        }

        /// <summary>
        /// Construye una consulta WHERE con múltiples condiciones
        /// </summary>
        /// <param name="condiciones">Diccionario de condiciones</param>
        /// <returns>Cláusula WHERE</returns>
        public static string ConstruirWhere(System.Collections.Generic.Dictionary<string, object> condiciones)
        {
            if (condiciones == null || condiciones.Count == 0)
                return string.Empty;

            var clausulas = new System.Collections.Generic.List<string>();
            foreach (var condicion in condiciones)
            {
                if (condicion.Value != null)
                {
                    clausulas.Add($"{condicion.Key} = @{condicion.Key}");
                }
            }

            return clausulas.Count > 0 ? "WHERE " + string.Join(" AND ", clausulas) : string.Empty;
        }

        /// <summary>
        /// Obtiene información de la base de datos
        /// </summary>
        /// <returns>String con información de la BD</returns>
        public static string ObtenerInfoBaseDatos()
        {
            try
            {
                using (var connection = CrearConexionAbierta())
                {
                    return $"Servidor: {connection.DataSource}, Base de datos: {connection.Database}, " +
                           $"Versión: {connection.ServerVersion}";
                }
            }
            catch (Exception ex)
            {
                return $"Error al obtener información: {ex.Message}";
            }
        }
        #endregion
    }
}