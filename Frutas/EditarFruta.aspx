<%@ Page Title="Editar Fruta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarFruta.aspx.cs" Inherits="frutas.Frutas.EditarFruta" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <!-- Header -->
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2><i class="fas fa-edit text-primary"></i> Editar Fruta</h2>
                        <p class="text-muted">Modifique los datos de la fruta seleccionada</p>
                        <asp:Panel ID="pnlFrutaInfo" runat="server" Visible="false">
                            <small class="text-muted">
                                ID: <asp:Label ID="lblFrutaId" runat="server" />
                                | Creado por: <asp:Label ID="lblUsuarioCreador" runat="server" />
                                | Fecha: <asp:Label ID="lblFechaCreacion" runat="server" />
                            </small>
                        </asp:Panel>
                    </div>
                    <div>
                        <a href="ListaFrutas.aspx" class="btn btn-outline-secondary">
                            <i class="fas fa-arrow-left"></i> Volver a la Lista
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Mensaje de Carga -->
        <asp:Panel ID="pnlCargando" runat="server" Visible="true" CssClass="text-center py-5">
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Cargando...</span>
            </div>
            <p class="mt-3 text-muted">Cargando información de la fruta...</p>
        </asp:Panel>

        <!-- Mensaje de Error -->
        <asp:Panel ID="pnlError" runat="server" Visible="false">
            <div class="alert alert-danger">
                <h5><i class="fas fa-exclamation-triangle"></i> Error</h5>
                <asp:Label ID="lblMensajeError" runat="server" />
                <hr>
                <a href="ListaFrutas.aspx" class="btn btn-outline-danger">
                    <i class="fas fa-arrow-left"></i> Volver a la Lista
                </a>
            </div>
        </asp:Panel>

        <!-- Formulario Principal -->
        <asp:Panel ID="pnlFormulario" runat="server" Visible="false">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="card shadow">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0">
                                <i class="fas fa-apple-alt"></i> 
                                Información de la Fruta: <asp:Label ID="lblNombreFruta" runat="server" />
                            </h5>
                        </div>
                        <div class="card-body">
                            <!-- Alerta de Cambios -->
                            <asp:Panel ID="pnlCambiosDetectados" runat="server" Visible="false" CssClass="alert alert-warning">
                                <h6><i class="fas fa-exclamation-triangle"></i> Cambios Detectados</h6>
                                <p class="mb-0">Esta fruta ha sido modificada por otro usuario. Los cambios más recientes se han cargado automáticamente.</p>
                            </asp:Panel>

                            <div class="row">
                                <!-- Información Básica -->
                                <div class="col-md-6">
                                    <h6 class="text-primary mb-3"><i class="fas fa-info-circle"></i> Información Básica</h6>
                                    
                                    <!-- Nombre -->
                                    <div class="form-group">
                                        <label for="txtNombre">
                                            <i class="fas fa-apple-alt"></i> Nombre de la Fruta *
                                        </label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                                                   placeholder="Ej: Manzana Roja, Banana Premium..." MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                                                  ControlToValidate="txtNombre"
                                                                  ErrorMessage="El nombre es requerido"
                                                                  CssClass="text-danger small" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revNombre" runat="server"
                                                                      ControlToValidate="txtNombre"
                                                                      ValidationExpression="^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ\s\-'\.()]+$"
                                                                      ErrorMessage="El nombre contiene caracteres no válidos"
                                                                      CssClass="text-danger small" Display="Dynamic" />
                                    </div>

                                    <!-- Descripción -->
                                    <div class="form-group">
                                        <label for="txtDescripcion">
                                            <i class="fas fa-align-left"></i> Descripción
                                        </label>
                                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"
                                                   CssClass="form-control" placeholder="Descripción detallada de la fruta..." 
                                                   MaxLength="500" />
                                        <small class="form-text text-muted">Máximo 500 caracteres</small>
                                    </div>

                                    <!-- Categoría -->
                                    <div class="form-group">
                                        <label for="ddlCategoria">
                                            <i class="fas fa-tags"></i> Categoría *
                                        </label>
                                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="-- Seleccionar Categoría --" />
                                            <asp:ListItem Value="Frutas de Pepita" Text="Frutas de Pepita" />
                                            <asp:ListItem Value="Frutas Tropicales" Text="Frutas Tropicales" />
                                            <asp:ListItem Value="Cítricos" Text="Cítricos" />
                                            <asp:ListItem Value="Frutas de Primavera" Text="Frutas de Primavera" />
                                            <asp:ListItem Value="Frutas Exóticas" Text="Frutas Exóticas" />
                                            <asp:ListItem Value="Frutas de Vid" Text="Frutas de Vid" />
                                            <asp:ListItem Value="Frutos Secos" Text="Frutos Secos" />
                                            <asp:ListItem Value="Otra" Text="Otra" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" 
                                                                  ControlToValidate="ddlCategoria"
                                                                  ErrorMessage="Debe seleccionar una categoría"
                                                                  CssClass="text-danger small" Display="Dynamic" />
                                    </div>

                                    <!-- País de Origen -->
                                    <div class="form-group">
                                        <label for="ddlPaisOrigen">
                                            <i class="fas fa-globe"></i> País de Origen
                                        </label>
                                        <asp:DropDownList ID="ddlPaisOrigen" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="-- Seleccionar País --" />
                                            <asp:ListItem Value="Argentina" Text="Argentina" />
                                            <asp:ListItem Value="Brasil" Text="Brasil" />
                                            <asp:ListItem Value="Chile" Text="Chile" />
                                            <asp:ListItem Value="Colombia" Text="Colombia" />
                                            <asp:ListItem Value="Costa Rica" Text="Costa Rica" />
                                            <asp:ListItem Value="Ecuador" Text="Ecuador" />
                                            <asp:ListItem Value="España" Text="España" />
                                            <asp:ListItem Value="México" Text="México" />
                                            <asp:ListItem Value="Nueva Zelanda" Text="Nueva Zelanda" />
                                            <asp:ListItem Value="Perú" Text="Perú" />
                                            <asp:ListItem Value="Uruguay" Text="Uruguay" />
                                            <asp:ListItem Value="Otro" Text="Otro" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <!-- Información Comercial -->
                                <div class="col-md-6">
                                    <h6 class="text-primary mb-3"><i class="fas fa-dollar-sign"></i> Información Comercial</h6>
                                    
                                    <!-- Precio -->
                                    <div class="form-group">
                                        <label for="txtPrecio">
                                            <i class="fas fa-dollar-sign"></i> Precio por Unidad *
                                        </label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">$</span>
                                            </div>
                                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control text-end" 
                                                       placeholder="0.00" TextMode="Number" step="0.01" min="0.01" />
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                                  ControlToValidate="txtPrecio"
                                                                  ErrorMessage="El precio es requerido"
                                                                  CssClass="text-danger small" Display="Dynamic" />
                                        <asp:RangeValidator ID="rvPrecio" runat="server"
                                                          ControlToValidate="txtPrecio"
                                                          MinimumValue="0.01" MaximumValue="999999.99" Type="Double"
                                                          ErrorMessage="El precio debe estar entre $0.01 y $999,999.99"
                                                          CssClass="text-danger small" Display="Dynamic" />
                                    </div>

                                    <!-- Stock -->
                                    <div class="form-group">
                                        <label for="txtStock">
                                            <i class="fas fa-boxes"></i> Stock Actual *
                                        </label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control text-end" 
                                                       placeholder="0" TextMode="Number" min="0" />
                                            <div class="input-group-append">
                                                <span class="input-group-text">unidades</span>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvStock" runat="server" 
                                                                  ControlToValidate="txtStock"
                                                                  ErrorMessage="El stock es requerido"
                                                                  CssClass="text-danger small" Display="Dynamic" />
                                        <asp:RangeValidator ID="rvStock" runat="server"
                                                          ControlToValidate="txtStock"
                                                          MinimumValue="0" MaximumValue="999999" Type="Integer"
                                                          ErrorMessage="El stock debe estar entre 0 y 999,999"
                                                          CssClass="text-danger small" Display="Dynamic" />
                                    </div>

                                    <!-- Temporada -->
                                    <div class="form-group">
                                        <label for="ddlTemporada">
                                            <i class="fas fa-calendar-alt"></i> Temporada
                                        </label>
                                        <asp:DropDownList ID="ddlTemporada" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="-- Seleccionar Temporada --" />
                                            <asp:ListItem Value="Primavera" Text="Primavera" />
                                            <asp:ListItem Value="Verano" Text="Verano" />
                                            <asp:ListItem Value="Otoño" Text="Otoño" />
                                            <asp:ListItem Value="Invierno" Text="Invierno" />
                                            <asp:ListItem Value="Todo el año" Text="Todo el año" />
                                        </asp:DropDownList>
                                    </div>

                                    <!-- Fecha de Vencimiento -->
                                    <div class="form-group">
                                        <label for="txtFechaVencimiento">
                                            <i class="fas fa-calendar-times"></i> Fecha de Vencimiento
                                        </label>
                                        <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" 
                                                   TextMode="Date" />
                                        <small class="form-text text-muted">Opcional - Solo si la fruta tiene fecha de vencimiento específica</small>
                                    </div>

                                    <!-- Es Orgánica -->
                                    <div class="form-group">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkEsOrganica" runat="server" CssClass="form-check-input" />
                                            <label class="form-check-label" for="chkEsOrganica">
                                                <i class="fas fa-leaf text-success"></i> Es Fruta Orgánica
                                            </label>
                                        </div>
                                        <small class="form-text text-muted">Marque si la fruta es de producción orgánica certificada</small>
                                    </div>
                                </div>
                            </div>

                            <!-- Información de Auditoría -->
                            <div class="row mt-4">
                                <div class="col-12">
                                    <div class="alert alert-light">
                                        <h6><i class="fas fa-history"></i> Información de Auditoría</h6>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <small class="text-muted">
                                                    <strong>Creado por:</strong> <asp:Label ID="lblCreadoPor" runat="server" /><br/>
                                                    <strong>Fecha de creación:</strong> <asp:Label ID="lblFechaCreacion2" runat="server" />
                                                </small>
                                            </div>
                                            <div class="col-md-6">
                                                <small class="text-muted">
                                                    <strong>Última modificación:</strong> <asp:Label ID="lblUltimaModificacion" runat="server" /><br/>
                                                    <strong>Modificado por:</strong> <asp:Label ID="lblModificadoPor" runat="server" />
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Botones -->
                            <div class="row">
                                <div class="col-12">
                                    <hr>
                                    <div class="d-flex justify-content-between">
                                        <div>
                                            <a href="ListaFrutas.aspx" class="btn btn-secondary">
                                                <i class="fas fa-times"></i> Cancelar
                                            </a>
                                            <asp:Button ID="btnRecargar" runat="server" Text="Recargar" 
                                                      CssClass="btn btn-outline-info ms-2" OnClick="btnRecargar_Click" 
                                                      ToolTip="Recargar datos originales" />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" 
                                                      CssClass="btn btn-primary btn-lg" OnClick="btnGuardar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .form-group label {
            font-weight: 600;
            color: #495057;
        }
        
        .required {
            color: #dc3545;
        }
        
        .input-group-text {
            background-color: #e9ecef;
            border-color: #ced4da;
        }
        
        .alert-light {
            background-color: #f8f9fa;
            border-color: #dee2e6;
            color: #495057;
        }
        
        .spinner-border {
            width: 3rem;
            height: 3rem;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Configurar fecha mínima para vencimiento (solo fechas futuras)
            var today = new Date().toISOString().split('T')[0];
            $('#<%= txtFechaVencimiento.ClientID %>').attr('min', today);

            // Detectar cambios en el formulario
            var formChanged = false;
            $('input, select, textarea').on('change input', function() {
                formChanged = true;
                $(this).addClass('changed');
            });

            // Advertir antes de salir si hay cambios no guardados
            $(window).on('beforeunload', function() {
                if (formChanged) {
                    return '¿Está seguro de que desea salir? Los cambios no guardados se perderán.';
                }
            });

            // No mostrar advertencia al enviar el formulario
            $('form').on('submit', function() {
                formChanged = false;
            });

            // Validación en tiempo real del precio
            $('#<%= txtPrecio.ClientID %>').on('input', function() {
                var precio = parseFloat($(this).val());
                if (precio < 0.01) {
                    $(this).addClass('is-invalid');
                } else {
                    $(this).removeClass('is-invalid').addClass('is-valid');
                }
            });

            // Calculadora de valor total en tiempo real
            $('#<%= txtPrecio.ClientID %>, #<%= txtStock.ClientID %>').on('input', function() {
                var precio = parseFloat($('#<%= txtPrecio.ClientID %>').val()) || 0;
                var stock = parseInt($('#<%= txtStock.ClientID %>').val()) || 0;
                var total = precio * stock;
                
                // Mostrar valor total
                if (!$('#valorTotal').length && total > 0) {
                    $('#<%= txtStock.ClientID %>').closest('.input-group').after(
                        '<small class="form-text text-success">Valor total del inventario: $<span id="valorTotal">' + total.toFixed(2) + '</span></small>'
                    );
                } else if ($('#valorTotal').length) {
                    $('#valorTotal').text(total.toFixed(2));
                }
            });

            // Triggar el cálculo inicial
            $('#<%= txtPrecio.ClientID %>').trigger('input');
        });

        // Validación antes de enviar
        function validarFormulario() {
            var valido = true;
            var errores = [];

            // Validar campos requeridos
            if ($('#<%= txtNombre.ClientID %>').val().trim() === '') {
                errores.push('El nombre es requerido');
                valido = false;
            }

            if ($('#<%= ddlCategoria.ClientID %>').val() === '') {
                errores.push('La categoría es requerida');
                valido = false;
            }

            var precio = parseFloat($('#<%= txtPrecio.ClientID %>').val());
            if (isNaN(precio) || precio <= 0) {
                errores.push('El precio debe ser mayor a 0');
                valido = false;
            }

            var stock = parseInt($('#<%= txtStock.ClientID %>').val());
            if (isNaN(stock) || stock < 0) {
                errores.push('El stock debe ser 0 o mayor');
                valido = false;
            }

            // Validar fecha de vencimiento si está presente
            var fechaVencimiento = $('#<%= txtFechaVencimiento.ClientID %>').val();
            if (fechaVencimiento) {
                var fecha = new Date(fechaVencimiento);
                var hoy = new Date();
                hoy.setHours(0, 0, 0, 0);
                
                if (fecha <= hoy) {
                    errores.push('La fecha de vencimiento debe ser futura');
                    valido = false;
                }
            }

            if (!valido) {
                alert('Por favor corrija los siguientes errores:\n\n' + errores.join('\n'));
                return false;
            }

            // Confirmar guardado
            return confirm('¿Está seguro de que desea guardar los cambios?');
        }
    </script>
</asp:Content>