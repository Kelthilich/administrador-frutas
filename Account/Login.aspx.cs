using System;
using System.Web.UI;
using frutas.DTOs;
using frutas.Security;
using frutas.Services;

namespace frutas.Account
{
    public partial class Login : Page
    {
        private readonly UsuarioService _usuarioService;

        public Login()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si ya est� logueado, redirigir al dashboard
                if (SessionHelper.EstaLogueado)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                // Verificar si viene de logout u otra acci�n
                VerificarMensajes();
            }
        }

        /// <summary>
        /// Maneja el evento de click del bot�n de login
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Limpiar mensajes previos
                pnlError.Visible = false;

                // Crear DTO de login
                var loginDto = new LoginDto
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    RecordarUsuario = chkRecordar.Checked
                };

                // Validaciones b�sicas del lado cliente
                if (string.IsNullOrEmpty(loginDto.Username))
                {
                    MostrarError("El nombre de usuario es requerido");
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(loginDto.Password))
                {
                    MostrarError("La contrase�a es requerida");
                    txtPassword.Focus();
                    return;
                }

                // Intentar login usando el servicio
                var resultado = _usuarioService.Login(loginDto);

                if (resultado.Exitoso)
                {
                    // Login exitoso
                    var authResponse = resultado.Data;
                    
                    // Establecer sesi�n del usuario
                    SessionHelper.EstablecerSesion(authResponse.Usuario);

                    // Mensaje de �xito para la pr�xima p�gina
                    Session["AlertMessage"] = $"�Bienvenido {authResponse.Usuario.NombreCompleto ?? authResponse.Usuario.Username}!";
                    Session["AlertType"] = "success";

                    // Redirigir seg�n el par�metro ReturnUrl o al dashboard
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl) && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
                    {
                        Response.Redirect(returnUrl);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    // Login fallido
                    MostrarError(resultado.Mensaje);
                    txtPassword.Text = string.Empty; // Limpiar contrase�a
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error interno del sistema: {ex.Message}");
            }
        }

        /// <summary>
        /// Muestra un mensaje de error en la p�gina
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            pnlError.Visible = true;
        }

        /// <summary>
        /// Verifica si hay mensajes en la sesi�n para mostrar
        /// </summary>
        private void VerificarMensajes()
        {
            var mensaje = Session["AlertMessage"] as string;
            var tipo = Session["AlertType"] as string;

            if (!string.IsNullOrEmpty(mensaje))
            {
                // Mostrar el mensaje usando la master page
                var master = this.Master as SiteMaster;
                if (master != null)
                {
                    master.MostrarAlerta(mensaje, tipo ?? "info");
                }

                // Limpiar mensaje de la sesi�n
                Session.Remove("AlertMessage");
                Session.Remove("AlertType");
            }
        }

        /// <summary>
        /// Override del m�todo Render para agregar atributos a los controles
        /// </summary>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // Agregar atributos para mejor UX
            txtUsername.Attributes.Add("autocomplete", "username");
            txtPassword.Attributes.Add("autocomplete", "current-password");
            
            base.Render(writer);
        }
    }
}