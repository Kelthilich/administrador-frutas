<%@ Page Title="Acerca de" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="frutas.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-12">
                <!-- Hero Section -->
                <div class="bg-gradient-primary text-white rounded shadow-lg p-5 mb-4">
                    <div class="text-center">
                        <i class="fas fa-apple-alt fa-4x mb-3"></i>
                        <h1 class="display-4 fw-bold">Sistema de Gestión de Frutas</h1>
                        <p class="lead">Solución completa para la gestión de inventario de frutas</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="row g-4">
            <!-- Características -->
            <div class="col-md-4">
                <div class="card h-100 shadow">
                    <div class="card-body text-center">
                        <i class="fas fa-shield-alt fa-3x text-primary mb-3"></i>
                        <h5 class="card-title fw-bold">Seguro y Confiable</h5>
                        <p class="card-text">Sistema con autenticación robusta, encriptación de contraseñas y control de acceso por roles.</p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 shadow">
                    <div class="card-body text-center">
                        <i class="fas fa-chart-line fa-3x text-success mb-3"></i>
                        <h5 class="card-title fw-bold">Estadísticas en Tiempo Real</h5>
                        <p class="card-text">Visualiza métricas importantes de tu inventario con dashboards interactivos y actualizados.</p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 shadow">
                    <div class="card-body text-center">
                        <i class="fas fa-mobile-alt fa-3x text-info mb-3"></i>
                        <h5 class="card-title fw-bold">100% Responsive</h5>
                        <p class="card-text">Accede desde cualquier dispositivo: computadora, tablet o smartphone con diseño adaptable.</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tecnologías -->
        <div class="row mt-5">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header bg-white">
                        <h4 class="mb-0 fw-bold"><i class="fas fa-code"></i> Tecnologías Utilizadas</h4>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-3 col-6 text-center">
                                <div class="p-3 border rounded">
                                    <i class="fab fa-microsoft fa-2x text-primary mb-2"></i>
                                    <div class="fw-bold">ASP.NET</div>
                                    <small class="text-muted">Web Forms</small>
                                </div>
                            </div>
                            <div class="col-md-3 col-6 text-center">
                                <div class="p-3 border rounded">
                                    <i class="fas fa-database fa-2x text-success mb-2"></i>
                                    <div class="fw-bold">SQL Server</div>
                                    <small class="text-muted">LocalDB</small>
                                </div>
                            </div>
                            <div class="col-md-3 col-6 text-center">
                                <div class="p-3 border rounded">
                                    <i class="fab fa-bootstrap fa-2x text-info mb-2"></i>
                                    <div class="fw-bold">Bootstrap 5</div>
                                    <small class="text-muted">Framework CSS</small>
                                </div>
                            </div>
                            <div class="col-md-3 col-6 text-center">
                                <div class="p-3 border rounded">
                                    <i class="fab fa-js fa-2x text-warning mb-2"></i>
                                    <div class="fw-bold">jQuery</div>
                                    <small class="text-muted">JavaScript</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Versión -->
        <div class="row mt-4">
            <div class="col-12">
                <div class="alert alert-info">
                    <div class="d-flex align-items-center">
                        <i class="fas fa-info-circle fa-2x me-3"></i>
                        <div>
                            <h6 class="alert-heading mb-1">Versión del Sistema</h6>
                            <p class="mb-0">
                                <strong>Versión 1.0.0</strong> | 
                                .NET Framework 4.8 | 
                                Desarrollado con <i class="fas fa-heart text-danger"></i> por el equipo de desarrollo
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .bg-gradient-primary {
            background: linear-gradient(135deg, #4e73df 0%, #224abe 100%);
        }
    </style>
</asp:Content>
