<%@ Page Title="Test Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="frutas.Test" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header bg-info text-white">
                        <h4 class="mb-0"><i class="fas fa-cog"></i> Página de Prueba del Sistema</h4>
                    </div>
                    <div class="card-body">
                        <h5 class="fw-bold">Estado del Sistema:</h5>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Usuario Logueado:
                                <span class="badge bg-primary"><asp:Label ID="lblUsuarioLogueado" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Es Administrador:
                                <span class="badge bg-secondary"><asp:Label ID="lblEsAdmin" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Conexión a BD:
                                <span class="badge bg-success"><asp:Label ID="lblConexionBD" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Total Frutas en BD:
                                <span class="badge bg-info"><asp:Label ID="lblTotalFrutas" runat="server" /></span>
                            </li>
                        </ul>

                        <div class="mt-4">
                            <h5 class="fw-bold">Enlaces de Prueba:</h5>
                            <div class="d-grid gap-2" role="group">
                                <a href="~/Default.aspx" class="btn btn-outline-primary" runat="server">
                                    <i class="fas fa-home"></i> Dashboard
                                </a>
                                <a href="~/Frutas/ListaFrutas.aspx" class="btn btn-outline-success" runat="server">
                                    <i class="fas fa-list"></i> Lista de Frutas
                                </a>
                                <a href="~/Frutas/AgregarFruta.aspx" class="btn btn-outline-info" runat="server">
                                    <i class="fas fa-plus"></i> Agregar Fruta
                                </a>
                                <a href="~/Account/Login.aspx" class="btn btn-outline-warning" runat="server">
                                    <i class="fas fa-sign-in-alt"></i> Login
                                </a>
                            </div>
                        </div>

                        <div class="mt-4 alert alert-info" role="alert">
                            <h6 class="alert-heading"><i class="fas fa-info-circle"></i> Información</h6>
                            <p class="mb-0">Esta es una página de prueba para verificar el correcto funcionamiento del sistema.</p>
                        </div>
                    </div>
                    <div class="card-footer text-muted text-center">
                        <small>Sistema de Gestión de Frutas - .NET Framework 4.8</small>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>