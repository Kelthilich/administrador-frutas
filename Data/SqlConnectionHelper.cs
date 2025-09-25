using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace frutas.Data
{
    /// <summary>
    /// Helper para manejar conexiones a SQL Server
    /// Proporciona m�todos seguros y eficientes para acceso a datos
    /// </summary>
    public static class SqlConnectionHelper
    {
        #region Propiedades de Conexi�n
        /// <summary>
        /// Obtiene la cadena de conexi�n desde Web.config
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("No se encontr� la cadena de conexi�n 'DefaultConnection' en Web.config");
                }
                return connectionString;
            }
        }
        #endregion

        #region M�todos de Conexi�n
        /// <summary>
        /// Crea una nueva conexi�n SQL
        /// </summary>
        /// <returns>SqlConnection configurada</returns>
        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Crea y abre una nueva conexi�n SQL
        /// </summary>
        /// <returns>SqlConnection abierta</returns>
        public static SqlConnection CrearConexionAbierta()
        {
            var connection = CrearConexion();
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Verifica si la conexi�n a la base de datos es v�lida
        /// </summary>
        /// <returns>True si la conexi�n es exitosa</returns>
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

        #region M�todos de Ejecuci�n
        /// <summary>
        /// Ejecuta una consulta que no devuelve resultados
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Par�metros opcionales</param>
        /// <returns>N�mero de filas afectadas</returns>
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
        /// <param name="parametros">Par�metros opcionales</param>
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
        /// <param name="parametros">Par�metros opcionales</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader EjecutarReader(string sql, params SqlParameter[] parametros)
        {
            var connection = CrearConexionAbierta();
            var command = new SqlCommand(sql, connection);
            
            if (parametros != null)
            {
                command.Parameters.AddRange(parametros);
            }
            
            // CommandBehavior.CloseConnection cierra la conexi�n cuando se cierre el reader
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un DataTable
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Par�metros opcionales</param>
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

        #region M�todos de Transacci�n
        /// <summary>
        /// Ejecuta m�ltiples comandos en una transacci�n
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
        /// Ejecuta una acci�n dentro de una transacci�n
        /// </summary>
        /// <param name="accion">Acci�n a ejecutar</param>
        /// <returns>True si la transacci�n fue exitosa</returns>
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

        #region M�todos de Utilidad
        /// <summary>
        /// Crea un par�metro SQL con valor
        /// </summary>
        /// <param name="nombre">Nombre del par�metro</param>
        /// <param name="valor">Valor del par�metro</param>
        /// <returns>SqlParameter configurado</returns>
        public static SqlParameter CrearParametro(string nombre, object valor)
        {
            return new SqlParameter(nombre, valor ?? DBNull.Value);
        }

        /// <summary>
        /// Crea un par�metro SQL con tipo espec�fico
        /// </summary>
        /// <param name="nombre">Nombre del par�metro</param>
        /// <param name="tipo">Tipo SQL</param>
        /// <param name="valor">Valor del par�metro</param>
        /// <returns>SqlParameter configurado</returns>
        public static SqlParameter CrearParametro(string nombre, SqlDbType tipo, object valor)
        {
            return new SqlParameter(nombre, tipo) { Value = valor ?? DBNull.Value };
        }

        /// <summary>
        /// Escapar texto para prevenir SQL Injection en consultas din�micas
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
        /// Construye una consulta WHERE con m�ltiples condiciones
        /// </summary>
        /// <param name="condiciones">Diccionario de condiciones</param>
        /// <returns>Cl�usula WHERE</returns>
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
        /// Obtiene informaci�n de la base de datos
        /// </summary>
        /// <returns>String con informaci�n de la BD</returns>
        public static string ObtenerInfoBaseDatos()
        {
            try
            {
                using (var connection = CrearConexionAbierta())
                {
                    return $"Servidor: {connection.DataSource}, Base de datos: {connection.Database}, " +
                           $"Versi�n: {connection.ServerVersion}";
                }
            }
            catch (Exception ex)
            {
                return $"Error al obtener informaci�n: {ex.Message}";
            }
        }
        #endregion
    }
}