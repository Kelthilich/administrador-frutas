<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="frutas.Admin.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Header -->
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2 class="fw-bold"><i class="fas fa-users text-primary"></i> Gestión de Usuarios</h2>
                        <p class="text-muted">Administre los usuarios del sistema</p>
                    </div>
                    <div>
                        <a href="~/Default.aspx" runat="server" class="btn btn-outline-secondary">
                            <i class="fas fa-arrow-left"></i> Volver al Dashboard
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Estadísticas -->
        <div class="row mb-4">
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-start border-primary border-4 shadow h-100">
                    <div class="card-body">
                        <div class="row g-0 align-items-center">
                            <div class="col me-2">
                                <div class="text-xs fw-bold text-primary text-uppercase mb-1">
                                    Total Usuarios
                                </div>
                                <div class="h5 mb-0 fw-bold text-dark">
                                    <asp:Label ID="lblTotalUsuarios" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-users fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-start border-success border-4 shadow h-100">
                    <div class="card-body">
                        <div class="row g-0 align-items-center">
                            <div class="col me-2">
                                <div class="text-xs fw-bold text-success text-uppercase mb-1">
                                    Usuarios Activos
                                </div>
                                <div class="h5 mb-0 fw-bold text-dark">
                                    <asp:Label ID="lblUsuariosActivos" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-user-check fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-start border-info border-4 shadow h-100">
                    <div class="card-body">
                        <div class="row g-0 align-items-center">
                            <div class="col me-2">
                                <div class="text-xs fw-bold text-info text-uppercase mb-1">
                                    Administradores
                                </div>
                                <div class="h5 mb-0 fw-bold text-dark">
                                    <asp:Label ID="lblAdministradores" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-user-shield fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-start border-warning border-4 shadow h-100">
                    <div class="card-body">
                        <div class="row g-0 align-items-center">
                            <div class="col me-2">
                                <div class="text-xs fw-bold text-warning text-uppercase mb-1">
                                    Usuarios Bloqueados
                                </div>
                                <div class="h5 mb-0 fw-bold text-dark">
                                    <asp:Label ID="lblUsuariosBloqueados" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-user-lock fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Lista de Usuarios -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header py-3 bg-white">
                        <h6 class="m-0 fw-bold text-primary">
                            <i class="fas fa-table"></i> Lista de Usuarios
                        </h6>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-bordered table-hover"
                                        AutoGenerateColumns="false" DataKeyNames="Id" 
                                        OnRowCommand="gvUsuarios_RowCommand" OnRowDataBound="gvUsuarios_RowDataBound"
                                        EmptyDataText="No hay usuarios registrados">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="50px" />
                                    
                                    <asp:TemplateField HeaderText="Usuario">
                                        <ItemTemplate>
                                            <div class="d-flex align-items-center">
                                                <div class="user-icon me-3">
                                                    <i class="fas fa-user-circle fa-2x text-primary"></i>
                                                </div>
                                                <div>
                                                    <strong><%# Eval("Username") %></strong>
                                                    <br />
                                                    <small class="text-muted"><%# Eval("Email") %></small>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Información">
                                        <ItemTemplate>
                                            <div>
                                                <strong><%# Eval("NombreCompleto") ?? "Sin nombre" %></strong>
                                                <br />
                                                <span class='<%# GetRolCssClass(Eval("Rol").ToString()) %>'>
                                                    <%# Eval("Rol") %>
                                                </span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# (bool)Eval("Activo") ? 
                                                    "<span class='badge bg-success'>Activo</span>" : 
                                                    "<span class='badge bg-secondary'>Inactivo</span>" %>
                                                <br />
                                                <small class="text-muted">
                                                    Creado: <%# ((DateTime)Eval("FechaCreacion")).ToString("dd/MM/yyyy") %>
                                                </small>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Último Login">
                                        <ItemTemplate>
                                            <small class="text-muted">
                                                <i class="fas fa-clock"></i>
                                                <%# Eval("UltimoLogin") != null ? 
                                                    ((DateTime)Eval("UltimoLogin")).ToString("dd/MM/yyyy HH:mm") : 
                                                    "Nunca" %>
                                            </small>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <div class="btn-group" role="group">
                                                <asp:LinkButton ID="btnCambiarEstado" runat="server" 
                                                              CommandName="CambiarEstado" CommandArgument='<%# Eval("Id") %>'
                                                              CssClass='<%# (bool)Eval("Activo") ? "btn btn-sm btn-outline-warning" : "btn btn-sm btn-outline-success" %>' 
                                                              ToolTip='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>'>
                                                    <i class='<%# (bool)Eval("Activo") ? "fas fa-user-times" : "fas fa-user-check" %>'></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnResetPassword" runat="server" 
                                                              CommandName="ResetPassword" CommandArgument='<%# Eval("Id") %>'
                                                              CssClass="btn btn-sm btn-outline-info" 
                                                              ToolTip="Resetear Contraseña"
                                                              OnClientClick="return confirm('¿Generar nueva contraseña temporal?');">
                                                    <i class="fas fa-key"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal para mostrar contraseña temporal -->
        <div class="modal fade" id="modalPassword" tabindex="-1" aria-labelledby="modalPasswordLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h5 class="modal-title" id="modalPasswordLabel">
                            <i class="fas fa-key"></i> Contraseña Temporal Generada
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Se ha generado una nueva contraseña temporal para el usuario:</p>
                        <div class="alert alert-success">
                            <strong><i class="fas fa-lock"></i> Nueva Contraseña:</strong> 
                            <span id="nuevaPassword" class="fw-bold"></span>
                        </div>
                        <p class="text-muted">
                            <small>
                                <i class="fas fa-info-circle"></i> 
                                Proporcione esta contraseña al usuario. Se recomienda que la cambie en su primer login.
                            </small>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">
                            <i class="fas fa-check"></i> Entendido
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .text-gray-300 {
            color: #dddfeb !important;
        }
        
        .user-icon {
            width: 40px;
            text-align: center;
        }
        
        .badge-rol-admin {
            background-color: #e74a3b;
            color: white;
        }
        
        .badge-rol-user {
            background-color: #6c757d;
            color: white;
        }
        
        .text-xs {
            font-size: 0.7rem;
        }
    </style>

    <script type="text/javascript">
        function mostrarPassword(password) {
            document.getElementById('nuevaPassword').innerText = password;
            var modal = new bootstrap.Modal(document.getElementById('modalPassword'));
            modal.show();
        }
        
        $(document).ready(function() {
            // Inicializar tooltips de Bootstrap 5
            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            });
        });
    </script>
</asp:Content>