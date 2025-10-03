<%@ Page Title="Logs del Sistema" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logs.aspx.cs" Inherits="frutas.Admin.Logs" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Header -->
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2 class="fw-bold"><i class="fas fa-clipboard-list text-warning"></i> Logs del Sistema</h2>
                        <p class="text-muted">Auditoría y seguimiento de actividades</p>
                    </div>
                    <div>
                        <a href="~/Default.aspx" runat="server" class="btn btn-outline-secondary">
                            <i class="fas fa-arrow-left"></i> Volver al Dashboard
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Filtros -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header bg-white">
                        <h6 class="m-0 fw-bold"><i class="fas fa-filter"></i> Filtros de Búsqueda</h6>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-2">
                                <label for="ddlSeveridad" class="form-label fw-bold">Severidad:</label>
                                <asp:DropDownList ID="ddlSeveridad" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="" Text="Todas" />
                                    <asp:ListItem Value="INFO" Text="Información" />
                                    <asp:ListItem Value="WARNING" Text="Advertencia" />
                                    <asp:ListItem Value="ERROR" Text="Error" />
                                    <asp:ListItem Value="CRITICAL" Text="Crítico" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label for="ddlAccion" class="form-label fw-bold">Acción:</label>
                                <asp:DropDownList ID="ddlAccion" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="" Text="Todas las acciones" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label for="txtUsuario" class="form-label fw-bold">Usuario:</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" 
                                           placeholder="Nombre de usuario..." />
                            </div>
                            <div class="col-md-2">
                                <label for="txtFechaDesde" class="form-label fw-bold">Desde:</label>
                                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" 
                                           TextMode="Date" />
                            </div>
                            <div class="col-md-2">
                                <label for="txtFechaHasta" class="form-label fw-bold">Hasta:</label>
                                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" 
                                           TextMode="Date" />
                            </div>
                            <div class="col-md-2">
                                <label class="form-label">&nbsp;</label>
                                <div class="d-grid gap-2">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                              CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                                              CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Estadísticas -->
        <div class="row mb-4 g-3">
            <div class="col-md-3">
                <div class="card bg-info text-white shadow">
                    <div class="card-body text-center">
                        <h4 class="fw-bold"><asp:Label ID="lblTotalLogs" runat="server" Text="0" /></h4>
                        <p class="mb-0"><i class="fas fa-list"></i> Total Logs</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-success text-white shadow">
                    <div class="card-body text-center">
                        <h4 class="fw-bold"><asp:Label ID="lblLogsExitosos" runat="server" Text="0" /></h4>
                        <p class="mb-0"><i class="fas fa-check-circle"></i> Exitosos</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-warning text-white shadow">
                    <div class="card-body text-center">
                        <h4 class="fw-bold"><asp:Label ID="lblLogsAdvertencia" runat="server" Text="0" /></h4>
                        <p class="mb-0"><i class="fas fa-exclamation-triangle"></i> Advertencias</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-danger text-white shadow">
                    <div class="card-body text-center">
                        <h4 class="fw-bold"><asp:Label ID="lblLogsError" runat="server" Text="0" /></h4>
                        <p class="mb-0"><i class="fas fa-times-circle"></i> Errores</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Lista de Logs -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header d-flex justify-content-between align-items-center bg-white">
                        <h6 class="m-0 fw-bold text-primary">
                            <i class="fas fa-list"></i> Registro de Actividades
                        </h6>
                        <div>
                            <small class="text-muted">
                                Página <asp:Label ID="lblPaginaActual" runat="server" Text="1" CssClass="fw-bold" /> 
                                de <asp:Label ID="lblTotalPaginas" runat="server" Text="1" CssClass="fw-bold" /> 
                                (<asp:Label ID="lblTotalRegistros" runat="server" Text="0" /> registros)
                            </small>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="gvLogs" runat="server" CssClass="table table-hover mb-0"
                                        AutoGenerateColumns="false" DataKeyNames="Id" 
                                        OnRowDataBound="gvLogs_RowDataBound"
                                        EmptyDataText="No se encontraron logs con los criterios especificados">
                                <Columns>
                                    <asp:TemplateField HeaderText="Fecha/Hora" ItemStyle-Width="140px">
                                        <ItemTemplate>
                                            <small class="text-muted">
                                                <i class="fas fa-calendar"></i> <%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %><br/>
                                                <i class="fas fa-clock"></i> <%# ((DateTime)Eval("Fecha")).ToString("HH:mm:ss") %>
                                            </small>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Severidad" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <span class='<%# GetSeveridadCssClass(Eval("Severidad").ToString()) %>'>
                                                <%# GetSeveridadTexto(Eval("Severidad").ToString()) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Usuario" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <div class="d-flex align-items-center">
                                                <i class="fas fa-user-circle fa-lg text-primary me-2"></i>
                                                <div>
                                                    <strong><%# Eval("Username") ?? "Sistema" %></strong>
                                                    <br/>
                                                    <small class="text-muted">ID: <%# Eval("UsuarioId") ?? "N/A" %></small>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acción">
                                        <ItemTemplate>
                                            <div>
                                                <strong><%# Eval("Accion") %></strong>
                                                <br/>
                                                <small class="text-muted">
                                                    Tabla: <%# Eval("Tabla") ?? "N/A" %>
                                                    <%# Eval("RegistroId") != null ? " | ID: " + Eval("RegistroId") : "" %>
                                                </small>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemTemplate>
                                            <div class="log-details">
                                                <%# GetDetallesTruncados(Eval("DetalleDepues"), Eval("Id")) %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IP/Info" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <small class="text-muted">
                                                <i class="fas fa-network-wired"></i> <%# Eval("IP") ?? "N/A" %><br/>
                                                <%# (bool)Eval("Exitoso") ? 
                                                    "<span class='text-success'><i class='fas fa-check-circle'></i> Exitoso</span>" : 
                                                    "<span class='text-danger'><i class='fas fa-times-circle'></i> Fallido</span>" %>
                                            </small>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                    <!-- Paginación -->
                    <div class="card-footer">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <label for="ddlTamañoPagina" class="form-label mb-0 me-2">Mostrar:</label>
                                <asp:DropDownList ID="ddlTamañoPagina" runat="server" CssClass="form-select form-select-sm d-inline-block w-auto" 
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTamañoPagina_SelectedIndexChanged">
                                    <asp:ListItem Value="25" Text="25" Selected="True" />
                                    <asp:ListItem Value="50" Text="50" />
                                    <asp:ListItem Value="100" Text="100" />
                                    <asp:ListItem Value="200" Text="200" />
                                </asp:DropDownList>
                                <span class="ms-2">registros por página</span>
                            </div>
                            
                            <div>
                                <asp:Button ID="btnPrimera" runat="server" Text="Primera" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnPrimera_Click" />
                                <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnAnterior_Click" />
                                <span class="mx-3">
                                    Página 
                                    <asp:TextBox ID="txtPagina" runat="server" CssClass="form-control form-control-sm d-inline-block text-center" 
                                               Style="width: 60px;" AutoPostBack="true" OnTextChanged="txtPagina_TextChanged" />
                                    de <asp:Label ID="lblTotalPaginasFooter" runat="server" Text="1" CssClass="fw-bold" />
                                </span>
                                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnSiguiente_Click" />
                                <asp:Button ID="btnUltima" runat="server" Text="Última" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnUltima_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal para detalles completos -->
        <div class="modal fade" id="modalDetalles" tabindex="-1" aria-labelledby="modalDetallesLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title" id="modalDetallesLabel">
                            <i class="fas fa-info-circle"></i> Detalles del Log
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="detalleCompleto"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                            <i class="fas fa-times"></i> Cerrar
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .log-details {
            font-size: 0.9em;
            line-height: 1.3;
        }
        
        .badge-severity-info { background-color: #17a2b8; color: white; }
        .badge-severity-warning { background-color: #ffc107; color: #212529; }  
        .badge-severity-error { background-color: #dc3545; color: white; }
        .badge-severity-critical { background-color: #6f42c1; color: white; }
        
        .table td {
            vertical-align: middle;
        }
        
        .table-hover tbody tr:hover {
            background-color: rgba(0,0,0,.02);
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Configurar fechas por defecto (últimos 7 días)
            var hoy = new Date();
            var hace7dias = new Date();
            hace7dias.setDate(hoy.getDate() - 7);
            
            if ($('#<%= txtFechaDesde.ClientID %>').val() === '') {
                $('#<%= txtFechaDesde.ClientID %>').val(hace7dias.toISOString().split('T')[0]);
            }
            if ($('#<%= txtFechaHasta.ClientID %>').val() === '') {
                $('#<%= txtFechaHasta.ClientID %>').val(hoy.toISOString().split('T')[0]);
            }
        });

        function mostrarDetalleCompleto(logId, detalleCompleto) {
            // Decodificar HTML entities
            var detalle = $('<div/>').html(detalleCompleto).text();
            
            // Mostrar en el modal
            $('#detalleCompleto').html('<pre class="bg-light p-3 rounded">' + detalle + '</pre>');
            
            var modal = new bootstrap.Modal(document.getElementById('modalDetalles'));
            modal.show();
        }
    </script>
</asp:Content>