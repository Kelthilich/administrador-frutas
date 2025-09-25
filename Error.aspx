<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6 text-center">
                <div class="card shadow">
                    <div class="card-body">
                        <i class="fas fa-exclamation-triangle fa-5x text-warning mb-4"></i>
                        <h2>¡Oops! Algo salió mal</h2>
                        <p class="lead">Ha ocurrido un error inesperado en la aplicación.</p>
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