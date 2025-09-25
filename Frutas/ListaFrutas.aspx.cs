using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using frutas.DTOs;
using frutas.Security;
using frutas.Services;

namespace frutas.Frutas
{
    public partial class ListaFrutas : Page
    {
        private readonly FrutaService _frutaService;
        private int PaginaActual
        {
            get { return ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 1; }
            set { ViewState["PaginaActual"] = value; }
        }

        private int TotalPaginas
        {
            get { return ViewState["TotalPaginas"] != null ? (int)ViewState["TotalPaginas"] : 1; }
            set { ViewState["TotalPaginas"] = value; }
        }

        public ListaFrutas()
        {
            _frutaService = new FrutaService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticaci�n
            SiteMaster.RequireLogin();

            if (!IsPostBack)
            {
                CargarFiltros();
                CargarFrutas();
            }
        }

        /// <summary>
        /// Carga los datos para los dropdowns de filtros
        /// </summary>
        private void CargarFiltros()
        {
            try
            {
                // Cargar categor�as
                var categoriasResult = _frutaService.ObtenerCategorias();
                if (categoriasResult.Exitoso)
                {
                    foreach (var categoria in categoriasResult.Data)
                    {
                        ddlCategoria.Items.Add(new ListItem(categoria, categoria));
                    }
                }

                // Cargar pa�ses
                var paisesResult = _frutaService.ObtenerPaises();
                if (paisesResult.Exitoso)
                {
                    foreach (var pais in paisesResult.Data)
                    {
                        ddlPais.Items.Add(new ListItem(pais, pais));
                    }
                }
            }
            catch (Exception ex)
            {
                var master = this.Master as SiteMaster;
                master?.MostrarAlerta($"Error cargando filtros: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Carga la lista de frutas con filtros y paginaci�n
        /// </summary>
        private void CargarFrutas()
        {
            try
            {
                var filtro = CrearFiltro();
                var resultado = _frutaService.ObtenerTodas(filtro);

                if (resultado.Exitoso)
                {
                    var data = resultado.Data;
                    
                    // Caregar datos al GridView
                    gvFrutas.DataSource = data.Frutas;
                    gvFrutas.DataBind();

                    // Actualizar estad�sticas
                    ActualizarEstadisticas(data.Frutas);

                    // Actualizar informaci�n de paginaci�n
                    ActualizarPaginacion(data);
                }
                else
                {
                    var master = this.Master as SiteMaster;
                    master?.MostrarAlerta($"Error cargando frutas: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                var master = this.Master as SiteMaster;
                master?.MostrarAlerta($"Error interno: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Crea el objeto de filtro basado en los controles de la p�gina
        /// </summary>
        private FrutaFiltroDto CrearFiltro()
        {
            return new FrutaFiltroDto
            {
                Nombre = string.IsNullOrEmpty(txtBuscar.Text) ? null : txtBuscar.Text.Trim(),
                Categoria = string.IsNullOrEmpty(ddlCategoria.SelectedValue) ? null : ddlCategoria.SelectedValue,
                PaisOrigen = string.IsNullOrEmpty(ddlPais.SelectedValue) ? null : ddlPais.SelectedValue,
                EsOrganica = chkSoloOrganicas.Checked ? (bool?)true : null,
                SoloDisponibles = chkSoloDisponibles.Checked,
                Pagina = PaginaActual,
                Tama�oPagina = int.Parse(ddlTama�oPagina.SelectedValue),
                OrdenarPor = "Nombre",
                OrdenDescendente = false
            };
        }

        /// <summary>
        /// Actualiza las estad�sticas mostradas en las tarjetas
        /// </summary>
        private void ActualizarEstadisticas(System.Collections.Generic.IEnumerable<FrutaDto> frutas)
        {
            if (frutas?.Any() == true)
            {
                var listaFrutas = frutas.ToList();
                
                lblTotalFrutas.Text = listaFrutas.Count.ToString();
                lblValorTotal.Text = listaFrutas.Sum(f => f.Precio * f.Stock).ToString("N2");
                lblStockTotal.Text = listaFrutas.Sum(f => f.Stock).ToString();
                lblPrecioPromedio.Text = listaFrutas.Average(f => f.Precio).ToString("N2");
            }
            else
            {
                lblTotalFrutas.Text = "0";
                lblValorTotal.Text = "0.00";
                lblStockTotal.Text = "0";
                lblPrecioPromedio.Text = "0.00";
            }
        }

        /// <summary>
        /// Actualiza la informaci�n de paginaci�n
        /// </summary>
        private void ActualizarPaginacion(FrutaListaDto data)
        {
            TotalPaginas = (int)Math.Ceiling((double)data.TotalRegistros / data.Tama�oPagina);
            
            lblPaginaActual.Text = data.PaginaActual.ToString();
            lblTotalPaginas.Text = TotalPaginas.ToString();
            lblTotalPaginasFooter.Text = TotalPaginas.ToString();
            lblTotalRegistros.Text = data.TotalRegistros.ToString();
            txtPagina.Text = data.PaginaActual.ToString();

            // Habilitar/deshabilitar botones de navegaci�n
            btnPrimera.Enabled = btnAnterior.Enabled = data.PaginaActual > 1;
            btnSiguiente.Enabled = btnUltima.Enabled = data.PaginaActual < TotalPaginas;
        }

        #region Eventos de Controles

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            PaginaActual = 1; // Resetear a primera p�gina
            CargarFrutas();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlCategoria.SelectedIndex = 0;
            ddlPais.SelectedIndex = 0;
            chkSoloOrganicas.Checked = false;
            chkSoloDisponibles.Checked = true;
            PaginaActual = 1;
            CargarFrutas();
        }

        protected void ddlTama�oPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaginaActual = 1; // Resetear a primera p�gina
            CargarFrutas();
        }

        #endregion

        #region Eventos de Paginaci�n

        protected void btnPrimera_Click(object sender, EventArgs e)
        {
            PaginaActual = 1;
            CargarFrutas();
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
                CargarFrutas();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (PaginaActual < TotalPaginas)
            {
                PaginaActual++;
                CargarFrutas();
            }
        }

        protected void btnUltima_Click(object sender, EventArgs e)
        {
            PaginaActual = TotalPaginas;
            CargarFrutas();
        }

        protected void txtPagina_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPagina.Text, out int pagina) && pagina >= 1 && pagina <= TotalPaginas)
            {
                PaginaActual = pagina;
                CargarFrutas();
            }
            else
            {
                txtPagina.Text = PaginaActual.ToString();
            }
        }

        #endregion

        #region Eventos del GridView

        protected void gvFrutas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int frutaId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"EditarFruta.aspx?id={frutaId}");
                    break;

                case "Eliminar":
                    EliminarFruta(frutaId);
                    break;
            }
        }

        protected void gvFrutas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var fruta = (FrutaDto)e.Row.DataItem;
                
                // Verificar permisos para editar/eliminar
                var btnEditar = e.Row.FindControl("btnEditar") as LinkButton;
                var btnEliminar = e.Row.FindControl("btnEliminar") as LinkButton;

                if (btnEditar != null && btnEliminar != null)
                {
                    // Los administradores pueden editar y eliminar todo
                    if (SessionHelper.EsAdministrador)
                    {
                        btnEditar.Visible = true;
                        btnEliminar.Visible = true;
                    }
                    else
                    {
                        // Los usuarios normales solo pueden editar/eliminar sus propias frutas
                        bool esPropio = fruta.UsuarioCreador == SessionHelper.UsuarioActualUsername;
                        btnEditar.Visible = esPropio;
                        btnEliminar.Visible = esPropio;
                    }
                }
            }
        }

        #endregion

        #region M�todos Helper

        /// <summary>
        /// Elimina una fruta
        /// </summary>
        private void EliminarFruta(int frutaId)
        {
            try
            {
                var resultado = _frutaService.Eliminar(frutaId);
                
                var master = this.Master as SiteMaster;
                if (resultado.Exitoso)
                {
                    master?.MostrarAlerta("Fruta eliminada exitosamente", "success");
                    CargarFrutas(); // Recargar la lista
                }
                else
                {
                    master?.MostrarAlerta($"Error eliminando fruta: {resultado.Mensaje}", "danger");
                }
            }
            catch (Exception ex)
            {
                var master = this.Master as SiteMaster;
                master?.MostrarAlerta($"Error interno: {ex.Message}", "danger");
            }
        }

        /// <summary>
        /// Obtiene la clase CSS para el badge de stock
        /// </summary>
        protected string GetStockCssClass(int stock)
        {
            if (stock == 0)
                return "badge badge-stock-empty";
            else if (stock < 10)
                return "badge badge-stock-low";
            else
                return "badge badge-stock-ok";
        }

        #endregion
    }
}