using System;
using System.Collections.Generic;
using System.Linq;
using frutas.DTOs;
using frutas.Models;
using frutas.Repositories;
using frutas.Security;

namespace frutas.Services
{
    /// <summary>
    /// Interfaz para el servicio de frutas
    /// Define las operaciones de negocio para la gestión de frutas
    /// </summary>
    public interface IFrutaService
    {
        // Operaciones CRUD
        ResponseDto<FrutaDto> ObtenerPorId(int id);
        ResponseDto<FrutaListaDto> ObtenerTodas(FrutaFiltroDto filtro = null);
        ResponseDto<FrutaDto> Crear(FrutaFormDto frutaDto);
        ResponseDto<FrutaDto> Actualizar(int id, FrutaFormDto frutaDto);
        ResponseDto Eliminar(int id);
        ResponseDto<bool> Existe(int id);

        // Búsquedas específicas
        ResponseDto<IEnumerable<FrutaDto>> BuscarPorNombre(string nombre);
        ResponseDto<IEnumerable<FrutaDto>> ObtenerPorCategoria(string categoria);
        ResponseDto<IEnumerable<FrutaDto>> ObtenerDisponibles();
        ResponseDto<IEnumerable<FrutaDto>> ObtenerOrganicas();
        ResponseDto<IEnumerable<FrutaDto>> ObtenerConStockBajo(int stockMinimo = 10);
        ResponseDto<IEnumerable<FrutaDto>> ObtenerProximasAVencer(int dias = 7);

        // Gestión de stock
        ResponseDto ActualizarStock(int id, int nuevoStock);
        ResponseDto ReducirStock(int id, int cantidad);

        // Catálogos
        ResponseDto<IEnumerable<string>> ObtenerCategorias();
        ResponseDto<IEnumerable<string>> ObtenerPaises();
        ResponseDto<IEnumerable<string>> ObtenerTemporadas();

        // Estadísticas
        ResponseDto<FrutaEstadisticasDto> ObtenerEstadisticas();
        ResponseDto<decimal> CalcularValorInventario();
    }

    /// <summary>
    /// Implementación del servicio de frutas
    /// Contiene toda la lógica de negocio relacionada con frutas
    /// </summary>
    public class FrutaService : IFrutaService
    {
        private readonly IFrutaRepository _frutaRepository;
        private readonly ILogRepository _logRepository;

        public FrutaService(IFrutaRepository frutaRepository, ILogRepository logRepository)
        {
            _frutaRepository = frutaRepository ?? throw new ArgumentNullException(nameof(frutaRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public FrutaService()
        {
            _frutaRepository = new FrutaRepository();
            _logRepository = new LogRepository();
        }

        #region Operaciones CRUD

        public ResponseDto<FrutaDto> ObtenerPorId(int id)
        {
            try
            {
                var fruta = _frutaRepository.ObtenerPorId(id);
                if (fruta == null)
                {
                    return ResponseDto<FrutaDto>.Error("Fruta no encontrada");
                }

                var frutaDto = MapearAFrutaDto(fruta);
                return ResponseDto<FrutaDto>.Exito(frutaDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_FRUTA_ERROR", "Frutas", $"Error obteniendo fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto<FrutaDto>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<FrutaListaDto> ObtenerTodas(FrutaFiltroDto filtro = null)
        {
            try
            {
                if (filtro == null)
                {
                    filtro = new FrutaFiltroDto();
                }

                // Validar parámetros de paginación
                if (filtro.Pagina < 1) filtro.Pagina = 1;
                if (filtro.TamañoPagina < 1 || filtro.TamañoPagina > 100) filtro.TamañoPagina = 10;

                // Obtener frutas con filtros
                var frutasRepository = _frutaRepository as FrutaRepository;
                var frutas = frutasRepository.BuscarPaginadoConFiltros(
                    filtro.Pagina, 
                    filtro.TamañoPagina,
                    filtro.Nombre,
                    filtro.Categoria,
                    filtro.PaisOrigen,
                    filtro.Temporada,
                    filtro.EsOrganica,
                    filtro.PrecioMinimo,
                    filtro.PrecioMaximo,
                    filtro.StockMinimo,
                    filtro.SoloDisponibles ?? true,
                    filtro.SoloActivos ?? true,
                    filtro.OrdenarPor,
                    filtro.OrdenDescendente
                );

                var frutasDto = frutas.Select(MapearAFrutaDto).ToList();

                // Contar total para paginación
                var totalFrutas = frutasRepository.BuscarConFiltros(
                    filtro.Nombre,
                    filtro.Categoria,
                    filtro.PaisOrigen,
                    filtro.Temporada,
                    filtro.EsOrganica,
                    filtro.PrecioMinimo,
                    filtro.PrecioMaximo,
                    filtro.StockMinimo,
                    filtro.SoloDisponibles ?? true,
                    filtro.SoloActivos ?? true
                ).Count();

                var resultado = new FrutaListaDto
                {
                    Frutas = frutasDto,
                    TotalRegistros = totalFrutas,
                    PaginaActual = filtro.Pagina,
                    TamañoPagina = filtro.TamañoPagina
                };

                return ResponseDto<FrutaListaDto>.Exito(resultado);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_FRUTAS_ERROR", "Frutas", $"Error obteniendo frutas: {ex.Message}", null, "ERROR");
                return ResponseDto<FrutaListaDto>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<FrutaDto> Crear(FrutaFormDto frutaDto)
        {
            try
            {
                // Validar datos
                var validacionResult = ValidarFrutaForm(frutaDto);
                if (!validacionResult.Exitoso)
                {
                    return ResponseDto<FrutaDto>.Error(validacionResult.Mensaje, validacionResult.Errores);
                }

                // Verificar permisos
                if (!SessionHelper.EstaLogueado)
                {
                    return ResponseDto<FrutaDto>.Error("Debe estar autenticado para crear frutas");
                }

                // Crear entidad
                var fruta = new Fruta
                {
                    Nombre = frutaDto.Nombre?.Trim(),
                    Descripcion = frutaDto.Descripcion?.Trim(),
                    Precio = frutaDto.Precio,
                    Stock = frutaDto.Stock,
                    Categoria = frutaDto.Categoria?.Trim(),
                    PaisOrigen = frutaDto.PaisOrigen?.Trim(),
                    Temporada = frutaDto.Temporada?.Trim(),
                    EsOrganica = frutaDto.EsOrganica,
                    FechaVencimiento = frutaDto.FechaVencimiento,
                    FechaCreacion = DateTime.Now,
                    Activo = true,
                    UsuarioCreacion = SessionHelper.UsuarioActualId
                };

                var frutaCreada = _frutaRepository.Agregar(fruta);
                var resultado = MapearAFrutaDto(frutaCreada);

                RegistrarLog("FRUTA_CREATED", "Frutas", $"Fruta creada: {fruta.Nombre}", frutaCreada.Id);

                return ResponseDto<FrutaDto>.Exito(resultado, "Fruta creada exitosamente");
            }
            catch (Exception ex)
            {
                RegistrarLog("CREATE_FRUTA_ERROR", "Frutas", $"Error creando fruta: {ex.Message}", null, "ERROR");
                return ResponseDto<FrutaDto>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<FrutaDto> Actualizar(int id, FrutaFormDto frutaDto)
        {
            try
            {
                // Obtener fruta existente
                var fruta = _frutaRepository.ObtenerPorId(id);
                if (fruta == null)
                {
                    return ResponseDto<FrutaDto>.Error("Fruta no encontrada");
                }

                // Verificar permisos
                if (!SessionHelper.EstaLogueado)
                {
                    return ResponseDto<FrutaDto>.Error("Debe estar autenticado para actualizar frutas");
                }

                if (!SessionHelper.PuedeEditar(fruta.UsuarioCreacion))
                {
                    return ResponseDto<FrutaDto>.Error("No tiene permisos para editar esta fruta");
                }

                // Validar datos
                var validacionResult = ValidarFrutaForm(frutaDto);
                if (!validacionResult.Exitoso)
                {
                    return ResponseDto<FrutaDto>.Error(validacionResult.Mensaje, validacionResult.Errores);
                }

                // Guardar estado anterior para auditoría
                string estadoAnterior = SerializarFruta(fruta);

                // Actualizar campos
                fruta.Nombre = frutaDto.Nombre?.Trim();
                fruta.Descripcion = frutaDto.Descripcion?.Trim();
                fruta.Precio = frutaDto.Precio;
                fruta.Stock = frutaDto.Stock;
                fruta.Categoria = frutaDto.Categoria?.Trim();
                fruta.PaisOrigen = frutaDto.PaisOrigen?.Trim();
                fruta.Temporada = frutaDto.Temporada?.Trim();
                fruta.EsOrganica = frutaDto.EsOrganica;
                fruta.FechaVencimiento = frutaDto.FechaVencimiento;
                fruta.FechaModificacion = DateTime.Now;
                fruta.UsuarioModificacion = SessionHelper.UsuarioActualId;

                _frutaRepository.Actualizar(fruta);

                string estadoPosterior = SerializarFruta(fruta);
                RegistrarLog("FRUTA_UPDATED", "Frutas", $"Fruta actualizada: {fruta.Nombre}", 
                           fruta.Id, "INFO", estadoAnterior, estadoPosterior);

                var resultado = MapearAFrutaDto(fruta);
                return ResponseDto<FrutaDto>.Exito(resultado, "Fruta actualizada exitosamente");
            }
            catch (Exception ex)
            {
                RegistrarLog("UPDATE_FRUTA_ERROR", "Frutas", $"Error actualizando fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto<FrutaDto>.Error("Error interno del servidor");
            }
        }

        public ResponseDto Eliminar(int id)
        {
            try
            {
                var fruta = _frutaRepository.ObtenerPorId(id);
                if (fruta == null)
                {
                    return ResponseDto.Error("Fruta no encontrada");
                }

                // Verificar permisos
                if (!SessionHelper.EstaLogueado)
                {
                    return ResponseDto.Error("Debe estar autenticado para eliminar frutas");
                }

                if (!SessionHelper.PuedeEliminar(fruta.UsuarioCreacion))
                {
                    return ResponseDto.Error("No tiene permisos para eliminar esta fruta");
                }

                // Soft delete
                _frutaRepository.Eliminar(id);

                RegistrarLog("FRUTA_DELETED", "Frutas", $"Fruta eliminada: {fruta.Nombre}", id);

                return ResponseDto.Exito("Fruta eliminada exitosamente");
            }
            catch (Exception ex)
            {
                RegistrarLog("DELETE_FRUTA_ERROR", "Frutas", $"Error eliminando fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto.Error("Error interno del servidor");
            }
        }

        public ResponseDto<bool> Existe(int id)
        {
            try
            {
                bool existe = _frutaRepository.Existe(id);
                return ResponseDto<bool>.Exito(existe);
            }
            catch (Exception ex)
            {
                RegistrarLog("EXISTS_FRUTA_ERROR", "Frutas", $"Error verificando existencia de fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto<bool>.Error("Error interno del servidor");
            }
        }

        #endregion

        #region Búsquedas específicas

        public ResponseDto<IEnumerable<FrutaDto>> BuscarPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return ResponseDto<IEnumerable<FrutaDto>>.Error("Debe proporcionar un nombre para buscar");
                }

                var frutas = _frutaRepository.BuscarPorNombre(nombre);
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("SEARCH_FRUTA_ERROR", "Frutas", $"Error buscando frutas por nombre '{nombre}': {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<FrutaDto>> ObtenerPorCategoria(string categoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria))
                {
                    return ResponseDto<IEnumerable<FrutaDto>>.Error("Debe proporcionar una categoría");
                }

                var frutas = _frutaRepository.ObtenerPorCategoria(categoria);
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_FRUTAS_BY_CATEGORY_ERROR", "Frutas", $"Error obteniendo frutas por categoría '{categoria}': {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<FrutaDto>> ObtenerDisponibles()
        {
            try
            {
                var frutas = _frutaRepository.ObtenerDisponibles();
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_AVAILABLE_FRUTAS_ERROR", "Frutas", $"Error obteniendo frutas disponibles: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<FrutaDto>> ObtenerOrganicas()
        {
            try
            {
                var frutas = _frutaRepository.ObtenerOrganicas();
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_ORGANIC_FRUTAS_ERROR", "Frutas", $"Error obteniendo frutas orgánicas: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<FrutaDto>> ObtenerConStockBajo(int stockMinimo = 10)
        {
            try
            {
                var frutas = _frutaRepository.ObtenerConStockBajo(stockMinimo);
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_LOW_STOCK_FRUTAS_ERROR", "Frutas", $"Error obteniendo frutas con stock bajo: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<FrutaDto>> ObtenerProximasAVencer(int dias = 7)
        {
            try
            {
                var frutas = _frutaRepository.ObtenerProximasAVencer(dias);
                var frutasDto = frutas.Select(MapearAFrutaDto);

                return ResponseDto<IEnumerable<FrutaDto>>.Exito(frutasDto);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_EXPIRING_FRUTAS_ERROR", "Frutas", $"Error obteniendo frutas próximas a vencer: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<FrutaDto>>.Error("Error interno del servidor");
            }
        }

        #endregion

        #region Gestión de stock

        public ResponseDto ActualizarStock(int id, int nuevoStock)
        {
            try
            {
                if (nuevoStock < 0)
                {
                    return ResponseDto.Error("El stock no puede ser negativo");
                }

                var fruta = _frutaRepository.ObtenerPorId(id);
                if (fruta == null)
                {
                    return ResponseDto.Error("Fruta no encontrada");
                }

                int stockAnterior = fruta.Stock;
                _frutaRepository.ActualizarStock(id, nuevoStock);

                RegistrarLog("STOCK_UPDATED", "Frutas", 
                    $"Stock actualizado para {fruta.Nombre}: {stockAnterior} -> {nuevoStock}", id);

                return ResponseDto.Exito("Stock actualizado exitosamente");
            }
            catch (Exception ex)
            {
                RegistrarLog("UPDATE_STOCK_ERROR", "Frutas", $"Error actualizando stock de fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto.Error("Error interno del servidor");
            }
        }

        public ResponseDto ReducirStock(int id, int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    return ResponseDto.Error("La cantidad debe ser mayor a cero");
                }

                var fruta = _frutaRepository.ObtenerPorId(id);
                if (fruta == null)
                {
                    return ResponseDto.Error("Fruta no encontrada");
                }

                if (fruta.Stock < cantidad)
                {
                    return ResponseDto.Error($"Stock insuficiente. Stock actual: {fruta.Stock}");
                }

                int stockAnterior = fruta.Stock;
                _frutaRepository.ReducirStock(id, cantidad);

                RegistrarLog("STOCK_REDUCED", "Frutas", 
                    $"Stock reducido para {fruta.Nombre}: {stockAnterior} -> {stockAnterior - cantidad} (-{cantidad})", id);

                return ResponseDto.Exito("Stock reducido exitosamente");
            }
            catch (Exception ex)
            {
                RegistrarLog("REDUCE_STOCK_ERROR", "Frutas", $"Error reduciendo stock de fruta {id}: {ex.Message}", id, "ERROR");
                return ResponseDto.Error("Error interno del servidor");
            }
        }

        #endregion

        #region Catálogos

        public ResponseDto<IEnumerable<string>> ObtenerCategorias()
        {
            try
            {
                var categorias = _frutaRepository.ObtenerCategorias();
                return ResponseDto<IEnumerable<string>>.Exito(categorias);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_CATEGORIES_ERROR", "Frutas", $"Error obteniendo categorías: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<string>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<string>> ObtenerPaises()
        {
            try
            {
                var paises = _frutaRepository.ObtenerPaises();
                return ResponseDto<IEnumerable<string>>.Exito(paises);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_COUNTRIES_ERROR", "Frutas", $"Error obteniendo países: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<string>>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<IEnumerable<string>> ObtenerTemporadas()
        {
            try
            {
                var temporadas = _frutaRepository.ObtenerTemporadas();
                return ResponseDto<IEnumerable<string>>.Exito(temporadas);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_SEASONS_ERROR", "Frutas", $"Error obteniendo temporadas: {ex.Message}", null, "ERROR");
                return ResponseDto<IEnumerable<string>>.Error("Error interno del servidor");
            }
        }

        #endregion

        #region Estadísticas

        public ResponseDto<FrutaEstadisticasDto> ObtenerEstadisticas()
        {
            try
            {
                var todasLasFrutas = _frutaRepository.ObtenerActivos().ToList();
                var frutasDisponibles = _frutaRepository.ObtenerDisponibles().ToList();
                var frutasOrganicas = _frutaRepository.ObtenerOrganicas().ToList();

                var estadisticas = new FrutaEstadisticasDto
                {
                    TotalFrutas = todasLasFrutas.Count,
                    FrutasDisponibles = frutasDisponibles.Count,
                    FrutasAgotadas = todasLasFrutas.Count(f => f.Stock == 0),
                    FrutasOrganicas = frutasOrganicas.Count,
                    ValorTotalInventario = _frutaRepository.CalcularValorInventario(),
                    PrecioPromedio = todasLasFrutas.Any() ? todasLasFrutas.Average(f => f.Precio) : 0,
                    StockTotal = todasLasFrutas.Sum(f => f.Stock)
                };

                // Estadísticas por categoría
                var categorias = todasLasFrutas.GroupBy(f => f.Categoria ?? "Sin Categoría");
                foreach (var grupo in categorias)
                {
                    estadisticas.EstadisticasPorCategoria.Add(new CategoriaEstadisticaDto
                    {
                        Categoria = grupo.Key,
                        CantidadFrutas = grupo.Count(),
                        StockTotal = grupo.Sum(f => f.Stock),
                        ValorTotal = grupo.Sum(f => f.Precio * f.Stock),
                        PrecioPromedio = grupo.Average(f => f.Precio)
                    });
                }

                // Categoría y país con más frutas
                if (estadisticas.EstadisticasPorCategoria.Any())
                {
                    estadisticas.CategoriaConMasFrutas = estadisticas.EstadisticasPorCategoria
                        .OrderByDescending(c => c.CantidadFrutas)
                        .First().Categoria;
                }

                var paisesFrutas = todasLasFrutas.GroupBy(f => f.PaisOrigen ?? "Sin País")
                    .OrderByDescending(g => g.Count());
                estadisticas.PaisConMasFrutas = paisesFrutas.FirstOrDefault()?.Key;

                return ResponseDto<FrutaEstadisticasDto>.Exito(estadisticas);
            }
            catch (Exception ex)
            {
                RegistrarLog("GET_STATISTICS_ERROR", "Frutas", $"Error obteniendo estadísticas: {ex.Message}", null, "ERROR");
                return ResponseDto<FrutaEstadisticasDto>.Error("Error interno del servidor");
            }
        }

        public ResponseDto<decimal> CalcularValorInventario()
        {
            try
            {
                decimal valor = _frutaRepository.CalcularValorInventario();
                return ResponseDto<decimal>.Exito(valor);
            }
            catch (Exception ex)
            {
                RegistrarLog("CALCULATE_INVENTORY_ERROR", "Frutas", $"Error calculando valor de inventario: {ex.Message}", null, "ERROR");
                return ResponseDto<decimal>.Error("Error interno del servidor");
            }
        }

        #endregion

        #region Métodos privados

        private FrutaDto MapearAFrutaDto(Fruta fruta)
        {
            return new FrutaDto
            {
                Id = fruta.Id,
                Nombre = fruta.Nombre,
                Descripcion = fruta.Descripcion,
                Precio = fruta.Precio,
                Stock = fruta.Stock,
                Categoria = fruta.Categoria,
                PaisOrigen = fruta.PaisOrigen,
                Temporada = fruta.Temporada,
                EsOrganica = fruta.EsOrganica,
                FechaVencimiento = fruta.FechaVencimiento,
                FechaCreacion = fruta.FechaCreacion,
                FechaModificacion = fruta.FechaModificacion,
                Activo = fruta.Activo,
                UsuarioCreador = fruta.UsuarioCreador?.Username,
                UsuarioModificador = fruta.UsuarioModificador?.Username
            };
        }

        private ResponseDto ValidarFrutaForm(FrutaFormDto frutaDto)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(frutaDto.Nombre))
                errores.Add("El nombre de la fruta es requerido");

            if (frutaDto.Precio <= 0)
                errores.Add("El precio debe ser mayor a cero");

            if (frutaDto.Stock < 0)
                errores.Add("El stock no puede ser negativo");

            if (frutaDto.FechaVencimiento.HasValue && frutaDto.FechaVencimiento.Value <= DateTime.Now.Date)
                errores.Add("La fecha de vencimiento debe ser futura");

            if (errores.Any())
            {
                return ResponseDto.Error("Datos de fruta inválidos", errores);
            }

            return ResponseDto.Exito();
        }

        private string SerializarFruta(Fruta fruta)
        {
            return $"Nombre: {fruta.Nombre}, Precio: {fruta.Precio}, Stock: {fruta.Stock}, " +
                   $"Categoria: {fruta.Categoria}, País: {fruta.PaisOrigen}, " +
                   $"Temporada: {fruta.Temporada}, Orgánica: {fruta.EsOrganica}";
        }

        private void RegistrarLog(string accion, string tabla, string detalle, int? registroId = null, 
            string severidad = "INFO", string detalleAntes = null, string detalleDepues = null)
        {
            try
            {
                var log = new Log
                {
                    UsuarioId = SessionHelper.UsuarioActualId,
                    Username = SessionHelper.UsuarioActualUsername,
                    Accion = accion,
                    Tabla = tabla,
                    RegistroId = registroId,
                    DetalleAntes = detalleAntes,
                    DetalleDepues = detalleDepues ?? detalle,
                    Fecha = DateTime.Now,
                    IP = SessionHelper.ObtenerIPCliente(),
                    UserAgent = SessionHelper.ObtenerUserAgent(),
                    Severidad = severidad,
                    Exitoso = severidad != "ERROR"
                };

                _logRepository.Agregar(log);
            }
            catch
            {
                // No hacer nada si falla el logging para evitar errores en cascada
            }
        }

        #endregion
    }
}