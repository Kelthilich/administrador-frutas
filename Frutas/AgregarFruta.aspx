<%@ Page Title="Agregar Fruta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarFruta.aspx.cs" Inherits="frutas.Frutas.AgregarFruta" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <!-- Header -->
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2 class="fw-bold"><i class="fas fa-plus-circle text-success"></i> Agregar Nueva Fruta</h2>
                        <p class="text-muted">Complete el formulario para agregar una nueva fruta al inventario</p>
                    </div>
                    <div>
                        <a href="ListaFrutas.aspx" class="btn btn-outline-secondary">
                            <i class="fas fa-arrow-left"></i> Volver a la Lista
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">
                    <div class="card-header bg-success text-white py-3">
                        <h5 class="mb-0"><i class="fas fa-apple-alt"></i> Información de la Fruta</h5>
                    </div>
                    <div class="card-body p-4">
                        <div class="row">
                            <!-- Información Básica -->
                            <div class="col-md-6">
                                <h6 class="text-primary mb-3 fw-bold"><i class="fas fa-info-circle"></i> Información Básica</h6>
                                
                                <!-- Nombre -->
                                <div class="mb-3">
                                    <label for="txtNombre" class="form-label fw-bold">
                                        <i class="fas fa-apple-alt text-success"></i> Nombre de la Fruta *
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
                                <div class="mb-3">
                                    <label for="txtDescripcion" class="form-label fw-bold">
                                        <i class="fas fa-align-left text-success"></i> Descripción
                                    </label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"
                                               CssClass="form-control" placeholder="Descripción detallada de la fruta..." 
                                               MaxLength="500" />
                                    <small class="form-text text-muted">Máximo 500 caracteres</small>
                                </div>

                                <!-- Categoría -->
                                <div class="mb-3">
                                    <label for="ddlCategoria" class="form-label fw-bold">
                                        <i class="fas fa-tags text-success"></i> Categoría *
                                    </label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
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
                                        <button type="button" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#modalCategoria">
                                            <i class="fas fa-plus"></i>
                                        </button>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" 
                                                              ControlToValidate="ddlCategoria"
                                                              ErrorMessage="Debe seleccionar una categoría"
                                                              CssClass="text-danger small" Display="Dynamic" />
                                </div>

                                <!-- País de Origen -->
                                <div class="mb-3">
                                    <label for="ddlPaisOrigen" class="form-label fw-bold">
                                        <i class="fas fa-globe text-success"></i> País de Origen
                                    </label>
                                    <asp:DropDownList ID="ddlPaisOrigen" runat="server" CssClass="form-select">
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
                                <h6 class="text-primary mb-3 fw-bold"><i class="fas fa-dollar-sign"></i> Información Comercial</h6>
                                
                                <!-- Precio -->
                                <div class="mb-3">
                                    <label for="txtPrecio" class="form-label fw-bold">
                                        <i class="fas fa-dollar-sign text-success"></i> Precio por Unidad *
                                    </label>
                                    <div class="input-group">
                                        <span class="input-group-text">$</span>
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
                                <div class="mb-3">
                                    <label for="txtStock" class="form-label fw-bold">
                                        <i class="fas fa-boxes text-success"></i> Stock Inicial *
                                    </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control text-end" 
                                                   placeholder="0" TextMode="Number" min="0" />
                                        <span class="input-group-text">unidades</span>
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
                                <div class="mb-3">
                                    <label for="ddlTemporada" class="form-label fw-bold">
                                        <i class="fas fa-calendar-alt text-success"></i> Temporada
                                    </label>
                                    <asp:DropDownList ID="ddlTemporada" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="-- Seleccionar Temporada --" />
                                        <asp:ListItem Value="Primavera" Text="Primavera" />
                                        <asp:ListItem Value="Verano" Text="Verano" />
                                        <asp:ListItem Value="Otoño" Text="Otoño" />
                                        <asp:ListItem Value="Invierno" Text="Invierno" />
                                        <asp:ListItem Value="Todo el año" Text="Todo el año" />
                                    </asp:DropDownList>
                                </div>

                                <!-- Fecha de Vencimiento -->
                                <div class="mb-3">
                                    <label for="txtFechaVencimiento" class="form-label fw-bold">
                                        <i class="fas fa-calendar-times text-success"></i> Fecha de Vencimiento
                                    </label>
                                    <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" 
                                               TextMode="Date" />
                                    <small class="form-text text-muted">Opcional - Solo si la fruta tiene fecha de vencimiento específica</small>
                                    <asp:CompareValidator ID="cvFechaVencimiento" runat="server"
                                                        ControlToValidate="txtFechaVencimiento"
                                                        Operator="GreaterThan" Type="Date"
                                                        ErrorMessage="La fecha de vencimiento debe ser futura"
                                                        CssClass="text-danger small" Display="Dynamic" />
                                </div>

                                <!-- Es Orgánica -->
                                <div class="mb-3">
                                    <div class="form-check">
                                        <asp:CheckBox ID="chkEsOrganica" runat="server" CssClass="form-check-input" />
                                        <label class="form-check-label" for="<%= chkEsOrganica.ClientID %>">
                                            <i class="fas fa-leaf text-success"></i> Es Fruta Orgánica
                                        </label>
                                    </div>
                                    <small class="form-text text-muted">Marque si la fruta es de producción orgánica certificada</small>
                                </div>
                            </div>
                        </div>

                        <!-- Vista Previa -->
                        <div class="row mt-4">
                            <div class="col-12">
                                <div class="alert alert-info">
                                    <h6 class="fw-bold"><i class="fas fa-eye"></i> Vista Previa</h6>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <strong id="previewNombre">Nombre de la Fruta</strong>
                                            <br>
                                            <span class="text-muted" id="previewDescripcion">Descripción de la fruta</span>
                                            <br>
                                            <span class="badge bg-secondary" id="previewCategoria">Categoría</span>
                                            <span class="badge bg-info" id="previewPais">País</span>
                                            <span class="badge bg-success" id="previewOrganica" style="display:none;"><i class="fas fa-leaf"></i> Orgánica</span>
                                        </div>
                                        <div class="col-md-4 text-end">
                                            <h5 class="text-success mb-1">$<span id="previewPrecio">0.00</span></h5>
                                            <small class="text-muted">Stock: <span id="previewStock">0</span> unidades</small>
                                            <br>
                                            <small class="text-muted" id="previewTemporada">Temporada</small>
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
                                    <a href="ListaFrutas.aspx" class="btn btn-secondary">
                                        <i class="fas fa-times"></i> Cancelar
                                    </a>
                                    <div>
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Fruta" 
                                                  CssClass="btn btn-success btn-lg" OnClick="btnGuardar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para Nueva Categoría -->
    <div class="modal fade" id="modalCategoria" tabindex="-1" aria-labelledby="modalCategoriaLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="modalCategoriaLabel">
                        <i class="fas fa-plus-circle"></i> Agregar Nueva Categoría
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtNuevaCategoria" class="form-label fw-bold">Nombre de la Categoría:</label>
                        <input type="text" id="txtNuevaCategoria" class="form-control" 
                               placeholder="Ej: Frutas de Verano" maxlength="50" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="fas fa-times"></i> Cancelar
                    </button>
                    <button type="button" class="btn btn-success" onclick="agregarCategoria()">
                        <i class="fas fa-check"></i> Agregar
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptsContent" runat="server">
    <style>
        .input-group-text {
            background-color: #e9ecef;
            border-color: #ced4da;
        }
        
        .alert-info {
            background-color: #f8f9fa;
            border-color: #dee2e6;
            color: #495057;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            // Configurar fecha mínima para vencimiento
            var today = new Date().toISOString().split('T')[0];
            $('#<%= txtFechaVencimiento.ClientID %>').attr('min', today);

            // Vista previa en tiempo real
            $('#<%= txtNombre.ClientID %>').on('input', function() {
                $('#previewNombre').text($(this).val() || 'Nombre de la Fruta');
            });

            $('#<%= txtDescripcion.ClientID %>').on('input', function() {
                $('#previewDescripcion').text($(this).val() || 'Descripción de la fruta');
            });

            $('#<%= ddlCategoria.ClientID %>').on('change', function() {
                $('#previewCategoria').text($(this).val() || 'Categoría');
            });

            $('#<%= ddlPaisOrigen.ClientID %>').on('change', function() {
                $('#previewPais').text($(this).val() || 'País');
            });

            $('#<%= txtPrecio.ClientID %>').on('input', function() {
                var precio = parseFloat($(this).val()) || 0;
                $('#previewPrecio').text(precio.toFixed(2));
            });

            $('#<%= txtStock.ClientID %>').on('input', function() {
                $('#previewStock').text($(this).val() || '0');
            });

            $('#<%= ddlTemporada.ClientID %>').on('change', function() {
                $('#previewTemporada').text($(this).val() || 'Temporada');
            });

            $('#<%= chkEsOrganica.ClientID %>').on('change', function() {
                if ($(this).is(':checked')) {
                    $('#previewOrganica').show();
                } else {
                    $('#previewOrganica').hide();
                }
            });

            // Calculadora de valor total
            $('#<%= txtPrecio.ClientID %>, #<%= txtStock.ClientID %>').on('input', function() {
                var precio = parseFloat($('#<%= txtPrecio.ClientID %>').val()) || 0;
                var stock = parseInt($('#<%= txtStock.ClientID %>').val()) || 0;
                var total = precio * stock;
                
                if (total > 0) {
                    if (!$('#valorTotal').length) {
                        $('#previewStock').parent().after('<br><small class="text-success fw-bold">Valor total: $<span id="valorTotal">' + total.toFixed(2) + '</span></small>');
                    } else {
                        $('#valorTotal').text(total.toFixed(2));
                    }
                }
            });
        });

        // Función para agregar nueva categoría
        function agregarCategoria() {
            var nuevaCategoria = $('#txtNuevaCategoria').val().trim();
            
            if (nuevaCategoria === '') {
                alert('Por favor ingrese el nombre de la categoría');
                return;
            }

            // Verificar si ya existe
            var existe = false;
            $('#<%= ddlCategoria.ClientID %> option').each(function() {
                if ($(this).text().toLowerCase() === nuevaCategoria.toLowerCase()) {
                    existe = true;
                    return false;
                }
            });

            if (existe) {
                alert('Esta categoría ya existe');
                return;
            }

            // Agregar nueva opción
            $('#<%= ddlCategoria.ClientID %>').append(new Option(nuevaCategoria, nuevaCategoria));
            $('#<%= ddlCategoria.ClientID %>').val(nuevaCategoria);
            $('#previewCategoria').text(nuevaCategoria);

            // Cerrar modal y limpiar
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalCategoria'));
            modal.hide();
            $('#txtNuevaCategoria').val('');
        }
    </script>
</asp:Content>