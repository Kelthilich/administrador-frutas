<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="frutas._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Welcome Section -->
        <div class="row">
            <div class="col-12">
                <div class="jumbotron bg-gradient-success text-white">
                    <div class="container">
                        <h1 class="display-4">
                            <i class="fas fa-apple-alt"></i> 
                            ¡Bienvenido al Sistema de Gestión de Frutas!
                        </h1>
                        <p class="lead">
                            Gestiona tu inventario de frutas de manera eficiente y profesional.
                        </p>
                        <hr class="my-4 bg-white">
                        <p>
                            Desarrollado con .NET Framework 4.8 siguiendo las mejores prácticas de arquitectura en capas.
                        </p>
                        <asp:Panel ID="pnlWelcomeActions" runat="server" Visible="false">
                            <a class="btn btn-light btn-lg" href="~/Frutas/ListaFrutas.aspx" role="button">
                                <i class="fas fa-list"></i> Ver Inventario
                            </a>
                            <a class="btn btn-outline-light btn-lg ms-2" href="~/Frutas/AgregarFruta.aspx" role="button">
                                <i class="fas fa-plus"></i> Agregar Fruta
                            </a>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Statistics Cards -->
        <div class="row mb-4">
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col me-2">
                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                    Total Frutas
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblTotalFrutas" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-apple-alt fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-success shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col me-2">
                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                    Valor Inventario
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    $<asp:Label ID="lblValorInventario" runat="server" Text="0.00" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-info shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col me-2">
                                <div class="text-xs font-weight-bold text-info text-uppercase mb-1">
                                    Stock Total
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblStockTotal" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-boxes fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-warning shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col me-2">
                                <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                    Stock Bajo
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblStockBajo" runat="server" Text="0" />
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-exclamation-triangle fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Content Row -->
        <div class="row">
            <!-- Quick Actions -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">
                            <i class="fas fa-bolt"></i> Acciones Rápidas
                        </h6>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <a href="~/Frutas/ListaFrutas.aspx" runat="server" class="btn btn-primary btn-block">
                                    <i class="fas fa-list"></i><br />
                                    Ver Inventario
                                </a>
                            </div>
                            <div class="col-md-6 mb-3">
                                <a href="~/Frutas/AgregarFruta.aspx" runat="server" class="btn btn-success btn-block">
                                    <i class="fas fa-plus"></i><br />
                                    Agregar Fruta
                                </a>
                            </div>
                            <div class="col-md-6 mb-3" runat="server" id="divAdminActions" visible="false">
                                <a href="~/Admin/Usuarios.aspx" runat="server" class="btn btn-info btn-block">
                                    <i class="fas fa-users"></i><br />
                                    Gestionar Usuarios
                                </a>
                            </div>
                            <div class="col-md-6 mb-3" runat="server" id="divLogActions" visible="false">
                                <a href="~/Admin/Logs.aspx" runat="server" class="btn btn-warning btn-block">
                                    <i class="fas fa-clipboard-list"></i><br />
                                    Ver Logs
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Recent Activity -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">
                            <i class="fas fa-clock"></i> Actividad Reciente
                        </h6>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptActividades" runat="server">
                            <ItemTemplate>
                                <div class="d-flex align-items-center py-2">
                                    <div class="me-3">
                                        <div class="icon-circle bg-<%# GetActivityColor(Eval("Severidad").ToString()) %>">
                                            <i class="fas <%# GetActivityIcon(Eval("Accion").ToString()) %> text-white"></i>
                                        </div>
                                    </div>
                                    <div class="flex-grow-1">
                                        <div class="small text-gray-500"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></div>
                                        <div class="font-weight-bold"><%# Eval("DetalleDepues") %></div>
                                        <div class="small text-muted">Usuario: <%# Eval("Username") ?? "Sistema" %></div>
                                    </div>
                                </div>
                                <hr class="my-2" />
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <asp:Panel ID="pnlNoActivity" runat="server" Visible="false" CssClass="text-center py-4">
                            <i class="fas fa-info-circle fa-2x text-muted mb-2"></i>
                            <p class="text-muted">No hay actividad reciente para mostrar.</p>
                        </asp:Panel>
                        
                        <div class="text-center mt-3" runat="server" id="divVerMasLogs" visible="false">
                            <a href="~/Admin/Logs.aspx" class="btn btn-sm btn-outline-primary">
                                Ver todos los logs <i class="fas fa-arrow-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Charts Row -->
        <div class="row">
            <!-- Stock por Categoría -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">
                            <i class="fas fa-chart-pie"></i> Stock por Categoría
                        </h6>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptCategorias" runat="server">
                            <ItemTemplate>
                                <div class="d-flex justify-content-between align-items-center py-2">
                                    <div>
                                        <strong><%# Eval("Categoria") %></strong>
                                        <small class="text-muted">(<%# Eval("CantidadFrutas") %> frutas)</small>
                                    </div>
                                    <div class="text-end">
                                        <div class="h6 mb-0 font-weight-bold"><%# Eval("StockTotal") %></div>
                                        <small class="text-success">$<%# Eval("ValorTotal", "{0:N2}") %></small>
                                    </div>
                                </div>
                                <hr class="my-1" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <!-- Alertas y Notificaciones -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">
                            <i class="fas fa-bell"></i> Alertas y Notificaciones
                        </h6>
                    </div>
                    <div class="card-body">
                        <!-- Stock Bajo -->
                        <asp:Panel ID="pnlStockBajo" runat="server" Visible="false" CssClass="alert alert-warning">
                            <h6><i class="fas fa-exclamation-triangle"></i> Stock Bajo</h6>
                            <asp:Repeater ID="rptStockBajo" runat="server">
                                <ItemTemplate>
                                    <div class="small">
                                        • <strong><%# Eval("Nombre") %></strong> - Stock: <%# Eval("Stock") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>

                        <!-- Próximas a Vencer -->
                        <asp:Panel ID="pnlProximasVencer" runat="server" Visible="false" CssClass="alert alert-info">
                            <h6><i class="fas fa-calendar-exclamation"></i> Próximas a Vencer</h6>
                            <asp:Repeater ID="rptProximasVencer" runat="server">
                                <ItemTemplate>
                                    <div class="small">
                                        • <strong><%# Eval("Nombre") %></strong> - Vence: <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>

                        <!-- Sin Alertas -->
                        <asp:Panel ID="pnlSinAlertas" runat="server" Visible="true" CssClass="alert alert-success">
                            <h6><i class="fas fa-check-circle"></i> Todo en Orden</h6>
                            <p class="mb-0 small">No hay alertas importantes en este momento.</p>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Public Section for Non-Authenticated Users -->
        <asp:Panel ID="pnlPublicSection" runat="server" Visible="false">
            <div class="row">
                <div class="col-12">
                    <div class="card shadow">
                        <div class="card-body text-center py-5">
                            <h3><i class="fas fa-sign-in-alt text-primary"></i> Inicie Sesión para Acceder</h3>
                            <p class="lead">Para gestionar el inventario de frutas, debe iniciar sesión en el sistema.</p>
                            <div class="mt-4">
                                <a href="~/Account/Login.aspx" class="btn btn-primary btn-lg me-3">
                                    <i class="fas fa-sign-in-alt"></i> Iniciar Sesión
                                </a>
                                <a href="~/Account/Register.aspx" class="btn btn-outline-primary btn-lg">
                                    <i class="fas fa-user-plus"></i> Registrarse
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .bg-gradient-success {
            background: linear-gradient(135deg, #28a745 0%, #20c997 100%);
        }
        
        .border-left-primary {
            border-left: 0.25rem solid #4e73df !important;
        }
        
        .border-left-success {
            border-left: 0.25rem solid #1cc88a !important;
        }
        
        .border-left-info {
            border-left: 0.25rem solid #36b9cc !important;
        }
        
        .border-left-warning {
            border-left: 0.25rem solid #f6c23e !important;
        }
        
        .icon-circle {
            height: 2.5rem;
            width: 2.5rem;
            border-radius: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        .text-gray-300 {
            color: #dddfeb !important;
        }
        
        .text-gray-500 {
            color: #858796 !important;
        }
        
        .text-gray-800 {
            color: #5a5c69 !important;
        }
        
        .bg-primary { background-color: #4e73df !important; }
        .bg-success { background-color: #1cc88a !important; }
        .bg-info { background-color: #36b9cc !important; }
        .bg-warning { background-color: #f6c23e !important; }
        .bg-danger { background-color: #e74a3b !important; }
        
        .card {
            transition: all 0.3s ease;
        }
        
        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15) !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Animaciones de entrada para las tarjetas
            $('.card').each(function(index) {
                $(this).delay(index * 100).fadeIn(500);
            });
            
            // Tooltip para los iconos
            $('[data-bs-toggle="tooltip"]').tooltip();
            
            // Auto-refresh cada 5 minutos
            setTimeout(function() {
                location.reload();
            }, 300000); // 5 minutos
        });
    </script>
</asp:Content>
