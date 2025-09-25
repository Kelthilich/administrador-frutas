using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using frutas.Data;
using frutas.Models;

namespace frutas.Repositories
{
    /// <summary>
    /// Implementación base del patrón Repository para .NET Framework 4.8
    /// Utiliza ADO.NET para acceso directo a datos
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly string _tableName;
        protected readonly string _connectionString;

        protected BaseRepository(string tableName)
        {
            _tableName = tableName;
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        #region Métodos abstractos que deben implementar las clases derivadas

        /// <summary>
        /// Mapea un SqlDataReader a una entidad
        /// </summary>
        protected abstract T MapearDesdeReader(SqlDataReader reader);

        /// <summary>
        /// Obtiene los parámetros SQL para insertar una entidad
        /// </summary>
        protected abstract SqlParameter[] ObtenerParametrosInsertar(T entidad);

        /// <summary>
        /// Obtiene los parámetros SQL para actualizar una entidad
        /// </summary>
        protected abstract SqlParameter[] ObtenerParametrosActualizar(T entidad);

        /// <summary>
        /// Obtiene el SQL para insertar una entidad
        /// </summary>
        protected abstract string ObtenerSqlInsertar();

        /// <summary>
        /// Obtiene el SQL para actualizar una entidad
        /// </summary>
        protected abstract string ObtenerSqlActualizar();

        #endregion

        #region Operaciones de consulta

        public virtual T ObtenerPorId(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearDesdeReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }

        public virtual IEnumerable<T> ObtenerTodos()
        {
            string sql = $"SELECT * FROM {_tableName} ORDER BY Id";
            return EjecutarConsulta(sql);
        }

        public virtual IEnumerable<T> ObtenerActivos()
        {
            string sql = $"SELECT * FROM {_tableName} WHERE Activo = 1 ORDER BY Id";
            return EjecutarConsulta(sql);
        }

        public virtual IEnumerable<T> ObtenerPaginado(int pagina, int tamañoPagina)
        {
            int offset = (pagina - 1) * tamañoPagina;
            string sql = $@"
                SELECT * FROM {_tableName} 
                ORDER BY Id 
                OFFSET @Offset ROWS 
                FETCH NEXT @TamañoPagina ROWS ONLY";

            var parametros = new[]
            {
                new SqlParameter("@Offset", offset),
                new SqlParameter("@TamañoPagina", tamañoPagina)
            };

            return EjecutarConsulta(sql, parametros);
        }

        public virtual bool Existe(int id)
        {
            string sql = $"SELECT COUNT(1) FROM {_tableName} WHERE Id = @Id";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }

        public virtual int Contar()
        {
            string sql = $"SELECT COUNT(*) FROM {_tableName}";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Operaciones de modificación

        public virtual T Agregar(T entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.Activo = true;

            string sql = ObtenerSqlInsertar();
            var parametros = ObtenerParametrosInsertar(entidad);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parametros);
                    
                    var nuevoId = command.ExecuteScalar();
                    if (nuevoId != null)
                    {
                        entidad.Id = Convert.ToInt32(nuevoId);
                    }
                }
            }

            return entidad;
        }

        public virtual void Actualizar(T entidad)
        {
            entidad.FechaModificacion = DateTime.Now;

            string sql = ObtenerSqlActualizar();
            var parametros = ObtenerParametrosActualizar(entidad);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parametros);
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void Eliminar(int id)
        {
            // Soft delete
            string sql = $"UPDATE {_tableName} SET Activo = 0, FechaModificacion = @FechaModificacion WHERE Id = @Id";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@FechaModificacion", DateTime.Now));
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void EliminarFisico(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void EliminarFisico(T entidad)
        {
            EliminarFisico(entidad.Id);
        }

        #endregion

        #region Operaciones por lotes

        public virtual void AgregarRango(IEnumerable<T> entidades)
        {
            foreach (var entidad in entidades)
            {
                Agregar(entidad);
            }
        }

        public virtual void ActualizarRango(IEnumerable<T> entidades)
        {
            foreach (var entidad in entidades)
            {
                Actualizar(entidad);
            }
        }

        public virtual void EliminarRango(IEnumerable<T> entidades)
        {
            foreach (var entidad in entidades)
            {
                Eliminar(entidad.Id);
            }
        }

        #endregion

        #region Métodos helper

        protected IEnumerable<T> EjecutarConsulta(string sql, SqlParameter[] parametros = null)
        {
            var resultados = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultados.Add(MapearDesdeReader(reader));
                        }
                    }
                }
            }

            return resultados;
        }

        protected int EjecutarComando(string sql, SqlParameter[] parametros = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        protected object EjecutarEscalar(string sql, SqlParameter[] parametros = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }

                    return command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Implementaciones pendientes (para mantener compatibilidad con la interfaz)

        // Estos métodos requieren funcionalidad específica que implementaremos cuando sea necesario
        public virtual IEnumerable<T> Buscar(Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual T BuscarUno(Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual bool Existe(Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual int Contar(Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual IEnumerable<T> ObtenerPaginado(int pagina, int tamañoPagina, Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual IEnumerable<T> ObtenerPaginadoOrdenado<TKey>(int pagina, int tamañoPagina, Expression<Func<T, TKey>> ordenarPor, bool descendente = false)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual IEnumerable<T> ObtenerPaginadoOrdenado<TKey>(int pagina, int tamañoPagina, Expression<Func<T, bool>> predicado, Expression<Func<T, TKey>> ordenarPor, bool descendente = false)
        {
            throw new NotImplementedException("Este método requiere Entity Framework o implementación específica");
        }

        public virtual void GuardarCambios()
        {
            // En ADO.NET los cambios se guardan inmediatamente
            // Este método se mantiene para compatibilidad con la interfaz
        }

        #endregion
    }
}