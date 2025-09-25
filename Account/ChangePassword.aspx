<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="frutas.Account.ChangePassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow">
                    <div class="card-header bg-warning text-dark text-center">
                        <h4><i class="fas fa-key"></i> Cambiar Contraseña</h4>
                    </div>
                    <div class="card-body">
                        <!-- Contraseña Actual -->
                        <div class="form-group">
                            <label for="txtPasswordActual">
                                <i class="fas fa-lock"></i> Contraseña Actual *
                            </label>
                            <asp:TextBox ID="txtPasswordActual" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Ingrese su contraseña actual" />
                            <asp:RequiredFieldValidator ID="rfvPasswordActual" runat="server" 
                                                      ControlToValidate="txtPasswordActual"
                                                      ErrorMessage="La contraseña actual es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Nueva Contraseña -->
                        <div class="form-group">
                            <label for="txtPasswordNueva">
                                <i class="fas fa-lock"></i> Nueva Contraseña *
                            </label>
                            <asp:TextBox ID="txtPasswordNueva" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Ingrese la nueva contraseña" />
                            <small class="form-text text-muted">
                                Mínimo 8 caracteres. Debe incluir: mayúscula, minúscula, número y carácter especial.
                            </small>
                            <asp:RequiredFieldValidator ID="rfvPasswordNueva" runat="server" 
                                                      ControlToValidate="txtPasswordNueva"
                                                      ErrorMessage="La nueva contraseña es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revPasswordNueva" runat="server"
                                                          ControlToValidate="txtPasswordNueva"
                                                          ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                                                          ErrorMessage="La contraseña debe tener al menos 8 caracteres, incluyendo mayúscula, minúscula, número y carácter especial"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Confirmar Nueva Contraseña -->
                        <div class="form-group">
                            <label for="txtPasswordConfirmar">
                                <i class="fas fa-lock"></i> Confirmar Nueva Contraseña *
                            </label>
                            <asp:TextBox ID="txtPasswordConfirmar" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Confirme la nueva contraseña" />
                            <asp:RequiredFieldValidator ID="rfvPasswordConfirmar" runat="server" 
                                                      ControlToValidate="txtPasswordConfirmar"
                                                      ErrorMessage="Debe confirmar la nueva contraseña"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:CompareValidator ID="cvPassword" runat="server"
                                                ControlToValidate="txtPasswordConfirmar"
                                                ControlToCompare="txtPasswordNueva"
                                                ErrorMessage="Las contraseñas no coinciden"
                                                CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Mensaje de resultado -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Botones -->
                        <div class="form-group">
                            <asp:Button ID="btnCambiar" runat="server" Text="Cambiar Contraseña" 
                                      CssClass="btn btn-warning btn-block" OnClick="btnCambiar_Click" />
                        </div>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-0">
                                <a href="Profile.aspx" class="text-muted">
                                    <i class="fas fa-user"></i> Volver al Perfil
                                </a>
                                |
                                <a href="~/Default.aspx" class="text-muted">
                                    <i class="fas fa-home"></i> Dashboard
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Consejos de Seguridad -->
                <div class="card mt-4">
                    <div class="card-body">
                        <h6 class="card-title text-muted">
                            <i class="fas fa-shield-alt"></i> Consejos de Seguridad
                        </h6>
                        <ul class="small text-muted">
                            <li>Use una contraseña única que no haya usado en otros sitios</li>
                            <li>Incluya al menos 8 caracteres con mayúsculas, minúsculas, números y símbolos</li>
                            <li>No comparta su contraseña con nadie</li>
                            <li>Cambie su contraseña periódicamente</li>
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
            // Focus en el primer campo
            $('#<%= txtPasswordActual.ClientID %>').focus();

            // Validación de fortaleza de contraseña en tiempo real
            $('#<%= txtPasswordNueva.ClientID %>').keyup(function () {
                var password = $(this).val();
                var strength = checkPasswordStrength(password);
                showPasswordStrength(strength);
            });
        });

        // Función para verificar fortaleza de contraseña
        function checkPasswordStrength(password) {
            var strength = 0;
            if (password.length >= 8) strength++;
            if (/[a-z]/.test(password)) strength++;
            if (/[A-Z]/.test(password)) strength++;
            if (/[0-9]/.test(password)) strength++;
            if (/[^a-zA-Z0-9]/.test(password)) strength++;
            return strength;
        }

        // Mostrar indicador de fortaleza de contraseña
        function showPasswordStrength(strength) {
            var $password = $('#<%= txtPasswordNueva.ClientID %>');
            var $feedback = $('#password-strength');
            
            if ($feedback.length === 0) {
                $feedback = $('<div id="password-strength" class="small mt-1"></div>');
                $password.after($feedback);
            }

            $password.removeClass('is-invalid is-valid');
            
            if (strength < 3) {
                $feedback.html('<span class="text-danger">Contraseña débil</span>');
                $password.addClass('is-invalid');
            } else if (strength < 5) {
                $feedback.html('<span class="text-warning">Contraseña regular</span>');
            } else {
                $feedback.html('<span class="text-success">Contraseña fuerte</span>');
                $password.addClass('is-valid');
            }
        }
    </script>
</asp:Content>