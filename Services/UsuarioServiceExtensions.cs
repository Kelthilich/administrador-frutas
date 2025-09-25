using System;
using System.Web.UI;
using frutas.Security;
using frutas.Services;
using frutas.DTOs;

namespace frutas.Services
{
    public static class UsuarioServiceExtensions
    {
        private static readonly UsuarioService _usuarioService = new UsuarioService();

        public static ResponseDto<UsuarioDto> ActualizarPerfil(this UsuarioService service, ActualizarPerfilDto perfilDto)
        {
            try
            {
                var usuarioId = SessionHelper.UsuarioActualId;
                if (usuarioId == null)
                {
                    return ResponseDto<UsuarioDto>.Error("Debe estar autenticado");
                }

                var repository = new Repositories.UsuarioRepository();
                var usuario = repository.ObtenerPorId(usuarioId.Value);
                if (usuario == null)
                {
                    return ResponseDto<UsuarioDto>.Error("Usuario no encontrado");
                }

                // Verificar si el email ya está en uso por otro usuario
                if (!string.Equals(usuario.Email, perfilDto.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var usuarioConEmail = repository.ObtenerPorEmail(perfilDto.Email);
                    if (usuarioConEmail != null && usuarioConEmail.Id != usuario.Id)
                    {
                        return ResponseDto<UsuarioDto>.Error("El email ya está en uso por otro usuario");
                    }
                }

                // Actualizar campos
                usuario.Email = perfilDto.Email;
                usuario.NombreCompleto = perfilDto.NombreCompleto;
                usuario.FechaModificacion = DateTime.Now;
                usuario.UsuarioModificacion = usuarioId;

                repository.Actualizar(usuario);

                var usuarioDto = new UsuarioDto
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Email = usuario.Email,
                    NombreCompleto = usuario.NombreCompleto,
                    Rol = usuario.Rol,
                    FechaCreacion = usuario.FechaCreacion,
                    UltimoLogin = usuario.UltimoLogin,
                    Activo = usuario.Activo
                };

                // Actualizar la sesión con los nuevos datos
                SessionHelper.ActualizarSesion(usuarioDto);

                return ResponseDto<UsuarioDto>.Exito(usuarioDto, "Perfil actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseDto<UsuarioDto>.Error("Error interno del servidor");
            }
        }

        public static ResponseDto CambiarPassword(this UsuarioService service, CambiarPasswordDto cambiarDto)
        {
            try
            {
                var usuarioId = SessionHelper.UsuarioActualId;
                if (usuarioId == null)
                {
                    return ResponseDto.Error("Debe estar autenticado");
                }

                var repository = new Repositories.UsuarioRepository();
                var usuario = repository.ObtenerPorId(usuarioId.Value);
                if (usuario == null)
                {
                    return ResponseDto.Error("Usuario no encontrado");
                }

                // Verificar contraseña actual
                if (!Security.PasswordHelper.VerificarPassword(cambiarDto.PasswordActual, usuario.PasswordHash, usuario.Salt))
                {
                    return ResponseDto.Error("La contraseña actual es incorrecta");
                }

                // Validar nueva contraseña
                var validacionPassword = Security.PasswordHelper.ValidarFortaleza(cambiarDto.PasswordNueva);
                if (!string.IsNullOrEmpty(validacionPassword))
                {
                    return ResponseDto.Error(validacionPassword);
                }

                // Verificar que no sea la misma contraseña
                if (Security.PasswordHelper.VerificarPassword(cambiarDto.PasswordNueva, usuario.PasswordHash, usuario.Salt))
                {
                    return ResponseDto.Error("La nueva contraseña debe ser diferente a la actual");
                }

                // Crear nuevo hash
                var (nuevoHash, nuevoSalt) = Security.PasswordHelper.CrearHashCompleto(cambiarDto.PasswordNueva);
                
                usuario.PasswordHash = nuevoHash;
                usuario.Salt = nuevoSalt;
                usuario.FechaModificacion = DateTime.Now;
                usuario.UsuarioModificacion = usuarioId;
                
                // Resetear intentos fallidos
                usuario.IntentosFallidos = 0;
                usuario.CuentaBloqueada = false;
                usuario.FechaBloqueo = null;

                repository.Actualizar(usuario);

                return ResponseDto.Exito("Contraseña actualizada exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseDto.Error("Error interno del servidor");
            }
        }
    }
}