<%@ Page Title="Página no encontrada" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6 text-center">
                <div class="card shadow">
                    <div class="card-body">
                        <i class="fas fa-search fa-5x text-info mb-4"></i>
                        <h1 class="display-1">404</h1>
                        <h2>Página no encontrada</h2>
                        <p class="lead">La página que estás buscando no existe o ha sido movida.</p>
                        <hr>
                        <div class="mt-4">
                            <a href="~/" class="btn btn-primary" runat="server">
                                <i class="fas fa-home"></i> Ir al Inicio
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>