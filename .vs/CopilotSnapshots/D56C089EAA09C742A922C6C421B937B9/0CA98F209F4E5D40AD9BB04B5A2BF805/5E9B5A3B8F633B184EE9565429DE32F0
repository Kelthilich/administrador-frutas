using System;
using System.Web;
using System.Web.UI;

namespace frutas
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Configurar jQuery ScriptResourceMapping para validación unobtrusive
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "https://code.jquery.com/jquery-3.6.0.min.js",
                    DebugPath = "https://code.jquery.com/jquery-3.6.0.js",
                    CdnPath = "https://code.jquery.com/jquery-3.6.0.min.js",
                    CdnDebugPath = "https://code.jquery.com/jquery-3.6.0.js",
                    CdnSupportsSecureConnection = true
                });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Manejo básico de errores
        }
    }
}