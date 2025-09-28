-- ================================================
-- Script de Creación de Base de Datos
-- Sistema de Gestión de Frutas
-- .NET Framework 4.8
-- ================================================

USE master;
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FrutasDB')
BEGIN
    CREATE DATABASE FrutasDB;
    PRINT 'Base de datos FrutasDB creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos FrutasDB ya existe.';
END
GO

USE FrutasDB;
GO

-- ================================================
-- TABLA: Usuarios
-- ================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Usuarios] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Username] NVARCHAR(50) NOT NULL,
        [Email] NVARCHAR(100) NOT NULL,
        [PasswordHash] NVARCHAR(255) NOT NULL,
        [Salt] NVARCHAR(255) NOT NULL,
        [NombreCompleto] NVARCHAR(100) NULL,
        [Rol] NVARCHAR(20) NOT NULL DEFAULT 'Usuario',
        [FechaCreacion] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [FechaModificacion] DATETIME2 NULL,
        [UltimoLogin] DATETIME2 NULL,
        [IntentosFallidos] INT NOT NULL DEFAULT 0,
        [CuentaBloqueada] BIT NOT NULL DEFAULT 0,
        [FechaBloqueo] DATETIME2 NULL,
        [Activo] BIT NOT NULL DEFAULT 1,
        [UsuarioCreacion] INT NULL,
        [UsuarioModificacion] INT NULL,
        
        CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UQ_Usuarios_Username] UNIQUE NONCLUSTERED ([Username] ASC),
        CONSTRAINT [UQ_Usuarios_Email] UNIQUE NONCLUSTERED ([Email] ASC)
    );
    
    -- Índices adicionales
    CREATE NONCLUSTERED INDEX [IX_Usuarios_Activo] ON [dbo].[Usuarios] ([Activo] ASC);
    CREATE NONCLUSTERED INDEX [IX_Usuarios_Rol] ON [dbo].[Usuarios] ([Rol] ASC);
    CREATE NONCLUSTERED INDEX [IX_Usuarios_FechaCreacion] ON [dbo].[Usuarios] ([FechaCreacion] ASC);
    
    PRINT 'Tabla Usuarios creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Usuarios ya existe.';
END
GO

-- ================================================
-- TABLA: Frutas
-- ================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Frutas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Frutas] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Nombre] NVARCHAR(100) NOT NULL,
        [Descripcion] NVARCHAR(500) NULL,
        [Precio] DECIMAL(10,2) NOT NULL,
        [Stock] INT NOT NULL,
        [Categoria] NVARCHAR(50) NULL,
        [PaisOrigen] NVARCHAR(50) NULL,
        [Temporada] NVARCHAR(30) NULL,
        [EsOrganica] BIT NOT NULL DEFAULT 0,
        [FechaVencimiento] DATETIME2 NULL,
        [FechaCreacion] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [FechaModificacion] DATETIME2 NULL,
        [UsuarioCreacion] INT NULL,
        [UsuarioModificacion] INT NULL,
        [Activo] BIT NOT NULL DEFAULT 1,
        
        CONSTRAINT [PK_Frutas] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Frutas_UsuarioCreacion] FOREIGN KEY ([UsuarioCreacion]) REFERENCES [dbo].[Usuarios] ([Id]),
        CONSTRAINT [FK_Frutas_UsuarioModificacion] FOREIGN KEY ([UsuarioModificacion]) REFERENCES [dbo].[Usuarios] ([Id]),
        CONSTRAINT [CK_Frutas_Precio] CHECK ([Precio] > 0),
        CONSTRAINT [CK_Frutas_Stock] CHECK ([Stock] >= 0)
    );
    
    -- Índices adicionales
    CREATE NONCLUSTERED INDEX [IX_Frutas_Nombre] ON [dbo].[Frutas] ([Nombre] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_Categoria] ON [dbo].[Frutas] ([Categoria] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_Precio] ON [dbo].[Frutas] ([Precio] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_Stock] ON [dbo].[Frutas] ([Stock] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_Activo] ON [dbo].[Frutas] ([Activo] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_FechaCreacion] ON [dbo].[Frutas] ([FechaCreacion] ASC);
    CREATE NONCLUSTERED INDEX [IX_Frutas_EsOrganica] ON [dbo].[Frutas] ([EsOrganica] ASC);
    
    PRINT 'Tabla Frutas creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Frutas ya existe.';
END
GO

-- ================================================
-- TABLA: Logs
-- ================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logs]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Logs] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [UsuarioId] INT NULL,
        [Username] NVARCHAR(50) NULL,
        [Accion] NVARCHAR(100) NOT NULL,
        [Tabla] NVARCHAR(50) NOT NULL,
        [RegistroId] INT NULL,
        [DetalleAntes] NVARCHAR(MAX) NULL,
        [DetalleDepues] NVARCHAR(MAX) NULL,
        [Fecha] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [IP] NVARCHAR(45) NULL,
        [UserAgent] NVARCHAR(500) NULL,
        [Endpoint] NVARCHAR(500) NULL,
        [MetodoHttp] NVARCHAR(10) NULL,
        [TiempoEjecucion] BIGINT NULL,
        [Exitoso] BIT NOT NULL DEFAULT 1,
        [MensajeError] NVARCHAR(MAX) NULL,
        [Severidad] NVARCHAR(20) NOT NULL DEFAULT 'INFO',
        
        CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Logs_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
    );
    
    -- Índices adicionales
    CREATE NONCLUSTERED INDEX [IX_Logs_UsuarioId] ON [dbo].[Logs] ([UsuarioId] ASC);
    CREATE NONCLUSTERED INDEX [IX_Logs_Fecha] ON [dbo].[Logs] ([Fecha] DESC);
    CREATE NONCLUSTERED INDEX [IX_Logs_Accion] ON [dbo].[Logs] ([Accion] ASC);
    CREATE NONCLUSTERED INDEX [IX_Logs_Tabla] ON [dbo].[Logs] ([Tabla] ASC);
    CREATE NONCLUSTERED INDEX [IX_Logs_Severidad] ON [dbo].[Logs] ([Severidad] ASC);
    CREATE NONCLUSTERED INDEX [IX_Logs_Exitoso] ON [dbo].[Logs] ([Exitoso] ASC);
    
    PRINT 'Tabla Logs creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Logs ya existe.';
END
GO

-- ================================================
-- TABLA: ApiKeys
-- ================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApiKeys]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ApiKeys] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [KeyValue] NVARCHAR(255) NOT NULL,
        [UsuarioId] INT NOT NULL,
        [Nombre] NVARCHAR(100) NOT NULL,
        [Descripcion] NVARCHAR(500) NULL,
        [FechaCreacion] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [FechaExpiracion] DATETIME2 NULL,
        [Activo] BIT NOT NULL DEFAULT 1,
        [UltimoUso] DATETIME2 NULL,
        [ContadorUsos] INT NOT NULL DEFAULT 0,
        [LimitePorHora] INT NOT NULL DEFAULT 1000,
        [Permisos] NVARCHAR(50) NOT NULL DEFAULT 'READ',
        [IPsPermitidas] NVARCHAR(500) NULL,
        
        CONSTRAINT [PK_ApiKeys] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UQ_ApiKeys_KeyValue] UNIQUE NONCLUSTERED ([KeyValue] ASC),
        CONSTRAINT [FK_ApiKeys_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
    );
    
    -- Índices adicionales
    CREATE NONCLUSTERED INDEX [IX_ApiKeys_UsuarioId] ON [dbo].[ApiKeys] ([UsuarioId] ASC);
    CREATE NONCLUSTERED INDEX [IX_ApiKeys_Activo] ON [dbo].[ApiKeys] ([Activo] ASC);
    CREATE NONCLUSTERED INDEX [IX_ApiKeys_FechaExpiracion] ON [dbo].[ApiKeys] ([FechaExpiracion] ASC);
    
    PRINT 'Tabla ApiKeys creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla ApiKeys ya existe.';
END
GO

-- ================================================
-- DATOS INICIALES
-- ================================================

-- Insertar usuario administrador por defecto
IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [Username] = 'admin')
BEGIN
    INSERT INTO [dbo].[Usuarios] (
        [Username], 
        [Email], 
        [PasswordHash], 
        [Salt], 
        [NombreCompleto], 
        [Rol],
        [FechaCreacion],
        [Activo]
    ) VALUES (
        'admin',
        'admin@frutas.com',
        'YJLx4Vd7aK4h5z3xHqF8nvULdmUlA7gFoOW3Q7WnCKM=', -- Password: Admin123!
        'tQ7WlJZp4D8vX9mR2hL3nF6sK1pN5qY7uE4rT8oA6cB=',
        'Administrador del Sistema',
        'Administrador',
        GETDATE(),
        1
    );
    
    PRINT 'Usuario administrador creado exitosamente.';
    PRINT 'Username: admin';
    PRINT 'Password: Admin123!';
END
ELSE
BEGIN
    PRINT 'El usuario administrador ya existe.';
END
GO

-- Insertar datos de ejemplo para frutas
IF NOT EXISTS (SELECT 1 FROM [dbo].[Frutas])
BEGIN
    DECLARE @AdminId INT = (SELECT TOP 1 Id FROM [dbo].[Usuarios] WHERE Username = 'admin');
    
    INSERT INTO [dbo].[Frutas] (
        [Nombre], [Descripcion], [Precio], [Stock], [Categoria], 
        [PaisOrigen], [Temporada], [EsOrganica], [UsuarioCreacion], [Activo]
    ) VALUES 
    ('Manzana Roja', 'Manzanas rojas frescas y jugosas', 2.50, 100, 'Frutas de Pepita', 'Argentina', 'Otoño', 0, @AdminId, 1),
    ('Banana', 'Bananas amarillas maduras y dulces', 1.80, 150, 'Frutas Tropicales', 'Ecuador', 'Todo el año', 1, @AdminId, 1),
    ('Naranja', 'Naranjas jugosas ricas en vitamina C', 3.00, 80, 'Cítricos', 'España', 'Invierno', 0, @AdminId, 1),
    ('Frutilla', 'Frutillas rojas y aromáticas', 4.50, 50, 'Frutas de Primavera', 'Chile', 'Primavera', 1, @AdminId, 1),
    ('Pera', 'Peras verdes y crujientes', 2.80, 75, 'Frutas de Pepita', 'Argentina', 'Verano', 0, @AdminId, 1),
    ('Kiwi', 'Kiwis verdes ricos en vitamina C', 5.20, 40, 'Frutas Exóticas', 'Nueva Zelanda', 'Invierno', 1, @AdminId, 1),
    ('Uva', 'Uvas moradas dulces sin semillas', 6.80, 60, 'Frutas de Vid', 'Chile', 'Verano', 0, @AdminId, 1),
    ('Piña', 'Piñas tropicales jugosas y dulces', 8.50, 25, 'Frutas Tropicales', 'Costa Rica', 'Todo el año', 0, @AdminId, 1);
    
    PRINT 'Datos de ejemplo insertados exitosamente.';
END
ELSE
BEGIN
    PRINT 'Ya existen datos en la tabla Frutas.';
END
GO

-- ================================================
-- PROCEDIMIENTOS ALMACENADOS
-- ================================================

-- Procedimiento para obtener estadísticas generales
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetEstadisticasGenerales]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_GetEstadisticasGenerales];
GO

CREATE PROCEDURE [dbo].[sp_GetEstadisticasGenerales]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        'Usuarios Activos' as Metrica,
        COUNT(*) as Valor
    FROM Usuarios 
    WHERE Activo = 1
    
    UNION ALL
    
    SELECT 
        'Total Frutas' as Metrica,
        COUNT(*) as Valor
    FROM Frutas 
    WHERE Activo = 1
    
    UNION ALL
    
    SELECT 
        'Stock Total' as Metrica,
        SUM(Stock) as Valor
    FROM Frutas 
    WHERE Activo = 1
    
    UNION ALL
    
    SELECT 
        'Valor Inventario' as Metrica,
        CAST(SUM(Precio * Stock) as INT) as Valor
    FROM Frutas 
    WHERE Activo = 1
    
    UNION ALL
    
    SELECT 
        'Frutas Orgánicas' as Metrica,
        COUNT(*) as Valor
    FROM Frutas 
    WHERE Activo = 1 AND EsOrganica = 1
    
    UNION ALL
    
    SELECT 
        'Logs Hoy' as Metrica,
        COUNT(*) as Valor
    FROM Logs 
    WHERE CAST(Fecha as DATE) = CAST(GETDATE() as DATE);
END
GO

-- Procedimiento para limpiar logs antiguos
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LimpiarLogsAntiguos]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_LimpiarLogsAntiguos];
GO

CREATE PROCEDURE [dbo].[sp_LimpiarLogsAntiguos]
    @DiasAMantener INT = 90
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @FechaLimite DATETIME2 = DATEADD(DAY, -@DiasAMantener, GETDATE());
    DECLARE @RegistrosEliminados INT;
    
    DELETE FROM Logs 
    WHERE Fecha < @FechaLimite 
    AND Severidad NOT IN ('ERROR', 'CRITICAL');
    
    SET @RegistrosEliminados = @@ROWCOUNT;
    
    SELECT 
        @RegistrosEliminados as RegistrosEliminados,
        @FechaLimite as FechaLimite;
END
GO

PRINT '================================================';
PRINT 'Base de datos FrutasDB configurada exitosamente.';
PRINT '================================================';
PRINT 'Tablas creadas: Usuarios, Frutas, Logs, ApiKeys';
PRINT 'Usuario admin creado con password: Admin123!';
PRINT 'Datos de ejemplo insertados en tabla Frutas';
PRINT 'Procedimientos almacenados creados';
PRINT '================================================';