using System;
using System.Web.UI;
using frutas.Security;

namespace frutas
{
    /// <summary>
    /// Clase base para páginas que requieren autenticación
    /// </summary>
    public class SecureBasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verificar si el usuario está autenticado
            if (!SessionHelper.EstaLogueado)
            {
                // Guardar la URL a la que intentó acceder
                string returnUrl = Request.RawUrl;
                
                // Redirigir al login con ReturnUrl
                Response.Redirect($"~/Account/Login.aspx?ReturnUrl={Server.UrlEncode(returnUrl)}");
            }
        }
    }

    /// <summary>
    /// Clase base para páginas que requieren rol de Administrador
    /// </summary>
    public class AdminBasePage : SecureBasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verificar si el usuario es administrador
            if (!SessionHelper.EsAdministrador)
            {
                // Redirigir a página de acceso denegado
                Response.Redirect("~/Error.aspx");
            }
        }
    }
}