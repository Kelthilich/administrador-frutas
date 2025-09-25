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
                        <div class="form-group">
                            <label for="txtEmail">
                                <i class="fas fa-envelope"></i> Correo Electr�nico *
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
                        <div class="form-group">
                            <label for="txtNombreCompleto">
                                <i class="fas fa-id-card"></i> Nombre Completo
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
                        <div class="form-group">
                            <label for="txtPassword">
                                <i class="fas fa-lock"></i> Contrase�a *
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
                        <div class="form-group">
                            <label for="txtConfirmarPassword">
                                <i class="fas fa-lock"></i> Confirmar Contrase�a *
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
                        <div class="form-group form-check">
                            <asp:CheckBox ID="chkTerminos" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkTerminos">
                                Acepto los <a href="#" data-toggle="modal" data-target="#modalTerminos">t�rminos y condiciones</a> *
                            </label>
                            <asp:CustomValidator ID="cvTerminos" runat="server"
                                               ErrorMessage="Debe aceptar los t�rminos y condiciones"
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
                                �Ya tienes cuenta? 
                                <a href="Login.aspx" class="text-primary">
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
    <div class="modal fade" id="modalTerminos" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">T�rminos y Condiciones</h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h6>1. Aceptaci�n de T�rminos</h6>
                    <p>Al registrarse en el Sistema de Gesti�n de Frutas, usted acepta cumplir con estos t�rminos y condiciones.</p>
                    
                    <h6>2. Uso del Sistema</h6>
                    <p>Este sistema est� dise�ado para la gesti�n de inventario de frutas. El uso debe ser responsable y conforme a las pol�ticas de la organizaci�n.</p>
                    
                    <h6>3. Privacidad</h6>
                    <p>Su informaci�n personal ser� protegida y utilizada �nicamente para los prop�sitos del sistema.</p>
                    
                    <h6>4. Responsabilidades del Usuario</h6>
                    <p>Usted es responsable de mantener la confidencialidad de su cuenta y contrase�a.</p>
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

            // Validaci�n de disponibilidad de username (simulada)
            $('#<%= txtUsername.ClientID %>').blur(function () {
                var username = $(this).val();
                if (username.length >= 3) {
                    // Aqu� podr�as hacer una llamada AJAX para verificar disponibilidad
                    // Por ahora solo mostramos feedback visual
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
                $feedback.html('<span class="text-danger">Contrase�a d�bil</span>');
                $password.addClass('is-invalid');
            } else if (strength < 5) {
                $feedback.html('<span class="text-warning">Contrase�a regular</span>');
            } else {
                $feedback.html('<span class="text-success">Contrase�a fuerte</span>');
                $password.addClass('is-valid');
            }
        }
    </script>
</asp:Content>