<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="frutas.Account.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center py-3">
                        <h4 class="mb-0"><i class="fas fa-user-plus"></i> Crear Nueva Cuenta</h4>
                    </div>
                    <div class="card-body p-4">
                        <!-- Username -->
                        <div class="mb-3">
                            <label for="txtUsername" class="form-label fw-bold">
                                <i class="fas fa-user text-primary"></i> Nombre de Usuario *
                            </label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                                       placeholder="Ingrese un nombre de usuario único" MaxLength="50" />
                            <small class="form-text text-muted">
                                3-50 caracteres. Solo letras, números, puntos, guiones y guiones bajos.
                            </small>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                                                      ControlToValidate="txtUsername"
                                                      ErrorMessage="El nombre de usuario es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revUsername" runat="server"
                                                          ControlToValidate="txtUsername"
                                                          ValidationExpression="^[a-zA-Z0-9._-]{3,50}$"
                                                          ErrorMessage="Formato de nombre de usuario inválido"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label fw-bold">
                                <i class="fas fa-envelope text-primary"></i> Correo Electrónico *
                            </label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                                       TextMode="Email" placeholder="ejemplo@correo.com" MaxLength="100" />
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
                                       placeholder="Ingrese su nombre completo (opcional)" MaxLength="100" />
                            <asp:RegularExpressionValidator ID="revNombreCompleto" runat="server"
                                                          ControlToValidate="txtNombreCompleto"
                                                          ValidationExpression="^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ\s\-'\.]*$"
                                                          ErrorMessage="El nombre contiene caracteres no válidos"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Password -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label fw-bold">
                                <i class="fas fa-lock text-primary"></i> Contraseña *
                            </label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Ingrese una contraseña segura" />
                            <small class="form-text text-muted">
                                Mínimo 8 caracteres. Debe incluir: mayúscula, minúscula, número y carácter especial.
                            </small>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                                                      ControlToValidate="txtPassword"
                                                      ErrorMessage="La contraseña es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Confirm Password -->
                        <div class="mb-3">
                            <label for="txtConfirmarPassword" class="form-label fw-bold">
                                <i class="fas fa-lock text-primary"></i> Confirmar Contraseña *
                            </label>
                            <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Confirme su contraseña" />
                            <asp:RequiredFieldValidator ID="rfvConfirmarPassword" runat="server" 
                                                      ControlToValidate="txtConfirmarPassword"
                                                      ErrorMessage="Debe confirmar la contraseña"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:CompareValidator ID="cvPassword" runat="server"
                                                ControlToValidate="txtConfirmarPassword"
                                                ControlToCompare="txtPassword"
                                                ErrorMessage="Las contraseñas no coinciden"
                                                CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Terms -->
                        <div class="mb-3">
                            <div class="form-check">
                                <asp:CheckBox ID="chkTerminos" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label" for="<%= chkTerminos.ClientID %>">
                                    Acepto los <a href="#" data-bs-toggle="modal" data-bs-target="#modalTerminos" class="text-decoration-none">términos y condiciones</a> *
                                </label>
                            </div>
                            <asp:CustomValidator ID="cvTerminos" runat="server"
                                               ErrorMessage="Debe aceptar los términos y condiciones"
                                               CssClass="text-danger small d-block" Display="Dynamic"
                                               ClientValidationFunction="validarTerminos" />
                        </div>

                        <!-- Error/Success Messages -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Crear Cuenta" 
                                      CssClass="btn btn-primary btn-lg" OnClick="btnRegistrar_Click" />
                        </div>

                        <hr>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-0">
                                ¿Ya tienes cuenta? 
                                <a href="Login.aspx" class="text-primary text-decoration-none fw-bold">
                                    <i class="fas fa-sign-in-alt"></i> Iniciar sesión aquí
                                </a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de Términos y Condiciones -->
    <div class="modal fade" id="modalTerminos" tabindex="-1" aria-labelledby="modalTerminosLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="modalTerminosLabel">
                        <i class="fas fa-file-contract"></i> Términos y Condiciones
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h6 class="fw-bold">1. Aceptación de Términos</h6>
                    <p>Al registrarse en el Sistema de Gestión de Frutas, usted acepta cumplir con estos términos y condiciones.</p>
                    
                    <h6 class="fw-bold">2. Uso del Sistema</h6>
                    <p>Este sistema está diseñado para la gestión de inventario de frutas. El uso debe ser responsable y conforme a las políticas de la organización.</p>
                    
                    <h6 class="fw-bold">3. Privacidad</h6>
                    <p>Su información personal será protegida y utilizada únicamente para los propósitos del sistema.</p>
                    
                    <h6 class="fw-bold">4. Responsabilidades del Usuario</h6>
                    <p>Usted es responsable de mantener la confidencialidad de su cuenta y contraseña.</p>

                    <h6 class="fw-bold">5. Modificaciones</h6>
                    <p>Nos reservamos el derecho de modificar estos términos en cualquier momento.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="fas fa-times"></i> Cerrar
                    </button>
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

            // Validación de disponibilidad de username (simulada)
            $('#<%= txtUsername.ClientID %>').blur(function () {
                var username = $(this).val();
                if (username.length >= 3) {
                    $(this).removeClass('is-invalid is-valid').addClass('is-valid');
                }
            });

            // Validación de fortaleza de contraseña en tiempo real
            $('#<%= txtPassword.ClientID %>').keyup(function () {
                var password = $(this).val();
                var strength = checkPasswordStrength(password);
                showPasswordStrength(strength);
            });
        });

        // Validación personalizada para términos y condiciones
        function validarTerminos(source, args) {
            args.IsValid = $('#<%= chkTerminos.ClientID %>').is(':checked');
        }

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
            var $password = $('#<%= txtPassword.ClientID %>');
            var $feedback = $('#password-strength');
            
            if ($feedback.length === 0) {
                $feedback = $('<div id="password-strength" class="small mt-1"></div>');
                $password.after($feedback);
            }

            $password.removeClass('is-invalid is-valid');
            
            if (strength < 3) {
                $feedback.html('<span class="text-danger"><i class="fas fa-times-circle"></i> Contraseña débil</span>');
                $password.addClass('is-invalid');
            } else if (strength < 5) {
                $feedback.html('<span class="text-warning"><i class="fas fa-exclamation-circle"></i> Contraseña regular</span>');
            } else {
                $feedback.html('<span class="text-success"><i class="fas fa-check-circle"></i> Contraseña fuerte</span>');
                $password.addClass('is-valid');
            }
        }
    </script>
</asp:Content>