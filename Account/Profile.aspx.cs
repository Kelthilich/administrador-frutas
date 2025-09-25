using System;
using System.Web.UI;
using frutas.Security;
using frutas.Services;
using frutas.DTOs;

namespace frutas.Account
{
    public partial class Profile : Page
    {
        private readonly UsuarioService _usuarioService;

        public Profile()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Requerir autenticación
            SiteMaster.RequireLogin();

            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                var usuario = SessionHelper.UsuarioActual;
                if (usuario != null)
                {
                    txtUsername.Text = usuario.Username;
                    txtEmail.Text = usuario.Email;
                    txtNombreCompleto.Text = usuario.NombreCompleto ?? string.Empty;
                    lblRol.Text = usuario.Rol;
                    lblFechaCreacion.Text = usuario.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    lblUltimoLogin.Text = usuario.UltimoLogin?.ToString("dd/MM/yyyy HH:mm") ?? "No disponible";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando datos del usuario: {ex.Message}", "danger");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                var perfilDto = new ActualizarPerfilDto
                {
                    Email = txtEmail.Text.Trim(),
                    NombreCompleto = string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ? null : txtNombreCompleto.Text.Trim()
                };

                var resultado = _usuarioService.ActualizarPerfil(perfilDto);

                if (resultado.Exitoso)
                {
                    // Actualizar la sesión con los nuevos datos
                    SessionHelper.ActualizarSesion(resultado.Data);
                    
                    MostrarMensaje("Perfil actualizado exitosamente", "success");
                }
                else
                {
                    MostrarMensaje($"Error actualizando perfil: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error interno: {ex.Message}", "danger");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo = "info")
        {
            lblMessage.Text = mensaje;
            pnlMessage.Visible = true;
            
            string cssClasses = "alert alert-dismissible fade show";
            
            switch (tipo.ToLower())
            {
                case "success":
                    cssClasses += " alert-success";
                    lblMessage.Text = $"<i class='fas fa-check-circle'></i> {mensaje}";
                    break;
                case "danger":
                case "error":
                    cssClasses += " alert-danger";
                    lblMessage.Text = $"<i class='fas fa-exclamation-triangle'></i> {mensaje}";
                    break;
                case "warning":
                    cssClasses += " alert-warning";
                    lblMessage.Text = $"<i class='fas fa-exclamation-triangle'></i> {mensaje}";
                    break;
                case "info":
                default:
                    cssClasses += " alert-info";
                    lblMessage.Text = $"<i class='fas fa-info-circle'></i> {mensaje}";
                    break;
            }
            
            pnlMessage.CssClass = cssClasses;
        }
    }
}