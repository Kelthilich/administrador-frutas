using System;
using System.Web.UI;
using frutas.Security;
using frutas.Services;
using frutas.DTOs;

namespace frutas.Account
{
    public partial class ChangePassword : Page
    {
        private readonly UsuarioService _usuarioService;

        public ChangePassword()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Requerir autenticaci�n
            SiteMaster.RequireLogin();

            if (!IsPostBack)
            {
                // Configuraci�n inicial si es necesaria
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                var cambiarPasswordDto = new CambiarPasswordDto
                {
                    PasswordActual = txtPasswordActual.Text,
                    PasswordNueva = txtPasswordNueva.Text,
                    ConfirmarPassword = txtPasswordConfirmar.Text
                };

                var resultado = _usuarioService.CambiarPassword(cambiarPasswordDto);

                if (resultado.Exitoso)
                {
                    MostrarMensaje("Contrase�a cambiada exitosamente", "success");
                    
                    // Limpiar campos
                    txtPasswordActual.Text = string.Empty;
                    txtPasswordNueva.Text = string.Empty;
                    txtPasswordConfirmar.Text = string.Empty;
                    
                    // Redirigir despu�s de un delay
                    ScriptManager.RegisterStartupScript(this, GetType(), "RedirectDelay",
                        @"setTimeout(function() {
                            window.location.href = 'Profile.aspx';
                        }, 3000);", true);
                }
                else
                {
                    MostrarMensaje(resultado.Mensaje, "danger");
                    
                    // Limpiar contrase�a actual por seguridad
                    txtPasswordActual.Text = string.Empty;
                    txtPasswordActual.Focus();
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