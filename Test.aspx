<%@ Page Title="Test Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="frutas.Test" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h4><i class="fas fa-cog"></i> Página de Prueba del Sistema</h4>
                    </div>
                    <div class="card-body">
                        <h5>Estado del Sistema:</h5>
                        <ul class="list-group">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Usuario Logueado:
                                <span class="badge badge-primary"><asp:Label ID="lblUsuarioLogueado" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Es Administrador:
                                <span class="badge badge-secondary"><asp:Label ID="lblEsAdmin" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Conexión a BD:
                                <span class="badge badge-success"><asp:Label ID="lblConexionBD" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Total Frutas en BD:
                                <span class="badge badge-info"><asp:Label ID="lblTotalFrutas" runat="server" /></span>
                            </li>
                        </ul>

                        <div class="mt-4">
                            <h5>Enlaces de Prueba:</h5>
                            <div class="btn-group-vertical" role="group">
                                <a href="~/Default.aspx" class="btn btn-outline-primary" runat="server">Dashboard</a>
                                <a href="~/Frutas/ListaFrutas.aspx" class="btn btn-outline-success" runat="server">Lista de Frutas</a>
                                <a href="~/Frutas/AgregarFruta.aspx" class="btn btn-outline-info" runat="server">Agregar Fruta</a>
                                <a href="~/Account/Login.aspx" class="btn btn-outline-warning" runat="server">Login</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>