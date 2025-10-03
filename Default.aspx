<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="frutas._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Welcome Section - MEJORADO -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="hero-banner shadow-lg rounded-3 overflow-hidden">
                    <div class="hero-content p-5">
                        <div class="d-flex align-items-center mb-3">
                            <div class="hero-icon me-4">
                                <i class="fas fa-apple-alt"></i>
                            </div>
                            <div>
                                <h1 class="display-3 fw-bold mb-2 text-white animate-fade-in">
                                    ¡Bienvenido al Sistema de Gestión de Frutas!
                                </h1>
                                <p class="lead text-white-50 mb-0">
                                    Gestiona tu inventario de frutas de manera eficiente y profesional.
                                </p>
                            </div>
                        </div>
                        <hr class="my-4 border-white-50" style="opacity: 0.3;">
                        <p class="text-white-50 mb-4">
                            <i class="fas fa-code me-2"></i>
                            Desarrollado con .NET Framework 4.8 siguiendo las mejores prácticas de arquitectura en capas.
                        </p>
                        <asp:Panel ID="pnlWelcomeActions" runat="server" Visible="false">
                            <div class="d-flex gap-3">
                                <a class="btn btn-light btn-lg shadow-sm" href="~/Frutas/ListaFrutas.aspx">
                                    <i class="fas fa-list me-2"></i> Ver Inventario
                                </a>
                                <a class="btn btn-outline-light btn-lg" href="~/Frutas/AgregarFruta.aspx">
                                    <i class="fas fa-plus me-2"></i> Agregar Fruta
                                </a>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Statistics Cards -->
        <div class="row mb-4 g-4">
            <div class="col-xl-3 col-md-6">
                <div class="stats-card border-left-primary">
                    <div class="card-body p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div>
                                <div class="stats-label text-primary">
                                    TOTAL FRUTAS
                                </div>
                                <div class="stats-value text-dark">
                                    <asp:Label ID="lblTotalFrutas" runat="server" Text="0" />
                                </div>
                            </div>
                            <div>
                                <div class="stats-icon bg-primary">
                                    <i class="fas fa-apple-alt"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6">
                <div class="stats-card border-left-success">
                    <div class="card-body p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div>
                                <div class="stats-label text-success">
                                    VALOR INVENTARIO
                                </div>
                                <div class="stats-value text-dark">
                                    $<asp:Label ID="lblValorInventario" runat="server" Text="0.00" />
                                </div>
                            </div>
                            <div>
                                <div class="stats-icon bg-success">
                                    <i class="fas fa-dollar-sign"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6">
                <div class="stats-card border-left-info">
                    <div class="card-body p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div>
                                <div class="stats-label text-info">
                                    STOCK TOTAL
                                </div>
                                <div class="stats-value text-dark">
                                    <asp:Label ID="lblStockTotal" runat="server" Text="0" />
                                </div>
                            </div>
                            <div>
                                <div class="stats-icon bg-info">
                                    <i class="fas fa-boxes"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-3 col-md-6">
                <div class="stats-card border-left-warning">
                    <div class="card-body p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div>
                                <div class="stats-label text-warning">
                                    STOCK BAJO
                                </div>
                                <div class="stats-value text-dark">
                                    <asp:Label ID="lblStockBajo" runat="server" Text="0" />
                                </div>
                            </div>
                            <div>
                                <div class="stats-icon bg-warning">
                                    <i class="fas fa-exclamation-triangle"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Content Row -->
        <div class="row g-4">
            <!-- Quick Actions - MEJORADO -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow-lg rounded-3 border-0 h-100">
                    <div class="card-header bg-white border-0 py-3">
                        <h5 class="m-0 fw-bold text-primary">
                            <i class="fas fa-bolt text-warning me-2"></i> Acciones Rápidas
                        </h5>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <a href="~/Frutas/ListaFrutas.aspx" runat="server" class="action-button btn-primary-custom">
                                    <div class="action-icon">
                                        <i class="fas fa-list"></i>
                                    </div>
                                    <div class="action-text">Ver Inventario</div>
                                </a>
                            </div>
                            <div class="col-md-6">
                                <a href="~/Frutas/AgregarFruta.aspx" runat="server" class="action-button btn-success-custom">
                                    <div class="action-icon">
                                        <i class="fas fa-plus"></i>
                                    </div>
                                    <div class="action-text">Agregar Fruta</div>
                                </a>
                            </div>
                            <div class="col-md-6" runat="server" id="divAdminActions" visible="false">
                                <a href="~/Admin/Usuarios.aspx" runat="server" class="action-button btn-info-custom">
                                    <div class="action-icon">
                                        <i class="fas fa-users"></i>
                                    </div>
                                    <div class="action-text">Gestionar Usuarios</div>
                                </a>
                            </div>
                            <div class="col-md-6" runat="server" id="divLogActions" visible="false">
                                <a href="~/Admin/Logs.aspx" runat="server" class="action-button btn-warning-custom">
                                    <div class="action-icon">
                                        <i class="fas fa-clipboard-list"></i>
                                    </div>
                                    <div class="action-text">Ver Logs</div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Recent Activity -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow-lg rounded-3 border-0 h-100">
                    <div class="card-header bg-white border-0 py-3">
                        <h5 class="m-0 fw-bold text-primary">
                            <i class="fas fa-clock text-info me-2"></i> Actividad Reciente
                        </h5>
                    </div>
                    <div class="card-body p-4" style="max-height: 400px; overflow-y: auto;">
                        <asp:Repeater ID="rptActividades" runat="server">
                            <ItemTemplate>
                                <div class="activity-item">
                                    <div class="activity-icon-wrapper">
                                        <div class="activity-icon bg-<%# GetActivityColor(Eval("Severidad").ToString()) %>">
                                            <i class="fas <%# GetActivityIcon(Eval("Accion").ToString()) %> text-white"></i>
                                        </div>
                                    </div>
                                    <div class="activity-content">
                                        <div class="activity-time"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></div>
                                        <div class="activity-title"><%# Eval("DetalleDepues") %></div>
                                        <div class="activity-user">Usuario: <%# Eval("Username") ?? "Sistema" %></div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <asp:Panel ID="pnlNoActivity" runat="server" Visible="false" CssClass="text-center py-5">
                            <i class="fas fa-info-circle fa-3x text-muted mb-3 opacity-50"></i>
                            <p class="text-muted mb-0">No hay actividad reciente para mostrar.</p>
                        </asp:Panel>
                        
                        <div class="text-center mt-3" runat="server" id="divVerMasLogs" visible="false">
                            <a href="~/Admin/Logs.aspx" class="btn btn-sm btn-outline-primary rounded-pill">
                                Ver todos los logs <i class="fas fa-arrow-right ms-1"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Charts Row -->
        <div class="row g-4 mt-2">
            <!-- Stock por Categoría -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow-lg rounded-3 border-0">
                    <div class="card-header bg-white border-0 py-3">
                        <h5 class="m-0 fw-bold text-primary">
                            <i class="fas fa-chart-pie me-2"></i> Stock por Categoría
                        </h5>
                    </div>
                    <div class="card-body p-4">
                        <asp:Repeater ID="rptCategorias" runat="server">
                            <ItemTemplate>
                                <div class="category-item">
                                    <div class="category-name">
                                        <strong><%# Eval("Categoria") %></strong>
                                        <small class="text-muted ms-2">(<%# Eval("CantidadFrutas") %> frutas)</small>
                                    </div>
                                    <div class="category-stats">
                                        <div class="category-stock"><%# Eval("StockTotal") %></div>
                                        <small class="text-success">$<%# Eval("ValorTotal", "{0:N2}") %></small>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <!-- Alertas y Notificaciones -->
            <div class="col-lg-6 mb-4">
                <div class="card shadow-lg rounded-3 border-0">
                    <div class="card-header bg-white border-0 py-3">
                        <h5 class="m-0 fw-bold text-primary">
                            <i class="fas fa-bell me-2"></i> Alertas y Notificaciones
                        </h5>
                    </div>
                    <div class="card-body p-4">
                        <!-- Stock Bajo -->
                        <asp:Panel ID="pnlStockBajo" runat="server" Visible="false" CssClass="alert alert-warning border-0 rounded-3">
                            <h6><i class="fas fa-exclamation-triangle me-2"></i> Stock Bajo</h6>
                            <asp:Repeater ID="rptStockBajo" runat="server">
                                <ItemTemplate>
                                    <div class="small">
                                        • <strong><%# Eval("Nombre") %></strong> - Stock: <%# Eval("Stock") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>

                        <!-- Próximas a Vencer -->
                        <asp:Panel ID="pnlProximasVencer" runat="server" Visible="false" CssClass="alert alert-info border-0 rounded-3">
                            <h6><i class="fas fa-calendar-exclamation me-2"></i> Próximas a Vencer</h6>
                            <asp:Repeater ID="rptProximasVencer" runat="server">
                                <ItemTemplate>
                                    <div class="small">
                                        • <strong><%# Eval("Nombre") %></strong> - Vence: <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>

                        <!-- Sin Alertas -->
                        <asp:Panel ID="pnlSinAlertas" runat="server" Visible="true" CssClass="alert alert-success border-0 rounded-3">
                            <h6><i class="fas fa-check-circle me-2"></i> Todo en Orden</h6>
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
        /* HERO BANNER - MEJORADO */
        .hero-banner {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            position: relative;
            overflow: hidden;
        }

        .hero-banner::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: url('data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="%23ffffff" fill-opacity="0.1" d="M0,96L48,112C96,128,192,160,288,160C384,160,480,128,576,122.7C672,117,768,139,864,154.7C960,171,1056,181,1152,165.3C1248,149,1344,107,1392,85.3L1440,64L1440,320L1392,320C1344,320,1248,320,1152,320C1056,320,960,320,864,320C768,320,672,320,576,320C480,320,384,320,288,320C192,320,96,320,48,320L0,320Z"></path></svg>') no-repeat bottom;
            background-size: cover;
            opacity: 0.3;
        }

        .hero-content {
            position: relative;
            z-index: 1;
        }

        .hero-icon {
            width: 80px;
            height: 80px;
            background: rgba(255, 255, 255, 0.2);
            border-radius: 20px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2.5rem;
            color: white;
            backdrop-filter: blur(10px);
        }

        .animate-fade-in {
            animation: fadeIn 1s ease-in;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(-20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        /* STATS CARDS - MEJORADO */
        .stats-card {
            background: white;
            border-radius: 15px;
            border: none;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
            height: 100%;
        }

        .stats-card .card-body {
            padding: 1.5rem !important;
        }

        .stats-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 4px;
            height: 100%;
            transition: width 0.3s ease;
        }

        .stats-card.border-left-primary::before { background: #4e73df; }
        .stats-card.border-left-success::before { background: #1cc88a; }
        .stats-card.border-left-info::before { background: #36b9cc; }
        .stats-card.border-left-warning::before { background: #f6c23e; }

        .stats-card:hover {
            transform: translateY(-8px);
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
        }

        .stats-card:hover::before {
            width: 8px;
        }

        .stats-label {
            font-size: 0.7rem;
            font-weight: 700;
            letter-spacing: 0.5px;
            text-transform: uppercase;
            margin-bottom: 0.5rem;
            line-height: 1.2;
        }

        .stats-value {
            font-size: 1.75rem;
            font-weight: 700;
            line-height: 1;
        }

        .stats-icon {
            width: 60px;
            height: 60px;
            border-radius: 15px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.75rem;
            color: white;
            opacity: 0.9;
            flex-shrink: 0;
        }

        /* ACTION BUTTONS - MEJORADO */
        .action-button {
            display: block;
            padding: 1.5rem;
            text-decoration: none;
            border-radius: 15px;
            text-align: center;
            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
            position: relative;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
        }

        .action-button::before {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            width: 0;
            height: 0;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.3);
            transform: translate(-50%, -50%);
            transition: width 0.6s, height 0.6s;
        }

        .action-button:hover::before {
            width: 300px;
            height: 300px;
        }

        .action-button:hover {
            transform: translateY(-5px) scale(1.02);
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.2);
        }

        .btn-primary-custom {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }

        .btn-success-custom {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
            color: white;
        }

        .btn-info-custom {
            background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
            color: white;
        }

        .btn-warning-custom {
            background: linear-gradient(135deg, #fa709a 0%, #fee140 100%);
            color: white;
        }

        .action-icon {
            font-size: 2.5rem;
            margin-bottom: 0.75rem;
            position: relative;
            z-index: 1;
        }

        .action-text {
            font-size: 1rem;
            font-weight: 600;
            position: relative;
            z-index: 1;
        }

        /* ACTIVITY ITEMS - MEJORADO */
        .activity-item {
            display: flex;
            padding: 1rem;
            border-radius: 12px;
            margin-bottom: 0.75rem;
            background: #f8f9fa;
            transition: all 0.3s ease;
        }

        .activity-item:hover {
            background: #e9ecef;
            transform: translateX(5px);
        }

        .activity-icon-wrapper {
            margin-right: 1rem;
        }

        .activity-icon {
            width: 45px;
            height: 45px;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.25rem;
        }

        .activity-content {
            flex: 1;
        }

        .activity-time {
            font-size: 0.75rem;
            color: #6c757d;
            margin-bottom: 0.25rem;
        }

        .activity-title {
            font-weight: 600;
            color: #212529;
            margin-bottom: 0.25rem;
        }

        .activity-user {
            font-size: 0.85rem;
            color: #6c757d;
        }

        /* CATEGORY ITEMS - MEJORADO */
        .category-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 1rem;
            border-radius: 12px;
            margin-bottom: 0.75rem;
            background: #f8f9fa;
            transition: all 0.3s ease;
        }

        .category-item:hover {
            background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
            transform: translateX(5px);
        }

        .category-name strong {
            color: #212529;
            font-size: 1rem;
        }

        .category-stats {
            text-align: right;
        }

        .category-stock {
            font-size: 1.25rem;
            font-weight: 700;
            color: #212529;
        }

        /* COLORS */
        .bg-primary { background-color: #4e73df !important; }
        .bg-success { background-color: #1cc88a !important; }
        .bg-info { background-color: #36b9cc !important; }
        .bg-warning { background-color: #f6c23e !important; }
        .bg-danger { background-color: #e74a3b !important; }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Animación suave para las cards
            $('.stats-card, .card').css('opacity', '0');
            $('.stats-card, .card').each(function(index) {
                $(this).delay(index * 100).animate({
                    opacity: 1
                }, 600);
            });
            
            // Tooltips
            $('[data-bs-toggle="tooltip"]').tooltip();
            
            // Smooth scroll
            $('a[href^="#"]').on('click', function(e) {
                e.preventDefault();
                var target = $(this.getAttribute('href'));
                if(target.length) {
                    $('html, body').stop().animate({
                        scrollTop: target.offset().top - 100
                    }, 1000);
                }
            });
        });
    </script>
</asp:Content>
