using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using frutas.Models;

namespace frutas.Repositories
{
    /// <summary>
    /// Implementación del repositorio de frutas
    /// Proporciona acceso a datos específico para la entidad Fruta
    /// </summary>
    public class FrutaRepository : BaseRepository<Fruta>, IFrutaRepository
    {
        public FrutaRepository() : base("Frutas")
        {
        }

        #region Implementación de métodos abstractos

        protected override Fruta MapearDesdeReader(SqlDataReader reader)
        {
            return new Fruta
            {
                Id = (int)reader["Id"],
                Nombre = reader["Nombre"].ToString(),
                Descripcion = reader["Descripcion"]?.ToString(),
                Precio = (decimal)reader["Precio"],
                Stock = (int)reader["Stock"],
                Categoria = reader["Categoria"]?.ToString(),
                PaisOrigen = reader["PaisOrigen"]?.ToString(),
                Temporada = reader["Temporada"]?.ToString(),
                EsOrganica = (bool)reader["EsOrganica"],
                FechaVencimiento = reader["FechaVencimiento"] as DateTime?,
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                FechaModificacion = reader["FechaModificacion"] as DateTime?,
                Activo = (bool)reader["Activo"],
                UsuarioCreacion = reader["UsuarioCreacion"] as int?,
                UsuarioModificacion = reader["UsuarioModificacion"] as int?
            };
        }

        protected override SqlParameter[] ObtenerParametrosInsertar(Fruta fruta)
        {
            return new[]
            {
                new SqlParameter("@Nombre", fruta.Nombre),
                new SqlParameter("@Descripcion", fruta.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@Precio", fruta.Precio),
                new SqlParameter("@Stock", fruta.Stock),
                new SqlParameter("@Categoria", fruta.Categoria ?? (object)DBNull.Value),
                new SqlParameter("@PaisOrigen", fruta.PaisOrigen ?? (object)DBNull.Value),
                new SqlParameter("@Temporada", fruta.Temporada ?? (object)DBNull.Value),
                new SqlParameter("@EsOrganica", fruta.EsOrganica),
                new SqlParameter("@FechaVencimiento", fruta.FechaVencimiento ?? (object)DBNull.Value),
                new SqlParameter("@FechaCreacion", fruta.FechaCreacion),
                new SqlParameter("@Activo", fruta.Activo),
                new SqlParameter("@UsuarioCreacion", fruta.UsuarioCreacion ?? (object)DBNull.Value)
            };
        }

        protected override SqlParameter[] ObtenerParametrosActualizar(Fruta fruta)
        {
            return new[]
            {
                new SqlParameter("@Id", fruta.Id),
                new SqlParameter("@Nombre", fruta.Nombre),
                new SqlParameter("@Descripcion", fruta.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@Precio", fruta.Precio),
                new SqlParameter("@Stock", fruta.Stock),
                new SqlParameter("@Categoria", fruta.Categoria ?? (object)DBNull.Value),
                new SqlParameter("@PaisOrigen", fruta.PaisOrigen ?? (object)DBNull.Value),
                new SqlParameter("@Temporada", fruta.Temporada ?? (object)DBNull.Value),
                new SqlParameter("@EsOrganica", fruta.EsOrganica),
                new SqlParameter("@FechaVencimiento", fruta.FechaVencimiento ?? (object)DBNull.Value),
                new SqlParameter("@FechaModificacion", fruta.FechaModificacion ?? (object)DBNull.Value),
                new SqlParameter("@Activo", fruta.Activo),
                new SqlParameter("@UsuarioModificacion", fruta.UsuarioModificacion ?? (object)DBNull.Value)
            };
        }

        protected override string ObtenerSqlInsertar()
        {
            return @"
                INSERT INTO Frutas (Nombre, Descripcion, Precio, Stock, Categoria, PaisOrigen, 
                                  Temporada, EsOrganica, FechaVencimiento, FechaCreacion, Activo, UsuarioCreacion)
                OUTPUT INSERTED.Id
                VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria, @PaisOrigen, 
                        @Temporada, @EsOrganica, @FechaVencimiento, @FechaCreacion, @Activo, @UsuarioCreacion)";
        }

        protected override string ObtenerSqlActualizar()
        {
            return @"
                UPDATE Frutas 
                SET Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    Precio = @Precio,
                    Stock = @Stock,
                    Categoria = @Categoria,
                    PaisOrigen = @PaisOrigen,
                    Temporada = @Temporada,
                    EsOrganica = @EsOrganica,
                    FechaVencimiento = @FechaVencimiento,
                    FechaModificacion = @FechaModificacion,
                    Activo = @Activo,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE Id = @Id";
        }

        #endregion

        #region Métodos específicos de Fruta

        public IEnumerable<Fruta> BuscarPorNombre(string nombre)
        {
            string sql = "SELECT * FROM Frutas WHERE Nombre LIKE @Nombre AND Activo = 1 ORDER BY Nombre";
            var parametros = new[] { new SqlParameter("@Nombre", $"%{nombre}%") };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<Fruta> ObtenerPorCategoria(string categoria)
        {
            string sql = "SELECT * FROM Frutas WHERE Categoria = @Categoria AND Activo = 1 ORDER BY Nombre";
            var parametros = new[] { new SqlParameter("@Categoria", categoria) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<Fruta> ObtenerPorPais(string pais)
        {
            string sql = "SELECT * FROM Frutas WHERE PaisOrigen = @PaisOrigen AND Activo = 1 ORDER BY Nombre";
            var parametros = new[] { new SqlParameter("@PaisOrigen", pais) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<Fruta> ObtenerOrganicas()
        {
            string sql = "SELECT * FROM Frutas WHERE EsOrganica = 1 AND Activo = 1 ORDER BY Nombre";
            return EjecutarConsulta(sql);
        }

        public IEnumerable<Fruta> ObtenerDisponibles()
        {
            string sql = @"
                SELECT * FROM Frutas 
                WHERE Activo = 1 
                AND Stock > 0 
                AND (FechaVencimiento IS NULL OR FechaVencimiento > GETDATE())
                ORDER BY Nombre";
            
            return EjecutarConsulta(sql);
        }

        public IEnumerable<Fruta> ObtenerPorRangoPrecio(decimal precioMin, decimal precioMax)
        {
            string sql = @"
                SELECT * FROM Frutas 
                WHERE Precio BETWEEN @PrecioMin AND @PrecioMax 
                AND Activo = 1 
                ORDER BY Precio";
            
            var parametros = new[]
            {
                new SqlParameter("@PrecioMin", precioMin),
                new SqlParameter("@PrecioMax", precioMax)
            };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<Fruta> ObtenerConStockBajo(int stockMinimo = 10)
        {
            string sql = "SELECT * FROM Frutas WHERE Stock <= @StockMinimo AND Activo = 1 ORDER BY Stock";
            var parametros = new[] { new SqlParameter("@StockMinimo", stockMinimo) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public IEnumerable<Fruta> ObtenerProximasAVencer(int dias = 7)
        {
            string sql = @"
                SELECT * FROM Frutas 
                WHERE FechaVencimiento IS NOT NULL 
                AND FechaVencimiento BETWEEN GETDATE() AND DATEADD(DAY, @Dias, GETDATE())
                AND Activo = 1
                ORDER BY FechaVencimiento";
            
            var parametros = new[] { new SqlParameter("@Dias", dias) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public decimal CalcularValorInventario()
        {
            string sql = "SELECT ISNULL(SUM(Precio * Stock), 0) FROM Frutas WHERE Activo = 1";
            
            var resultado = EjecutarEscalar(sql);
            return resultado != null ? Convert.ToDecimal(resultado) : 0;
        }

        public IEnumerable<string> ObtenerCategorias()
        {
            string sql = @"
                SELECT DISTINCT Categoria 
                FROM Frutas 
                WHERE Categoria IS NOT NULL 
                AND Activo = 1 
                ORDER BY Categoria";
            
            var categorias = new List<string>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(reader["Categoria"].ToString());
                    }
                }
            }
            
            return categorias;
        }

        public IEnumerable<string> ObtenerPaises()
        {
            string sql = @"
                SELECT DISTINCT PaisOrigen 
                FROM Frutas 
                WHERE PaisOrigen IS NOT NULL 
                AND Activo = 1 
                ORDER BY PaisOrigen";
            
            var paises = new List<string>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paises.Add(reader["PaisOrigen"].ToString());
                    }
                }
            }
            
            return paises;
        }

        public IEnumerable<string> ObtenerTemporadas()
        {
            string sql = @"
                SELECT DISTINCT Temporada 
                FROM Frutas 
                WHERE Temporada IS NOT NULL 
                AND Activo = 1 
                ORDER BY Temporada";
            
            var temporadas = new List<string>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temporadas.Add(reader["Temporada"].ToString());
                    }
                }
            }
            
            return temporadas;
        }

        public void ActualizarStock(int frutaId, int nuevoStock)
        {
            string sql = @"
                UPDATE Frutas 
                SET Stock = @Stock, 
                    FechaModificacion = @FechaModificacion 
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", frutaId),
                new SqlParameter("@Stock", nuevoStock),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void ReducirStock(int frutaId, int cantidad)
        {
            string sql = @"
                UPDATE Frutas 
                SET Stock = Stock - @Cantidad, 
                    FechaModificacion = @FechaModificacion 
                WHERE Id = @Id 
                AND Stock >= @Cantidad";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", frutaId),
                new SqlParameter("@Cantidad", cantidad),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            var filasAfectadas = EjecutarComando(sql, parametros);
            
            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException("No se pudo reducir el stock. Stock insuficiente o fruta no encontrada.");
            }
        }

        #endregion

        #region Métodos de búsqueda avanzada

        /// <summary>
        /// Búsqueda avanzada con múltiples filtros
        /// </summary>
        public IEnumerable<Fruta> BuscarConFiltros(string nombre = null, string categoria = null, 
            string pais = null, string temporada = null, bool? esOrganica = null, 
            decimal? precioMin = null, decimal? precioMax = null, int? stockMin = null, 
            bool soloDisponibles = true, bool soloActivos = true)
        {
            var sql = new System.Text.StringBuilder("SELECT * FROM Frutas WHERE 1=1");
            var parametros = new List<SqlParameter>();

            if (soloActivos)
            {
                sql.Append(" AND Activo = 1");
            }

            if (soloDisponibles)
            {
                sql.Append(" AND Stock > 0 AND (FechaVencimiento IS NULL OR FechaVencimiento > GETDATE())");
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                sql.Append(" AND Nombre LIKE @Nombre");
                parametros.Add(new SqlParameter("@Nombre", $"%{nombre}%"));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                sql.Append(" AND Categoria = @Categoria");
                parametros.Add(new SqlParameter("@Categoria", categoria));
            }

            if (!string.IsNullOrEmpty(pais))
            {
                sql.Append(" AND PaisOrigen = @PaisOrigen");
                parametros.Add(new SqlParameter("@PaisOrigen", pais));
            }

            if (!string.IsNullOrEmpty(temporada))
            {
                sql.Append(" AND Temporada = @Temporada");
                parametros.Add(new SqlParameter("@Temporada", temporada));
            }

            if (esOrganica.HasValue)
            {
                sql.Append(" AND EsOrganica = @EsOrganica");
                parametros.Add(new SqlParameter("@EsOrganica", esOrganica.Value));
            }

            if (precioMin.HasValue)
            {
                sql.Append(" AND Precio >= @PrecioMin");
                parametros.Add(new SqlParameter("@PrecioMin", precioMin.Value));
            }

            if (precioMax.HasValue)
            {
                sql.Append(" AND Precio <= @PrecioMax");
                parametros.Add(new SqlParameter("@PrecioMax", precioMax.Value));
            }

            if (stockMin.HasValue)
            {
                sql.Append(" AND Stock >= @StockMin");
                parametros.Add(new SqlParameter("@StockMin", stockMin.Value));
            }

            sql.Append(" ORDER BY Nombre");

            return EjecutarConsulta(sql.ToString(), parametros.ToArray());
        }

        /// <summary>
        /// Búsqueda paginada con filtros
        /// </summary>
        public IEnumerable<Fruta> BuscarPaginadoConFiltros(int pagina, int tamañoPagina, 
            string nombre = null, string categoria = null, string pais = null, 
            string temporada = null, bool? esOrganica = null, decimal? precioMin = null, 
            decimal? precioMax = null, int? stockMin = null, bool soloDisponibles = true, 
            bool soloActivos = true, string ordenarPor = "Nombre", bool descendente = false)
        {
            int offset = (pagina - 1) * tamañoPagina;
            var sql = new System.Text.StringBuilder("SELECT * FROM Frutas WHERE 1=1");
            var parametros = new List<SqlParameter>();

            // Agregar filtros (mismo código que el método anterior)
            if (soloActivos)
            {
                sql.Append(" AND Activo = 1");
            }

            if (soloDisponibles)
            {
                sql.Append(" AND Stock > 0 AND (FechaVencimiento IS NULL OR FechaVencimiento > GETDATE())");
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                sql.Append(" AND Nombre LIKE @Nombre");
                parametros.Add(new SqlParameter("@Nombre", $"%{nombre}%"));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                sql.Append(" AND Categoria = @Categoria");
                parametros.Add(new SqlParameter("@Categoria", categoria));
            }

            if (!string.IsNullOrEmpty(pais))
            {
                sql.Append(" AND PaisOrigen = @PaisOrigen");
                parametros.Add(new SqlParameter("@PaisOrigen", pais));
            }

            if (!string.IsNullOrEmpty(temporada))
            {
                sql.Append(" AND Temporada = @Temporada");
                parametros.Add(new SqlParameter("@Temporada", temporada));
            }

            if (esOrganica.HasValue)
            {
                sql.Append(" AND EsOrganica = @EsOrganica");
                parametros.Add(new SqlParameter("@EsOrganica", esOrganica.Value));
            }

            if (precioMin.HasValue)
            {
                sql.Append(" AND Precio >= @PrecioMin");
                parametros.Add(new SqlParameter("@PrecioMin", precioMin.Value));
            }

            if (precioMax.HasValue)
            {
                sql.Append(" AND Precio <= @PrecioMax");
                parametros.Add(new SqlParameter("@PrecioMax", precioMax.Value));
            }

            if (stockMin.HasValue)
            {
                sql.Append(" AND Stock >= @StockMin");
                parametros.Add(new SqlParameter("@StockMin", stockMin.Value));
            }

            // Agregar ordenamiento y paginación
            string direccion = descendente ? "DESC" : "ASC";
            sql.Append($" ORDER BY {ordenarPor} {direccion}");
            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @TamañoPagina ROWS ONLY");

            parametros.Add(new SqlParameter("@Offset", offset));
            parametros.Add(new SqlParameter("@TamañoPagina", tamañoPagina));

            return EjecutarConsulta(sql.ToString(), parametros.ToArray());
        }

        #endregion
    }
}