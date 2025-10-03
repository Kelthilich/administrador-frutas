<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="frutas.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-12">
                <!-- Hero Section -->
                <div class="text-center mb-5">
                    <i class="fas fa-envelope fa-4x text-primary mb-3"></i>
                    <h1 class="display-4 fw-bold">Contáctanos</h1>
                    <p class="lead text-muted">Estamos aquí para ayudarte</p>
                </div>
            </div>
        </div>

        <div class="row g-4">
            <!-- Información de Contacto -->
            <div class="col-lg-6">
                <div class="card shadow h-100">
                    <div class="card-header bg-primary text-white">
                        <h5 class="mb-0"><i class="fas fa-info-circle"></i> Información de Contacto</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-4">
                            <div class="d-flex align-items-start mb-3">
                                <i class="fas fa-map-marker-alt fa-2x text-primary me-3"></i>
                                <div>
                                    <h6 class="fw-bold mb-1">Dirección</h6>
                                    <p class="text-muted mb-0">
                                        Calle Principal #123<br />
                                        Ciudad, País<br />
                                        Código Postal 12345
                                    </p>
                                </div>
                            </div>

                            <div class="d-flex align-items-start mb-3">
                                <i class="fas fa-phone fa-2x text-success me-3"></i>
                                <div>
                                    <h6 class="fw-bold mb-1">Teléfono</h6>
                                    <p class="text-muted mb-0">
                                        <a href="tel:+1234567890" class="text-decoration-none">+1 (234) 567-890</a>
                                    </p>
                                </div>
                            </div>

                            <div class="d-flex align-items-start mb-3">
                                <i class="fas fa-envelope fa-2x text-info me-3"></i>
                                <div>
                                    <h6 class="fw-bold mb-1">Email</h6>
                                    <p class="text-muted mb-0">
                                        <strong>Soporte:</strong> <a href="mailto:soporte@frutas.com">soporte@frutas.com</a><br />
                                        <strong>Ventas:</strong> <a href="mailto:ventas@frutas.com">ventas@frutas.com</a><br />
                                        <strong>General:</strong> <a href="mailto:info@frutas.com">info@frutas.com</a>
                                    </p>
                                </div>
                            </div>

                            <div class="d-flex align-items-start">
                                <i class="fas fa-clock fa-2x text-warning me-3"></i>
                                <div>
                                    <h6 class="fw-bold mb-1">Horario de Atención</h6>
                                    <p class="text-muted mb-0">
                                        Lunes a Viernes: 9:00 AM - 6:00 PM<br />
                                        Sábados: 9:00 AM - 1:00 PM<br />
                                        Domingos: Cerrado
                                    </p>
                                </div>
                            </div>
                        </div>

                        <!-- Redes Sociales -->
                        <div class="mt-4 pt-4 border-top">
                            <h6 class="fw-bold mb-3">Síguenos en Redes Sociales</h6>
                            <div class="d-flex gap-2">
                                <a href="#" class="btn btn-outline-primary btn-sm">
                                    <i class="fab fa-facebook-f"></i>
                                </a>
                                <a href="#" class="btn btn-outline-info btn-sm">
                                    <i class="fab fa-twitter"></i>
                                </a>
                                <a href="#" class="btn btn-outline-danger btn-sm">
                                    <i class="fab fa-instagram"></i>
                                </a>
                                <a href="#" class="btn btn-outline-success btn-sm">
                                    <i class="fab fa-whatsapp"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Formulario de Contacto -->
            <div class="col-lg-6">
                <div class="card shadow h-100">
                    <div class="card-header bg-success text-white">
                        <h5 class="mb-0"><i class="fas fa-paper-plane"></i> Envíanos un Mensaje</h5>
                    </div>
                    <div class="card-body">
                        <form>
                            <div class="mb-3">
                                <label for="txtNombre" class="form-label fw-bold">Nombre Completo</label>
                                <input type="text" class="form-control" id="txtNombre" placeholder="Tu nombre">
                            </div>

                            <div class="mb-3">
                                <label for="txtEmail" class="form-label fw-bold">Email</label>
                                <input type="email" class="form-control" id="txtEmail" placeholder="tu@email.com">
                            </div>

                            <div class="mb-3">
                                <label for="txtAsunto" class="form-label fw-bold">Asunto</label>
                                <input type="text" class="form-control" id="txtAsunto" placeholder="¿En qué podemos ayudarte?">
                            </div>

                            <div class="mb-3">
                                <label for="txtMensaje" class="form-label fw-bold">Mensaje</label>
                                <textarea class="form-control" id="txtMensaje" rows="5" placeholder="Escribe tu mensaje aquí..."></textarea>
                            </div>

                            <div class="d-grid">
                                <button type="submit" class="btn btn-success btn-lg">
                                    <i class="fas fa-paper-plane"></i> Enviar Mensaje
                                </button>
                            </div>
                        </form>

                        <div class="alert alert-info mt-4 mb-0">
                            <small>
                                <i class="fas fa-info-circle"></i> 
                                Nos comprometemos a responder tu mensaje en un plazo máximo de 24 horas hábiles.
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Mapa o Información Adicional -->
        <div class="row mt-4">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-body bg-light text-center p-5">
                        <i class="fas fa-map-marked-alt fa-3x text-muted mb-3"></i>
                        <h5 class="fw-bold">¿Necesitas más información?</h5>
                        <p class="text-muted mb-3">
                            Visita nuestra página de <a href="~/About.aspx" runat="server">Acerca de</a> para conocer más sobre el sistema
                            o regresa al <a href="~/" runat="server">Dashboard</a> para empezar a usar la aplicación.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
