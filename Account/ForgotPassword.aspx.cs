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
                // Configuraci�n inicial de la p�gina
            }
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                string usernameEmail = txtUsernameEmail.Text.Trim();

                // Simulaci�n de proceso de recuperaci�n
                // En un sistema real, aqu� se verificar�a si el usuario existe
                // y se enviar�a un correo con el link de recuperaci�n

                if (string.IsNullOrEmpty(usernameEmail))
                {
                    MostrarMensaje("Por favor ingrese su usuario o email.", "danger");
                    return;
                }

                // Simular �xito
                MostrarMensaje(
                    "Si el usuario o email existe en nuestro sistema, recibir�s instrucciones " +
                    "para restablecer tu contrase�a en los pr�ximos minutos.<br/><br/>" +
                    "<strong>Nota:</strong> Esta es una funcionalidad simulada en la versi�n demo.", 
                    "success"
                );

                // Limpiar el campo
                txtUsernameEmail.Text = string.Empty;

                // Opcionalmente redirigir al login despu�s de unos segundos
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
        /// Muestra un mensaje en la p�gina
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        /// <param name="tipo">Tipo de mensaje (success, danger, warning, info)</param>
        private void MostrarMensaje(string mensaje, string tipo = "info")
        {
            lblMessage.Text = mensaje;
            pnlMessage.Visible = true;
            
            // Configurar clases CSS seg�n el tipo
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