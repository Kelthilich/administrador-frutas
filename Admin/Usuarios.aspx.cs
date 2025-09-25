using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using frutas.DTOs;
using frutas.Security;
using frutas.Services;

namespace frutas.Admin
{
    public partial class Usuarios : Page
    {
        private readonly UsuarioService _usuarioService;

        public Usuarios()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación y permisos de administrador
            SiteMaster.RequireLogin();
            
            if (!SessionHelper.EsAdministrador)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarEstadisticas();
            }
        }

        /// <summary>
        /// Carga la lista de usuarios
        /// </summary>
        private void CargarUsuarios()
        {
            try
            {
                var resultado = _usuarioService.ObtenerTodos();
                
                if (resultado.Exitoso)
                {
                    gvUsuarios.DataSource = resultado.Data;
                    gvUsuarios.DataBind();
                }
                else
                {
                    MostrarAlerta($"Error cargando usuarios: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error interno: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Carga las estadísticas de usuarios
        /// </summary>
        private void CargarEstadisticas()
        {
            try
            {
                var resultado = _usuarioService.ObtenerTodos();
                
                if (resultado.Exitoso)
                {
                    var usuarios = resultado.Data.ToList();
                    
                    lblTotalUsuarios.Text = usuarios.Count.ToString();
                    lblUsuariosActivos.Text = usuarios.Count(u => u.Activo).ToString();
                    lblAdministradores.Text = usuarios.Count(u => u.Rol == "Administrador").ToString();
                    
                    // Para usuarios bloqueados necesitaríamos una consulta adicional
                    // Por ahora lo dejamos en 0
                    lblUsuariosBloqueados.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error cargando estadísticas: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Maneja los comandos del GridView
        /// </summary>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int usuarioId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "CambiarEstado":
                    CambiarEstadoUsuario(usuarioId);
                    break;

                case "ResetPassword":
                    ResetearPassword(usuarioId);
                    break;
            }
        }

        /// <summary>
        /// Configura las filas del GridView
        /// </summary>
        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var usuario = (UsuarioDto)e.Row.DataItem;
                
                // No permitir que el usuario actual se desactive a sí mismo
                if (usuario.Id == SessionHelper.UsuarioActualId)
                {
                    var btnCambiarEstado = e.Row.FindControl("btnCambiarEstado") as LinkButton;
                    if (btnCambiarEstado != null)
                    {
                        btnCambiarEstado.Enabled = false;
                        btnCambiarEstado.ToolTip = "No puede cambiar su propio estado";
                        btnCambiarEstado.CssClass += " disabled";
                    }
                }
            }
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un usuario
        /// </summary>
        private void CambiarEstadoUsuario(int usuarioId)
        {
            try
            {
                // No permitir que se desactive a sí mismo
                if (usuarioId == SessionHelper.UsuarioActualId)
                {
                    MostrarAlerta("No puede cambiar su propio estado", "warning");
                    return;
                }

                var usuarioResult = _usuarioService.ObtenerPorId(usuarioId);
                if (!usuarioResult.Exitoso)
                {
                    MostrarAlerta("Usuario no encontrado", "danger");
                    return;
                }

                var usuario = usuarioResult.Data;
                bool nuevoEstado = !usuario.Activo;

                var resultado = _usuarioService.ActivarDesactivar(usuarioId, nuevoEstado);

                if (resultado.Exitoso)
                {
                    string accion = nuevoEstado ? "activado" : "desactivado";
                    MostrarAlerta($"Usuario {accion} exitosamente", "success");
                    CargarUsuarios();
                    CargarEstadisticas();
                }
                else
                {
                    MostrarAlerta($"Error cambiando estado: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error interno: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Resetea la contraseña de un usuario
        /// </summary>
        private void ResetearPassword(int usuarioId)
        {
            try
            {
                var resultado = _usuarioService.GenerarPasswordTemporal(usuarioId);

                if (resultado.Exitoso)
                {
                    string nuevaPassword = resultado.Data;
                    MostrarAlerta("Contraseña temporal generada exitosamente", "success");
                    
                    // Mostrar la contraseña en el modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "MostrarPassword",
                        $"mostrarPassword('{nuevaPassword}');", true);
                }
                else
                {
                    MostrarAlerta($"Error generando contraseña: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error interno: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Obtiene la clase CSS para el rol
        /// </summary>
        protected string GetRolCssClass(string rol)
        {
            switch (rol?.ToLower())
            {
                case "administrador":
                    return "badge badge-rol-admin";
                case "usuario":
                default:
                    return "badge badge-rol-user";
            }
        }

        /// <summary>
        /// Muestra una alerta al usuario
        /// </summary>
        private void MostrarAlerta(string mensaje, string tipo)
        {
            var master = this.Master as SiteMaster;
            master?.MostrarAlerta(mensaje, tipo);
        }
    }
}