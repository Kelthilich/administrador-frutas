using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using frutas.Models;

namespace frutas.Repositories
{
    /// <summary>
    /// Implementación del repositorio de usuarios
    /// Proporciona acceso a datos específico para la entidad Usuario
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository() : base("Usuarios")
        {
        }

        #region Implementación de métodos abstractos

        protected override Usuario MapearDesdeReader(SqlDataReader reader)
        {
            return new Usuario
            {
                Id = (int)reader["Id"],
                Username = reader["Username"].ToString(),
                Email = reader["Email"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Salt = reader["Salt"].ToString(),
                NombreCompleto = reader["NombreCompleto"]?.ToString(),
                Rol = reader["Rol"].ToString(),
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                FechaModificacion = reader["FechaModificacion"] as DateTime?,
                UltimoLogin = reader["UltimoLogin"] as DateTime?,
                IntentosFallidos = (int)reader["IntentosFallidos"],
                CuentaBloqueada = (bool)reader["CuentaBloqueada"],
                FechaBloqueo = reader["FechaBloqueo"] as DateTime?,
                Activo = (bool)reader["Activo"],
                UsuarioCreacion = reader["UsuarioCreacion"] as int?,
                UsuarioModificacion = reader["UsuarioModificacion"] as int?
            };
        }

        protected override SqlParameter[] ObtenerParametrosInsertar(Usuario usuario)
        {
            return new[]
            {
                new SqlParameter("@Username", usuario.Username),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@PasswordHash", usuario.PasswordHash),
                new SqlParameter("@Salt", usuario.Salt),
                new SqlParameter("@NombreCompleto", usuario.NombreCompleto ?? (object)DBNull.Value),
                new SqlParameter("@Rol", usuario.Rol),
                new SqlParameter("@FechaCreacion", usuario.FechaCreacion),
                new SqlParameter("@Activo", usuario.Activo),
                new SqlParameter("@UsuarioCreacion", usuario.UsuarioCreacion ?? (object)DBNull.Value)
            };
        }

        protected override SqlParameter[] ObtenerParametrosActualizar(Usuario usuario)
        {
            return new[]
            {
                new SqlParameter("@Id", usuario.Id),
                new SqlParameter("@Username", usuario.Username),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@PasswordHash", usuario.PasswordHash),
                new SqlParameter("@Salt", usuario.Salt),
                new SqlParameter("@NombreCompleto", usuario.NombreCompleto ?? (object)DBNull.Value),
                new SqlParameter("@Rol", usuario.Rol),
                new SqlParameter("@FechaModificacion", usuario.FechaModificacion ?? (object)DBNull.Value),
                new SqlParameter("@UltimoLogin", usuario.UltimoLogin ?? (object)DBNull.Value),
                new SqlParameter("@IntentosFallidos", usuario.IntentosFallidos),
                new SqlParameter("@CuentaBloqueada", usuario.CuentaBloqueada),
                new SqlParameter("@FechaBloqueo", usuario.FechaBloqueo ?? (object)DBNull.Value),
                new SqlParameter("@Activo", usuario.Activo),
                new SqlParameter("@UsuarioModificacion", usuario.UsuarioModificacion ?? (object)DBNull.Value)
            };
        }

        protected override string ObtenerSqlInsertar()
        {
            return @"
                INSERT INTO Usuarios (Username, Email, PasswordHash, Salt, NombreCompleto, Rol, 
                                    FechaCreacion, Activo, UsuarioCreacion)
                OUTPUT INSERTED.Id
                VALUES (@Username, @Email, @PasswordHash, @Salt, @NombreCompleto, @Rol, 
                        @FechaCreacion, @Activo, @UsuarioCreacion)";
        }

        protected override string ObtenerSqlActualizar()
        {
            return @"
                UPDATE Usuarios 
                SET Username = @Username,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    Salt = @Salt,
                    NombreCompleto = @NombreCompleto,
                    Rol = @Rol,
                    FechaModificacion = @FechaModificacion,
                    UltimoLogin = @UltimoLogin,
                    IntentosFallidos = @IntentosFallidos,
                    CuentaBloqueada = @CuentaBloqueada,
                    FechaBloqueo = @FechaBloqueo,
                    Activo = @Activo,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE Id = @Id";
        }

        #endregion

        #region Métodos específicos de Usuario

        public Usuario ObtenerPorUsername(string username)
        {
            string sql = "SELECT * FROM Usuarios WHERE Username = @Username AND Activo = 1";
            var parametros = new[] { new SqlParameter("@Username", username) };
            
            var resultados = EjecutarConsulta(sql, parametros);
            foreach (var usuario in resultados)
            {
                return usuario; // Retorna el primero
            }
            
            return null;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            string sql = "SELECT * FROM Usuarios WHERE Email = @Email AND Activo = 1";
            var parametros = new[] { new SqlParameter("@Email", email) };
            
            var resultados = EjecutarConsulta(sql, parametros);
            foreach (var usuario in resultados)
            {
                return usuario; // Retorna el primero
            }
            
            return null;
        }

        public bool ExisteUsername(string username)
        {
            string sql = "SELECT COUNT(1) FROM Usuarios WHERE Username = @Username";
            var parametros = new[] { new SqlParameter("@Username", username) };
            
            var resultado = EjecutarEscalar(sql, parametros);
            return Convert.ToInt32(resultado) > 0;
        }

        public bool ExisteEmail(string email)
        {
            string sql = "SELECT COUNT(1) FROM Usuarios WHERE Email = @Email";
            var parametros = new[] { new SqlParameter("@Email", email) };
            
            var resultado = EjecutarEscalar(sql, parametros);
            return Convert.ToInt32(resultado) > 0;
        }

        public IEnumerable<Usuario> ObtenerPorRol(string rol)
        {
            string sql = "SELECT * FROM Usuarios WHERE Rol = @Rol AND Activo = 1 ORDER BY Username";
            var parametros = new[] { new SqlParameter("@Rol", rol) };
            
            return EjecutarConsulta(sql, parametros);
        }

        public void BloquearUsuario(int usuarioId, DateTime fechaBloqueo)
        {
            string sql = @"
                UPDATE Usuarios 
                SET CuentaBloqueada = 1, 
                    FechaBloqueo = @FechaBloqueo,
                    FechaModificacion = @FechaModificacion
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", usuarioId),
                new SqlParameter("@FechaBloqueo", fechaBloqueo),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void DesbloquearUsuario(int usuarioId)
        {
            string sql = @"
                UPDATE Usuarios 
                SET CuentaBloqueada = 0, 
                    FechaBloqueo = NULL,
                    IntentosFallidos = 0,
                    FechaModificacion = @FechaModificacion
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", usuarioId),
                new SqlParameter("@FechaModificacion", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void ActualizarUltimoLogin(int usuarioId)
        {
            string sql = @"
                UPDATE Usuarios 
                SET UltimoLogin = @UltimoLogin,
                    IntentosFallidos = 0
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", usuarioId),
                new SqlParameter("@UltimoLogin", DateTime.Now)
            };
            
            EjecutarComando(sql, parametros);
        }

        public void ActualizarIntentosFallidos(int usuarioId, int intentos)
        {
            string sql = @"
                UPDATE Usuarios 
                SET IntentosFallidos = @IntentosFallidos
                WHERE Id = @Id";
            
            var parametros = new[]
            {
                new SqlParameter("@Id", usuarioId),
                new SqlParameter("@IntentosFallidos", intentos)
            };
            
            EjecutarComando(sql, parametros);
        }

        public IEnumerable<Usuario> ObtenerUsuariosBloqueados()
        {
            string sql = "SELECT * FROM Usuarios WHERE CuentaBloqueada = 1 ORDER BY FechaBloqueo DESC";
            return EjecutarConsulta(sql);
        }

        public IEnumerable<Usuario> ObtenerUsuariosInactivos(int diasInactividad)
        {
            string sql = @"
                SELECT * FROM Usuarios 
                WHERE Activo = 1 
                AND (UltimoLogin IS NULL OR UltimoLogin < @FechaLimite)
                ORDER BY UltimoLogin";
            
            var fechaLimite = DateTime.Now.AddDays(-diasInactividad);
            var parametros = new[] { new SqlParameter("@FechaLimite", fechaLimite) };
            
            return EjecutarConsulta(sql, parametros);
        }

        #endregion
    }
}