<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="frutas.Account.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center">
                        <h4><i class="fas fa-user-plus"></i> Crear Nueva Cuenta</h4>
                    </div>
                    <div class="card-body">
                        <!-- Username -->
                        <div class="form-group">
                            <label for="txtUsername">
                                <i class="fas fa-user"></i> Nombre de Usuario *
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
                        <div class="form-group">
                            <label for="txtEmail">
                                <i class="fas fa-envelope"></i> Correo Electrónico *
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
                        <div class="form-group">
                            <label for="txtNombreCompleto">
                                <i class="fas fa-id-card"></i> Nombre Completo
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
                        <div class="form-group">
                            <label for="txtPassword">
                                <i class="fas fa-lock"></i> Contraseña *
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
                        <div class="form-group">
                            <label for="txtConfirmarPassword">
                                <i class="fas fa-lock"></i> Confirmar Contraseña *
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
                        <div class="form-group form-check">
                            <asp:CheckBox ID="chkTerminos" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkTerminos">
                                Acepto los <a href="#" data-toggle="modal" data-target="#modalTerminos">términos y condiciones</a> *
                            </label>
                            <asp:CustomValidator ID="cvTerminos" runat="server"
                                               ErrorMessage="Debe aceptar los términos y condiciones"
                                               CssClass="text-danger small" Display="Dynamic"
                                               ClientValidationFunction="validarTerminos" />
                        </div>

                        <!-- Error/Success Messages -->
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                            <asp:Label ID="lblMessage" runat="server" />
                        </asp:Panel>

                        <!-- Submit Button -->
                        <div class="form-group">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Crear Cuenta" 
                                      CssClass="btn btn-primary btn-block" OnClick="btnRegistrar_Click" />
                        </div>

                        <!-- Links -->
                        <div class="text-center">
                            <p class="mb-0">
                                ¿Ya tienes cuenta? 
                                <a href="Login.aspx" class="text-primary">
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
    <div class="modal fade" id="modalTerminos" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Términos y Condiciones</h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h6>1. Aceptación de Términos</h6>
                    <p>Al registrarse en el Sistema de Gestión de Frutas, usted acepta cumplir con estos términos y condiciones.</p>
                    
                    <h6>2. Uso del Sistema</h6>
                    <p>Este sistema está diseñado para la gestión de inventario de frutas. El uso debe ser responsable y conforme a las políticas de la organización.</p>
                    
                    <h6>3. Privacidad</h6>
                    <p>Su información personal será protegida y utilizada únicamente para los propósitos del sistema.</p>
                    
                    <h6>4. Responsabilidades del Usuario</h6>
                    <p>Usted es responsable de mantener la confidencialidad de su cuenta y contraseña.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
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
                    // Aquí podrías hacer una llamada AJAX para verificar disponibilidad
                    // Por ahora solo mostramos feedback visual
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