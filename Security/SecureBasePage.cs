using System;
using System.Web.UI;
using frutas.Security;

namespace frutas
{
    /// <summary>
    /// Clase base para p�ginas que requieren autenticaci�n
    /// </summary>
    public class SecureBasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verificar si el usuario est� autenticado
            if (!SessionHelper.EstaLogueado)
            {
                // Guardar la URL a la que intent� acceder
                string returnUrl = Request.RawUrl;
                
                // Redirigir al login con ReturnUrl
                Response.Redirect($"~/Account/Login.aspx?ReturnUrl={Server.UrlEncode(returnUrl)}");
            }
        }
    }

    /// <summary>
    /// Clase base para p�ginas que requieren rol de Administrador
    /// </summary>
    public class AdminBasePage : SecureBasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verificar si el usuario es administrador
            if (!SessionHelper.EsAdministrador)
            {
                // Redirigir a p�gina de acceso denegado
                Response.Redirect("~/Error.aspx");
            }
        }
    }
}