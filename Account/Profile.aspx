<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="frutas.Account.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center">
                        <h4><i class="fas fa-user-edit"></i> Mi Perfil</h4>
                    </div>
                    <div class="card-body">
                        <!-- Información del Usuario -->
                        <div class="form-group">
                            <label for="txtUsername">
                                <i class="fas fa-user"></i> Nombre de Usuario
                            </label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                                       ReadOnly="true" />
                            <small class="form-text text-muted">El nombre de usuario no se puede modificar</small>
                        </div>

                        <!-- Email -->
                        <div class="form-group">
                            <label for="txtEmail">
                                <i class="fas fa-envelope"></i> Correo Electrónico *
                            </label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                                       TextMode="Email" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                                                      ControlToValidate="txtEmail"
                                                      ErrorMessage="El email es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                                          ControlToValidate="txtEmail"
                                                          ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                                          ErrorMessage="Formato de email inválido"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Nombre Completo -->
                        <div class="form-group">
                            <label for="txtNombreCompleto">
                                <i class="fas fa-id-card"></i> Nombre Completo
                            </label>
                            <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" 
                                       MaxLength="100" />
                        </div>

                        <!-- Información de la Cuenta -->
                        <div class="form-group">
                            <label>
                                <i class="fas fa-info-circle"></i> Información de la Cuenta
                            </label>
                            <div class="card bg-light">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <strong>Rol:</strong> <asp:Label ID="lblRol" runat="server" />
                                        </div>
                                        <div class="col-sm-6">
                                            <strong>Estado:</strong> <span class="badge badge-success">Activo</span>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-sm-6">
                                            <strong>Cuenta creada:</strong><br />
                                            <small><asp:Label ID="lblFechaCreacion" runat="server" /></small>
                                        </div>
                                        <div class="col-sm-6">
                                            <strong>Último login:</strong><br />
                                            <small><asp:Label ID="lblUltimoLogin" runat="server" /></small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Mensaje de resultado -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Botones -->
                        <div class="form-group">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
                                      CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                            <a href="ChangePassword.aspx" class="btn btn-outline-warning ml-2">
                                <i class="fas fa-key"></i> Cambiar Contraseña
                            </a>
                            <a href="~/Default.aspx" class="btn btn-outline-secondary ml-2">
                                <i class="fas fa-arrow-left"></i> Volver al Dashboard
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>