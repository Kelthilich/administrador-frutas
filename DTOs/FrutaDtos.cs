using System;
using System.ComponentModel.DataAnnotations;

namespace frutas.DTOs
{
    /// <summary>
    /// DTO para transferir datos de fruta
    /// </summary>
    [Serializable]
    public class FrutaDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre de la fruta es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 999,999.99")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        public string Categoria { get; set; }

        [StringLength(50, ErrorMessage = "El país de origen no puede exceder 50 caracteres")]
        public string PaisOrigen { get; set; }

        [StringLength(30, ErrorMessage = "La temporada no puede exceder 30 caracteres")]
        public string Temporada { get; set; }

        public bool EsOrganica { get; set; } = false;

        public DateTime? FechaVencimiento { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }

        // Información de auditoría
        public string UsuarioCreador { get; set; }
        public string UsuarioModificador { get; set; }

        // Propiedades calculadas (no serializadas)
        [NonSerialized]
        private decimal? _valorStock;
        public decimal ValorStock => _valorStock ?? (Precio * Stock);
        
        [NonSerialized]
        private bool? _estaDisponible;
        public bool EstaDisponible => _estaDisponible ?? (Activo && Stock > 0 && 
                                     (FechaVencimiento == null || FechaVencimiento > DateTime.Now));
    }

    /// <summary>
    /// DTO para crear/actualizar fruta
    /// </summary>
    public class FrutaFormDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El nombre de la fruta es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 999,999.99")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        public string Categoria { get; set; }

        [StringLength(50, ErrorMessage = "El país de origen no puede exceder 50 caracteres")]
        public string PaisOrigen { get; set; }

        [StringLength(30, ErrorMessage = "La temporada no puede exceder 30 caracteres")]
        public string Temporada { get; set; }

        public bool EsOrganica { get; set; } = false;

        public DateTime? FechaVencimiento { get; set; }
    }

    /// <summary>
    /// DTO para filtros de búsqueda de frutas
    /// </summary>
    public class FrutaFiltroDto
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string PaisOrigen { get; set; }
        public string Temporada { get; set; }
        public bool? EsOrganica { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public int? StockMinimo { get; set; }
        public bool? SoloDisponibles { get; set; } = true;
        public bool? SoloActivos { get; set; } = true;

        // Paginación
        public int Pagina { get; set; } = 1;
        public int TamañoPagina { get; set; } = 10;

        // Ordenamiento
        public string OrdenarPor { get; set; } = "Nombre";
        public bool OrdenDescendente { get; set; } = false;
    }

    /// <summary>
    /// DTO para lista paginada de frutas
    /// </summary>
    public class FrutaListaDto
    {
        public System.Collections.Generic.List<FrutaDto> Frutas { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamañoPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamañoPagina);
        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;

        public FrutaListaDto()
        {
            Frutas = new System.Collections.Generic.List<FrutaDto>();
        }
    }

    /// <summary>
    /// DTO para estadísticas de frutas
    /// </summary>
    public class FrutaEstadisticasDto
    {
        public int TotalFrutas { get; set; }
        public int FrutasDisponibles { get; set; }
        public int FrutasAgotadas { get; set; }
        public int FrutasOrganicas { get; set; }
        public decimal ValorTotalInventario { get; set; }
        public decimal PrecioPromedio { get; set; }
        public int StockTotal { get; set; }
        public string CategoriaConMasFrutas { get; set; }
        public string PaisConMasFrutas { get; set; }
        public System.Collections.Generic.List<CategoriaEstadisticaDto> EstadisticasPorCategoria { get; set; }

        public FrutaEstadisticasDto()
        {
            EstadisticasPorCategoria = new System.Collections.Generic.List<CategoriaEstadisticaDto>();
        }
    }

    /// <summary>
    /// DTO para estadísticas por categoría
    /// </summary>
    public class CategoriaEstadisticaDto
    {
        public string Categoria { get; set; }
        public int CantidadFrutas { get; set; }
        public int StockTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal PrecioPromedio { get; set; }
    }

    /// <summary>
    /// DTO para logs del sistema
    /// </summary>
    [Serializable]
    public class LogDto
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string Username { get; set; }
        public string Accion { get; set; }
        public string Tabla { get; set; }
        public int? RegistroId { get; set; }
        public string DetalleAntes { get; set; }
        public string DetalleDepues { get; set; }
        public DateTime Fecha { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string Severidad { get; set; }
        public bool Exitoso { get; set; }
    }

    /// <summary>
    /// DTO para filtros de logs
    /// </summary>
    [Serializable]
    public class LogFiltroDto
    {
        public int? UsuarioId { get; set; }
        public string Severidad { get; set; }
        public string Accion { get; set; }
        public string Tabla { get; set; }
        public string Username { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamañoPagina { get; set; } = 25;
    }

    /// <summary>
    /// DTO para lista paginada de logs
    /// </summary>
    [Serializable]
    public class LogListaDto
    {
        public System.Collections.Generic.List<LogDto> Logs { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamañoPagina { get; set; }

        public LogListaDto()
        {
            Logs = new System.Collections.Generic.List<LogDto>();
        }
    }
}