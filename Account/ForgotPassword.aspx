<%@ Page Title="Recuperar Contrase�a" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="frutas.Account.ForgotPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow">
                    <div class="card-header bg-info text-white text-center py-4">
                        <i class="fas fa-key fa-3x mb-2"></i>
                        <h4 class="mb-0">Recuperar Contrase�a</h4>
                    </div>
                    <div class="card-body p-4">
                        <p class="text-muted mb-4">
                            <i class="fas fa-info-circle"></i>
                            Ingrese su nombre de usuario o correo electr�nico para recuperar su contrase�a.
                        </p>

                        <!-- Username o Email -->
                        <div class="mb-3">
                            <label for="txtUsernameEmail" class="form-label fw-bold">
                                <i class="fas fa-user text-info"></i> Usuario o Email
                            </label>
                            <asp:TextBox ID="txtUsernameEmail" runat="server" CssClass="form-control form-control-lg" 
                                       placeholder="Ingrese su usuario o correo electr�nico" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="rfvUsernameEmail" runat="server" 
                                                      ControlToValidate="txtUsernameEmail"
                                                      ErrorMessage="Este campo es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Mensaje de resultado -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="mb-3">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnRecuperar" runat="server" Text="Enviar Instrucciones" 
                                      CssClass="btn btn-info btn-lg" OnClick="btnRecuperar_Click" />
                        </div>

                        <hr>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-0">
                                �Recordaste tu contrase�a? 
                                <a href="Login.aspx" class="text-info text-decoration-none fw-bold">
                                    <i class="fas fa-sign-in-alt"></i> Iniciar sesi�n
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Info Card -->
                <div class="card shadow mt-4">
                    <div class="card-body">
                        <div class="alert alert-warning mb-0">
                            <h6 class="alert-heading">
                                <i class="fas fa-exclamation-triangle"></i> Funcionalidad Simulada
                            </h6>
                            <p class="small mb-0">
                                Esta es una versi�n demo. En un sistema real, se enviar�a un correo electr�nico 
                                con instrucciones para restablecer la contrase�a.
                            </p>
                        </div>
                    </div>
                </div>

                <!-- �Qu� hacer despu�s? -->
                <div class="card shadow mt-4">
                    <div class="card-body">
                        <h6 class="card-title text-muted fw-bold">
                            <i class="fas fa-question-circle"></i> �Qu� suceder� despu�s?
                        </h6>
                        <ul class="small text-muted mb-0">
                            <li>Recibir�s un correo electr�nico con instrucciones</li>
                            <li>El enlace ser� v�lido por 24 horas</li>
                            <li>Podr�s crear una nueva contrase�a segura</li>
                            <li>Si no recibes el correo, verifica tu carpeta de spam</li>
                        </ul>
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