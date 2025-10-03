<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="frutas.Account.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center py-3">
                        <h4 class="mb-0"><i class="fas fa-user-edit"></i> Mi Perfil</h4>
                    </div>
                    <div class="card-body p-4">
                        <!-- Información del Usuario -->
                        <div class="mb-3">
                            <label for="txtUsername" class="form-label fw-bold">
                                <i class="fas fa-user text-primary"></i> Nombre de Usuario
                            </label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                                       ReadOnly="true" />
                            <small class="form-text text-muted">El nombre de usuario no se puede modificar</small>
                        </div>

                        <!-- Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label fw-bold">
                                <i class="fas fa-envelope text-primary"></i> Correo Electrónico *
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
                        <div class="mb-3">
                            <label for="txtNombreCompleto" class="form-label fw-bold">
                                <i class="fas fa-id-card text-primary"></i> Nombre Completo
                            </label>
                            <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" 
                                       placeholder="Ingrese su nombre completo" MaxLength="100" />
                        </div>

                        <!-- Información de la Cuenta -->
                        <div class="mb-4">
                            <label class="form-label fw-bold">
                                <i class="fas fa-info-circle text-primary"></i> Información de la Cuenta
                            </label>
                            <div class="card bg-light">
                                <div class="card-body">
                                    <div class="row g-3">
                                        <div class="col-sm-6">
                                            <strong>Rol:</strong> 
                                            <asp:Label ID="lblRol" runat="server" CssClass="badge bg-info" />
                                        </div>
                                        <div class="col-sm-6">
                                            <strong>Estado:</strong> 
                                            <span class="badge bg-success">Activo</span>
                                        </div>
                                    </div>
                                    <div class="row g-3 mt-2">
                                        <div class="col-sm-6">
                                            <strong>Cuenta creada:</strong><br />
                                            <small class="text-muted">
                                                <i class="fas fa-calendar-plus"></i>
                                                <asp:Label ID="lblFechaCreacion" runat="server" />
                                            </small>
                                        </div>
                                        <div class="col-sm-6">
                                            <strong>Último login:</strong><br />
                                            <small class="text-muted">
                                                <i class="fas fa-sign-in-alt"></i>
                                                <asp:Label ID="lblUltimoLogin" runat="server" />
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Mensaje de resultado -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="mb-3">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Botones -->
                        <div class="d-grid gap-2 d-md-flex justify-content-md-start">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
                                      CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                            <a href="ChangePassword.aspx" class="btn btn-outline-warning">
                                <i class="fas fa-key"></i> Cambiar Contraseña
                            </a>
                            <a href="~/Default.aspx" class="btn btn-outline-secondary" runat="server">
                                <i class="fas fa-arrow-left"></i> Volver
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Card de Seguridad -->
                <div class="card shadow mt-4">
                    <div class="card-body">
                        <h6 class="card-title text-muted">
                            <i class="fas fa-shield-alt"></i> Seguridad de la Cuenta
                        </h6>
                        <ul class="small text-muted mb-0">
                            <li>Mantén tu información de contacto actualizada</li>
                            <li>Cambia tu contraseña periódicamente</li>
                            <li>No compartas tus credenciales con nadie</li>
                            <li>Cierra sesión al terminar de usar el sistema</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>