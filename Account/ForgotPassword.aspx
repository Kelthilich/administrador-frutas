<%@ Page Title="Recuperar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="frutas.Account.ForgotPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow">
                    <div class="card-header bg-info text-white text-center">
                        <h4><i class="fas fa-key"></i> Recuperar Contraseña</h4>
                    </div>
                    <div class="card-body">
                        <p class="text-muted mb-4">
                            Ingrese su nombre de usuario o correo electrónico para recuperar su contraseña.
                        </p>

                        <!-- Username o Email -->
                        <div class="form-group">
                            <label for="txtUsernameEmail">
                                <i class="fas fa-user"></i> Usuario o Email
                            </label>
                            <asp:TextBox ID="txtUsernameEmail" runat="server" CssClass="form-control" 
                                       placeholder="Ingrese su usuario o correo electrónico" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="rfvUsernameEmail" runat="server" 
                                                      ControlToValidate="txtUsernameEmail"
                                                      ErrorMessage="Este campo es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Mensaje de resultado -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="form-group">
                            <asp:Button ID="btnRecuperar" runat="server" Text="Enviar Instrucciones" 
                                      CssClass="btn btn-info btn-block" OnClick="btnRecuperar_Click" />
                        </div>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-0">
                                ¿Recordaste tu contraseña? 
                                <a href="Login.aspx" class="text-info">
                                    <i class="fas fa-sign-in-alt"></i> Iniciar sesión
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Info Card -->
                <div class="card mt-4">
                    <div class="card-body text-center">
                        <div class="alert alert-warning">
                            <h6><i class="fas fa-exclamation-triangle"></i> Funcionalidad Simulada</h6>
                            <p class="small mb-0">
                                Esta es una versión demo. En un sistema real, se enviaría un correo electrónico 
                                con instrucciones para restablecer la contraseña.
                            </p>
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
            // Focus en el campo al cargar
            $('#<%= txtUsernameEmail.ClientID %>').focus();

            // Enter key handling
            $('#<%= txtUsernameEmail.ClientID %>').keypress(function (e) {
                if (e.which === 13) { // Enter key
                    $('#<%= btnRecuperar.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>