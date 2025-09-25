using System;
using System.Web.UI;
using frutas.Security;
using frutas.Services;

namespace frutas
{
    public partial class Test : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInformacionSistema();
            }
        }

        private void CargarInformacionSistema()
        {
            // Verificar usuario logueado
            lblUsuarioLogueado.Text = SessionHelper.EstaLogueado ? SessionHelper.UsuarioActualUsername : "No logueado";
            lblEsAdmin.Text = SessionHelper.EsAdministrador ? "Sí" : "No";

            // Probar conexión a BD
            try
            {
                var frutaService = new FrutaService();
                var estadisticas = frutaService.ObtenerEstadisticas();
                
                if (estadisticas.Exitoso)
                {
                    lblConexionBD.Text = "Exitosa";
                    lblTotalFrutas.Text = estadisticas.Data.TotalFrutas.ToString();
                }
                else
                {
                    lblConexionBD.Text = "Error: " + estadisticas.Mensaje;
                    lblTotalFrutas.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                lblConexionBD.Text = "Excepción: " + ex.Message;
                lblTotalFrutas.Text = "N/A";
            }
        }
    }
}