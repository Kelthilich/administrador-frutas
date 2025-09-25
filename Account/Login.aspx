<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="frutas.Account.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow">
                    <div class="card-header bg-success text-white text-center">
                        <h4><i class="fas fa-sign-in-alt"></i> Iniciar Sesión</h4>
                    </div>
                    <div class="card-body">
                        <!-- Username -->
                        <div class="form-group">
                            <label for="txtUsername">
                                <i class="fas fa-user"></i> Usuario
                            </label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                                       placeholder="Ingrese su nombre de usuario" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                                                      ControlToValidate="txtUsername"
                                                      ErrorMessage="El nombre de usuario es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Password -->
                        <div class="form-group">
                            <label for="txtPassword">
                                <i class="fas fa-lock"></i> Contraseña
                            </label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Ingrese su contraseña" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                                                      ControlToValidate="txtPassword"
                                                      ErrorMessage="La contraseña es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Remember Me -->
                        <div class="form-group form-check">
                            <asp:CheckBox ID="chkRecordar" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkRecordar">
                                Recordarme
                            </label>
                        </div>

                        <!-- Error Message -->
                        <asp:Panel ID="pnlError" runat="server" Visible="false" 
                                 CssClass="alert alert-danger">
                            <i class="fas fa-exclamation-triangle"></i>
                            <asp:Label ID="lblError" runat="server" />
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="form-group">
                            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" 
                                      CssClass="btn btn-success btn-block" OnClick="btnLogin_Click" />
                        </div>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-2">
                                <a href="ForgotPassword.aspx" class="text-muted">
                                    <i class="fas fa-key"></i> ¿Olvidaste tu contraseña?
                                </a>
                            </p>
                            <p class="mb-0">
                                ¿No tienes cuenta? 
                                <a href="Register.aspx" class="text-success">
                                    <i class="fas fa-user-plus"></i> Regístrate aquí
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Info Card -->
                <div class="card mt-4">
                    <div class="card-body text-center">
                        <h6 class="card-title text-muted">
                            <i class="fas fa-info-circle"></i> Información del Sistema
                        </h6>
                        <p class="card-text small text-muted">
                            Sistema de Gestión de Frutas<br />
                            Desarrollado con .NET Framework 4.8<br />
                            <strong>Usuario Demo:</strong> admin / Admin123!
                        </p>
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
                if (e.which === 13) { // Enter key
                    $('#<%= btnLogin.ClientID %>').click();
                    return false;
                }
            });

            // Hide error panel after 5 seconds
            setTimeout(function () {
                $('#<%= pnlError.ClientID %>').fadeOut();
            }, 5000);
        });
    </script>
</asp:Content>