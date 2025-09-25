using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using frutas.Models;

namespace frutas.Repositories
{
    /// <summary>
    /// Interfaz gen�rica para repositorios
    /// Implementa el patr�n Repository con operaciones CRUD b�sicas
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        // Operaciones de consulta
        T ObtenerPorId(int id);
        IEnumerable<T> ObtenerTodos();
        IEnumerable<T> ObtenerActivos();
        IEnumerable<T> Buscar(Expression<Func<T, bool>> predicado);
        T BuscarUno(Expression<Func<T, bool>> predicado);
        bool Existe(int id);
        bool Existe(Expression<Func<T, bool>> predicado);
        int Contar();
        int Contar(Expression<Func<T, bool>> predicado);

        // Operaciones de modificaci�n
        T Agregar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
        void EliminarFisico(T entidad);
        void EliminarFisico(int id);

        // Operaciones por lotes
        void AgregarRango(IEnumerable<T> entidades);
        void ActualizarRango(IEnumerable<T> entidades);
        void EliminarRango(IEnumerable<T> entidades);

        // Operaciones con paginaci�n
        IEnumerable<T> ObtenerPaginado(int pagina, int tama�oPagina);
        IEnumerable<T> ObtenerPaginado(int pagina, int tama�oPagina, Expression<Func<T, bool>> predicado);
        IEnumerable<T> ObtenerPaginadoOrdenado<TKey>(int pagina, int tama�oPagina, 
            Expression<Func<T, TKey>> ordenarPor, bool descendente = false);
        IEnumerable<T> ObtenerPaginadoOrdenado<TKey>(int pagina, int tama�oPagina, 
            Expression<Func<T, bool>> predicado, Expression<Func<T, TKey>> ordenarPor, bool descendente = false);

        // Guardar cambios
        void GuardarCambios();
    }

    /// <summary>
    /// Interfaz espec�fica para el repositorio de usuarios
    /// Extiende IRepository con operaciones espec�ficas para usuarios
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObtenerPorUsername(string username);
        Usuario ObtenerPorEmail(string email);
        bool ExisteUsername(string username);
        bool ExisteEmail(string email);
        IEnumerable<Usuario> ObtenerPorRol(string rol);
        void BloquearUsuario(int usuarioId, DateTime fechaBloqueo);
        void DesbloquearUsuario(int usuarioId);
        void ActualizarUltimoLogin(int usuarioId);
        void ActualizarIntentosFallidos(int usuarioId, int intentos);
        IEnumerable<Usuario> ObtenerUsuariosBloqueados();
        IEnumerable<Usuario> ObtenerUsuariosInactivos(int diasInactividad);
    }

    /// <summary>
    /// Interfaz espec�fica para el repositorio de frutas
    /// Extiende IRepository con operaciones espec�ficas para frutas
    /// </summary>
    public interface IFrutaRepository : IRepository<Fruta>
    {
        IEnumerable<Fruta> BuscarPorNombre(string nombre);
        IEnumerable<Fruta> ObtenerPorCategoria(string categoria);
        IEnumerable<Fruta> ObtenerPorPais(string pais);
        IEnumerable<Fruta> ObtenerOrganicas();
        IEnumerable<Fruta> ObtenerDisponibles();
        IEnumerable<Fruta> ObtenerPorRangoPrecio(decimal precioMin, decimal precioMax);
        IEnumerable<Fruta> ObtenerConStockBajo(int stockMinimo = 10);
        IEnumerable<Fruta> ObtenerProximasAVencer(int dias = 7);
        decimal CalcularValorInventario();
        IEnumerable<string> ObtenerCategorias();
        IEnumerable<string> ObtenerPaises();
        IEnumerable<string> ObtenerTemporadas();
        void ActualizarStock(int frutaId, int nuevoStock);
        void ReducirStock(int frutaId, int cantidad);
    }

    /// <summary>
    /// Interfaz espec�fica para el repositorio de logs
    /// Log no hereda de BaseEntity por su naturaleza especial
    /// </summary>
    public interface ILogRepository
    {
        // Operaciones b�sicas
        Log Agregar(Log log);
        Log ObtenerPorId(int id);
        IEnumerable<Log> ObtenerTodos();
        
        // Consultas espec�ficas
        IEnumerable<Log> ObtenerPorUsuario(int usuarioId);
        IEnumerable<Log> ObtenerPorUsername(string username);
        IEnumerable<Log> ObtenerPorFecha(DateTime fecha);
        IEnumerable<Log> ObtenerPorRangoFecha(DateTime fechaInicio, DateTime fechaFin);
        IEnumerable<Log> ObtenerPorAccion(string accion);
        IEnumerable<Log> ObtenerPorTabla(string tabla);
        IEnumerable<Log> ObtenerPorSeveridad(string severidad);
        IEnumerable<Log> ObtenerErrores();
        IEnumerable<Log> ObtenerPorIP(string ip);
        
        // Paginaci�n
        IEnumerable<Log> ObtenerPaginado(int pagina, int tama�oPagina);
        IEnumerable<Log> ObtenerPaginadoConFiltros(int pagina, int tama�oPagina, 
            int? usuarioId = null, string accion = null, string tabla = null, 
            string severidad = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null);
        
        // Estad�sticas
        int ContarTodos();
        int ContarPorSeveridad(string severidad);
        int ContarPorFecha(DateTime fecha);
        int ContarPorUsuario(int usuarioId);
        int ContarErrores();
        
        // Mantenimiento
        void LimpiarLogsAntiguos(int diasAMantener = 90);
        void GuardarCambios();
    }

    /// <summary>
    /// Interfaz espec�fica para el repositorio de API Keys
    /// </summary>
    public interface IApiKeyRepository : IRepository<ApiKey>
    {
        ApiKey ObtenerPorKeyValue(string keyValue);
        IEnumerable<ApiKey> ObtenerPorUsuario(int usuarioId);
        IEnumerable<ApiKey> ObtenerActivas();
        IEnumerable<ApiKey> ObtenerVencidas();
        void RegistrarUso(string keyValue);
        void RevocarApiKey(int apiKeyId);
        void RevocarApiKeysPorUsuario(int usuarioId);
        bool ValidarApiKey(string keyValue, string ip = null);
        void LimpiarApiKeysVencidas();
    }
}