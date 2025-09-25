using System;
using System.Web.UI;

namespace frutas.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Configuración inicial de la página
            }
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                string usernameEmail = txtUsernameEmail.Text.Trim();

                // Simulación de proceso de recuperación
                // En un sistema real, aquí se verificaría si el usuario existe
                // y se enviaría un correo con el link de recuperación

                if (string.IsNullOrEmpty(usernameEmail))
                {
                    MostrarMensaje("Por favor ingrese su usuario o email.", "danger");
                    return;
                }

                // Simular éxito
                MostrarMensaje(
                    "Si el usuario o email existe en nuestro sistema, recibirás instrucciones " +
                    "para restablecer tu contraseña en los próximos minutos.<br/><br/>" +
                    "<strong>Nota:</strong> Esta es una funcionalidad simulada en la versión demo.", 
                    "success"
                );

                // Limpiar el campo
                txtUsernameEmail.Text = string.Empty;

                // Opcionalmente redirigir al login después de unos segundos
                ScriptManager.RegisterStartupScript(this, GetType(), "AutoRedirect",
                    @"setTimeout(function() {
                        window.location.href = 'Login.aspx';
                    }, 5000);", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error del sistema: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Muestra un mensaje en la página
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        /// <param name="tipo">Tipo de mensaje (success, danger, warning, info)</param>
        private void MostrarMensaje(string mensaje, string tipo = "info")
        {
            lblMessage.Text = mensaje;
            pnlMessage.Visible = true;
            
            // Configurar clases CSS según el tipo
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

            // Scroll hacia el mensaje
            ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToMessage",
                "$('html, body').animate({ scrollTop: $('#" + pnlMessage.ClientID + "').offset().top - 100 }, 500);", true);
        }
    }
}