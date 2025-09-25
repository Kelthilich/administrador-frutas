using System;
using System.Web.UI;
using frutas.DTOs;
using frutas.Security;
using frutas.Services;
using frutas.Validators;

namespace frutas.Account
{
    public partial class Register : Page
    {
        private readonly UsuarioService _usuarioService;

        public Register()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si ya está logueado, redirigir al dashboard
                if (SessionHelper.EstaLogueado)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
            }
        }

        /// <summary>
        /// Maneja el evento de click del botón de registro
        /// </summary>
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Limpiar mensajes previos
                pnlMessage.Visible = false;

                // Validar términos y condiciones
                if (!chkTerminos.Checked)
                {
                    MostrarMensaje("Debe aceptar los términos y condiciones", "danger");
                    return;
                }

                // Crear DTO de registro
                var registroDto = new RegistroDto
                {
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text,
                    ConfirmarPassword = txtConfirmarPassword.Text,
                    NombreCompleto = string.IsNullOrEmpty(txtNombreCompleto.Text) ? null : txtNombreCompleto.Text.Trim()
                };

                // Validaciones adicionales del lado servidor
                var validationResult = ValidacionesAdicionales(registroDto);
                if (!validationResult.IsValid)
                {
                    MostrarMensaje(validationResult.GetErrorsAsString(), "danger");
                    return;
                }

                // Intentar registrar usando el servicio
                var resultado = _usuarioService.Registrar(registroDto);

                if (resultado.Exitoso)
                {
                    // Registro exitoso
                    var usuario = resultado.Data;
                    
                    // Establecer mensaje de éxito
                    Session["AlertMessage"] = "¡Cuenta creada exitosamente! Ya puede iniciar sesión.";
                    Session["AlertType"] = "success";

                    // Redirigir a login
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    // Registro fallido
                    MostrarMensaje(resultado.Mensaje, "danger");
                    
                    // Si hay errores específicos, mostrarlos
                    if (resultado.Errores != null && resultado.Errores.Count > 0)
                    {
                        string erroresDetallados = string.Join("<br/>", resultado.Errores);
                        MostrarMensaje(erroresDetallados, "danger");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error interno del sistema: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Realiza validaciones adicionales del lado servidor
        /// </summary>
        /// <param name="registro">Datos de registro</param>
        /// <returns>Resultado de la validación</returns>
        private ValidationResult ValidacionesAdicionales(RegistroDto registro)
        {
            // Usar el validador de negocio
            var resultado = UsuarioValidator.ValidarRegistro(registro);

            // Validaciones específicas adicionales si es necesario
            if (registro.Username.ToLower() == "admin" || registro.Username.ToLower() == "administrator")
            {
                resultado.AddError("No se puede usar 'admin' como nombre de usuario");
            }

            return resultado;
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

        /// <summary>
        /// Limpia los campos del formulario
        /// </summary>
        private void LimpiarFormulario()
        {
            txtUsername.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmarPassword.Text = string.Empty;
            txtNombreCompleto.Text = string.Empty;
            chkTerminos.Checked = false;
        }

        /// <summary>
        /// Override del método Render para agregar atributos a los controles
        /// </summary>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // Agregar atributos para mejor UX y seguridad
            txtUsername.Attributes.Add("autocomplete", "username");
            txtEmail.Attributes.Add("autocomplete", "email");
            txtPassword.Attributes.Add("autocomplete", "new-password");
            txtConfirmarPassword.Attributes.Add("autocomplete", "new-password");
            txtNombreCompleto.Attributes.Add("autocomplete", "name");
            
            // Agregar data attributes para validaciones JavaScript
            txtUsername.Attributes.Add("data-validate", "username");
            txtEmail.Attributes.Add("data-validate", "email");
            txtPassword.Attributes.Add("data-validate", "password");
            
            base.Render(writer);
        }
    }
}