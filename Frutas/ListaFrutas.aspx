<%@ Page Title="Lista de Frutas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaFrutas.aspx.cs" Inherits="frutas.Frutas.ListaFrutas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Header -->
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2><i class="fas fa-apple-alt text-success"></i> Inventario de Frutas</h2>
                        <p class="text-muted">Gestiona el inventario completo de frutas</p>
                    </div>
                    <div>
                        <a href="AgregarFruta.aspx" class="btn btn-success">
                            <i class="fas fa-plus"></i> Agregar Fruta
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Filtros y B�squeda -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <h6 class="m-0"><i class="fas fa-filter"></i> Filtros de B�squeda</h6>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label for="txtBuscar">Buscar por Nombre:</label>
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" 
                                           placeholder="Nombre de la fruta..." />
                            </div>
                            <div class="col-md-2">
                                <label for="ddlCategoria">Categor�a:</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Text="Todas las categor�as" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label for="ddlPais">Pa�s de Origen:</label>
                                <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Text="Todos los pa�ses" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label for="chkSoloOrganicas">Filtros:</label>
                                <div class="form-check">
                                    <asp:CheckBox ID="chkSoloOrganicas" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label">Solo Org�nicas</label>
                                </div>
                                <div class="form-check">
                                    <asp:CheckBox ID="chkSoloDisponibles" runat="server" CssClass="form-check-input" Checked="true" />
                                    <label class="form-check-label">Solo Disponibles</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>&nbsp;</label>
                                <div class="d-flex">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                              CssClass="btn btn-primary mr-2" OnClick="btnBuscar_Click" />
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                                              CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Estad�sticas R�pidas -->
        <div class="row mb-4">
            <div class="col-md-3">
                <div class="card bg-primary text-white">
                    <div class="card-body text-center">
                        <h4><asp:Label ID="lblTotalFrutas" runat="server" Text="0" /></h4>
                        <p class="mb-0">Total Frutas</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-success text-white">
                    <div class="card-body text-center">
                        <h4>$<asp:Label ID="lblValorTotal" runat="server" Text="0.00" /></h4>
                        <p class="mb-0">Valor Total</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-info text-white">
                    <div class="card-body text-center">
                        <h4><asp:Label ID="lblStockTotal" runat="server" Text="0" /></h4>
                        <p class="mb-0">Stock Total</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-warning text-white">
                    <div class="card-body text-center">
                        <h4><asp:Label ID="lblPrecioPromedio" runat="server" Text="0.00" /></h4>
                        <p class="mb-0">Precio Promedio</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Lista de Frutas -->
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <h6 class="m-0"><i class="fas fa-list"></i> Lista de Frutas</h6>
                        <div>
                            <small class="text-muted">
                                P�gina <asp:Label ID="lblPaginaActual" runat="server" Text="1" /> 
                                de <asp:Label ID="lblTotalPaginas" runat="server" Text="1" /> 
                                (<asp:Label ID="lblTotalRegistros" runat="server" Text="0" /> registros)
                            </small>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <asp:GridView ID="gvFrutas" runat="server" CssClass="table table-hover table-responsive-sm"
                                    AutoGenerateColumns="false" DataKeyNames="Id" 
                                    OnRowCommand="gvFrutas_RowCommand" OnRowDataBound="gvFrutas_RowDataBound"
                                    EmptyDataText="No se encontraron frutas con los criterios especificados">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="50px" />
                                
                                <asp:TemplateField HeaderText="Fruta">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center">
                                            <div class="fruit-icon mr-3">
                                                <i class="fas fa-apple-alt fa-2x text-success"></i>
                                            </div>
                                            <div>
                                                <strong><%# Eval("Nombre") %></strong>
                                                <br />
                                                <small class="text-muted"><%# Eval("Descripcion") %></small>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Categor�a">
                                    <ItemTemplate>
                                        <span class="badge badge-secondary"><%# Eval("Categoria") %></span>
                                        <br />
                                        <small class="text-muted"><%# Eval("PaisOrigen") %></small>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Precio">
                                    <ItemTemplate>
                                        <strong class="text-success">$<%# Eval("Precio", "{0:N2}") %></strong>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Stock">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <span class='<%# GetStockCssClass((int)Eval("Stock")) %>'>
                                                <%# Eval("Stock") %>
                                            </span>
                                            <br />
                                            <small class="text-muted">unidades</small>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Informaci�n">
                                    <ItemTemplate>
                                        <div class="small">
                                            <%# (bool)Eval("EsOrganica") ? "<span class='badge badge-success'>Org�nica</span>" : "" %>
                                            <br />
                                            <span class="text-muted"><%# Eval("Temporada") %></span>
                                            <%# Eval("FechaVencimiento") != null ? 
                                                "<br/><small>Vence: " + ((DateTime)Eval("FechaVencimiento")).ToString("dd/MM/yyyy") + "</small>" : "" %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:LinkButton ID="btnEditar" runat="server" 
                                                          CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                                          CssClass="btn btn-sm btn-outline-primary" 
                                                          ToolTip="Editar">
                                                <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" 
                                                          CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                                          CssClass="btn btn-sm btn-outline-danger" 
                                                          ToolTip="Eliminar"
                                                          OnClientClick="return confirm('�Est� seguro de eliminar esta fruta?');">
                                                <i class="fas fa-trash"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <!-- Paginaci�n -->
                    <div class="card-footer">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <label for="ddlTama�oPagina">Mostrar:</label>
                                <asp:DropDownList ID="ddlTama�oPagina" runat="server" CssClass="form-control form-control-sm d-inline-block w-auto ml-2" AutoPostBack="true" OnSelectedIndexChanged="ddlTama�oPagina_SelectedIndexChanged">
                                    <asp:ListItem Value="10" Text="10" />
                                    <asp:ListItem Value="25" Text="25" Selected="True" />
                                    <asp:ListItem Value="50" Text="50" />
                                    <asp:ListItem Value="100" Text="100" />
                                </asp:DropDownList>
                                <span class="ml-2">registros por p�gina</span>
                            </div>
                            
                            <div>
                                <asp:Button ID="btnPrimera" runat="server" Text="Primeira" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnPrimera_Click" />
                                <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnAnterior_Click" />
                                <span class="mx-3">
                                    P�gina 
                                    <asp:TextBox ID="txtPagina" runat="server" CssClass="form-control form-control-sm d-inline-block text-center" 
                                               Style="width: 60px;" AutoPostBack="true" OnTextChanged="txtPagina_TextChanged" />
                                    de <asp:Label ID="lblTotalPaginasFooter" runat="server" Text="1" />
                                </span>
                                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnSiguiente_Click" />
                                <asp:Button ID="btnUltima" runat="server" Text="�ltima" CssClass="btn btn-sm btn-outline-secondary" 
                                          OnClick="btnUltima_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .fruit-icon {
            width: 40px;
            text-align: center;
        }
        
        .table th {
            border-top: none;
            font-weight: 600;
            background-color: #f8f9fa;
        }
        
        .badge-stock-ok { background-color: #28a745; }
        .badge-stock-low { background-color: #ffc107; color: #212529; }
        .badge-stock-empty { background-color: #dc3545; }
        
        .card-hover {
            transition: transform 0.2s ease;
        }
        
        .card-hover:hover {
            transform: translateY(-2px);
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Inicializar tooltips
            $('[data-toggle="tooltip"]').tooltip();
            
            // Auto-b�squeda con delay
            $('#<%= txtBuscar.ClientID %>').on('input', function() {
                clearTimeout(window.searchTimeout);
                window.searchTimeout = setTimeout(function() {
                    // Trigger b�squeda autom�tica despu�s de 1 segundo de inactividad
                    // $('#<%= btnBuscar.ClientID %>').click();
                }, 1000);
            });

            // Highlight filas al hover
            $('#<%= gvFrutas.ClientID %> tbody tr').hover(
                function() { $(this).addClass('table-active'); },
                function() { $(this).removeClass('table-active'); }
            );
        });
    </script>
</asp:Content>