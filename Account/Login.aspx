<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="frutas.Account.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow-lg">
                    <div class="card-header bg-success text-white text-center py-4">
                        <i class="fas fa-sign-in-alt fa-3x mb-2"></i>
                        <h4 class="mb-0">Iniciar Sesión</h4>
                    </div>
                    <div class="card-body p-4">
                        <!-- Username -->
                        <div class="mb-3">
                            <label for="txtUsername" class="form-label fw-bold">
                                <i class="fas fa-user text-success"></i> Usuario
                            </label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg" 
                                       placeholder="Ingrese su nombre de usuario" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                                                      ControlToValidate="txtUsername"
                                                      ErrorMessage="El nombre de usuario es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Password -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label fw-bold">
                                <i class="fas fa-lock text-success"></i> Contraseña
                            </label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control form-control-lg" placeholder="Ingrese su contraseña" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                                                      ControlToValidate="txtPassword"
                                                      ErrorMessage="La contraseña es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Remember Me -->
                        <div class="mb-3 form-check">
                            <asp:CheckBox ID="chkRecordar" runat="server" 
                                        CssClass="form-check-input" 
                                        Text="Recordarme en este dispositivo" />
                        </div>

                        <!-- Error Message -->
                        <asp:Panel ID="pnlError" runat="server" Visible="false" 
                                 CssClass="alert alert-danger alert-dismissible fade show">
                            <i class="fas fa-exclamation-triangle"></i>
                            <asp:Label ID="lblError" runat="server" />
                            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" 
                                      CssClass="btn btn-success btn-lg" OnClick="btnLogin_Click" />
                        </div>

                        <hr>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-2">
                                <a href="ForgotPassword.aspx" class="text-decoration-none">
                                    <i class="fas fa-key"></i> ¿Olvidaste tu contraseña?
                                </a>
                            </p>
                            <p class="mb-0">
                                ¿No tienes cuenta? 
                                <a href="Register.aspx" class="text-success text-decoration-none fw-bold">
                                    <i class="fas fa-user-plus"></i> Regístrate aquí
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Info Card -->
                <div class="card mt-4 shadow">
                    <div class="card-body text-center">
                        <h6 class="card-title text-muted mb-3">
                            <i class="fas fa-info-circle"></i> Información del Sistema
                        </h6>
                        <p class="card-text small text-muted mb-2">
                            <strong>Sistema de Gestión de Frutas</strong><br />
                            Desarrollado con .NET Framework 4.8
                        </p>
                        <div class="alert alert-info mb-0">
                            <strong>Usuario Demo:</strong> admin<br />
                            <strong>Contraseña:</strong> Admin123!
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Focus en el campo username al cargar
            $('#<%= txtUsername.ClientID %>').focus();

            // Enter key handling
            $('#<%= txtUsername.ClientID %>, #<%= txtPassword.ClientID %>').keypress(function (e) {
                if (e.which === 13) {
                    $('#<%= btnLogin.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>