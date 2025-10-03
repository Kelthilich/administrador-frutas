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
                                       placeholder="Ingrese un nombre de usuario �nico" MaxLength="50" />
                            <small class="form-text text-muted">
                                3-50 caracteres. Solo letras, n�meros, puntos, guiones y guiones bajos.
                            </small>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                                                      ControlToValidate="txtUsername"
                                                      ErrorMessage="El nombre de usuario es requerido"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revUsername" runat="server"
                                                          ControlToValidate="txtUsername"
                                                          ValidationExpression="^[a-zA-Z0-9._-]{3,50}$"
                                                          ErrorMessage="Formato de nombre de usuario inv�lido"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label fw-bold">
                                <i class="fas fa-envelope text-primary"></i> Correo Electr�nico *
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
                                                          ErrorMessage="Formato de email inv�lido"
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
                                                          ValidationExpression="^[a-zA-Z��������������\s\-'\.]*$"
                                                          ErrorMessage="El nombre contiene caracteres no v�lidos"
                                                          CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Password -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label fw-bold">
                                <i class="fas fa-lock text-primary"></i> Contrase�a *
                            </label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Ingrese una contrase�a segura" />
                            <small class="form-text text-muted">
                                M�nimo 8 caracteres. Debe incluir: may�scula, min�scula, n�mero y car�cter especial.
                            </small>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                                                      ControlToValidate="txtPassword"
                                                      ErrorMessage="La contrase�a es requerida"
                                                      CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Confirm Password -->
                        <div class="mb-3">
                            <label for="txtConfirmarPassword" class="form-label fw-bold">
                                <i class="fas fa-lock text-primary"></i> Confirmar Contrase�a *
                            </label>
                            <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" 
                                       CssClass="form-control" placeholder="Confirme su contrase�a" />
                            <asp:RequiredFieldValidator ID="rfvConfirmarPassword" runat="server" 
                                                      ControlToValidate="txtConfirmarPassword"
                                                      ErrorMessage="Debe confirmar la contrase�a"
                                                      CssClass="text-danger small" Display="Dynamic" />
                            <asp:CompareValidator ID="cvPassword" runat="server"
                                                ControlToValidate="txtConfirmarPassword"
                                                ControlToCompare="txtPassword"
                                                ErrorMessage="Las contrase�as no coinciden"
                                                CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- Terms -->
                        <div class="mb-3">
                            <div class="form-check">
                                <asp:CheckBox ID="chkTerminos" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label" for="<%= chkTerminos.ClientID %>">
                                    Acepto los <a href="#" data-bs-toggle="modal" data-bs-target="#modalTerminos" class="text-decoration-none">t�rminos y condiciones</a> *
                                </label>
                            </div>
                            <asp:CustomValidator ID="cvTerminos" runat="server"
                                               ErrorMessage="Debe aceptar los t�rminos y condiciones"
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
                                �Ya tienes cuenta? 
                                <a href="Login.aspx" class="text-primary text-decoration-none fw-bold">
                                    <i class="fas fa-sign-in-alt"></i> Iniciar sesi�n aqu�
                                </a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de T�rminos y Condiciones -->
    <div class="modal fade" id="modalTerminos" tabindex="-1" aria-labelledby="modalTerminosLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="modalTerminosLabel">
                        <i class="fas fa-file-contract"></i> T�rminos y Condiciones
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h6 class="fw-bold">1. Aceptaci�n de T�rminos</h6>
                    <p>Al registrarse en el Sistema de Gesti�n de Frutas, usted acepta cumplir con estos t�rminos y condiciones.</p>
                    
                    <h6 class="fw-bold">2. Uso del Sistema</h6>
                    <p>Este sistema est� dise�ado para la gesti�n de inventario de frutas. El uso debe ser responsable y conforme a las pol�ticas de la organizaci�n.</p>
                    
                    <h6 class="fw-bold">3. Privacidad</h6>
                    <p>Su informaci�n personal ser� protegida y utilizada �nicamente para los prop�sitos del sistema.</p>
                    
                    <h6 class="fw-bold">4. Responsabilidades del Usuario</h6>
                    <p>Usted es responsable de mantener la confidencialidad de su cuenta y contrase�a.</p>

                    <h6 class="fw-bold">5. Modificaciones</h6>
                    <p>Nos reservamos el derecho de modificar estos t�rminos en cualquier momento.</p>
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

            // Validaci�n de disponibilidad de username (simulada)
            $('#<%= txtUsername.ClientID %>').blur(function () {
                var username = $(this).val();
                if (username.length >= 3) {
                    $(this).removeClass('is-invalid is-valid').addClass('is-valid');
                }
            });

            // Validaci�n de fortaleza de contrase�a en tiempo real
            $('#<%= txtPassword.ClientID %>').keyup(function () {
                var password = $(this).val();
                var strength = checkPasswordStrength(password);
                showPasswordStrength(strength);
            });
        });

        // Validaci�n personalizada para t�rminos y condiciones
        function validarTerminos(source, args) {
            args.IsValid = $('#<%= chkTerminos.ClientID %>').is(':checked');
        }

        // Funci�n para verificar fortaleza de contrase�a
        function checkPasswordStrength(password) {
            var strength = 0;
            if (password.length >= 8) strength++;
            if (/[a-z]/.test(password)) strength++;
            if (/[A-Z]/.test(password)) strength++;
            if (/[0-9]/.test(password)) strength++;
            if (/[^a-zA-Z0-9]/.test(password)) strength++;
            return strength;
        }

        // Mostrar indicador de fortaleza de contrase�a
        function showPasswordStrength(strength) {
            var $password = $('#<%= txtPassword.ClientID %>');
            var $feedback = $('#password-strength');
            
            if ($feedback.length === 0) {
                $feedback = $('<div id="password-strength" class="small mt-1"></div>');
                $password.after($feedback);
            }

            $password.removeClass('is-invalid is-valid');
            
            if (strength < 3) {
                $feedback.html('<span class="text-danger"><i class="fas fa-times-circle"></i> Contrase�a d�bil</span>');
                $password.addClass('is-invalid');
            } else if (strength < 5) {
                $feedback.html('<span class="text-warning"><i class="fas fa-exclamation-circle"></i> Contrase�a regular</span>');
            } else {
                $feedback.html('<span class="text-success"><i class="fas fa-check-circle"></i> Contrase�a fuerte</span>');
                $password.addClass('is-valid');
            }
        }
    </script>
</asp:Content>