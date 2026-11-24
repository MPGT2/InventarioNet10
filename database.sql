-- ============================================================
-- InventarioNet10 - Script de base de datos (SQL Server)
-- Crea la base, la tabla Productos, indices y datos de ejemplo.
-- ============================================================

IF DB_ID('InventarioNet10') IS NULL
BEGIN
    CREATE DATABASE InventarioNet10;
END
GO

USE InventarioNet10;
GO

IF OBJECT_ID('dbo.Productos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Productos
    (
        Id            INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Productos PRIMARY KEY,
        Nombre        NVARCHAR(100)     NOT NULL,
        Categoria     NVARCHAR(60)      NOT NULL,
        Precio        DECIMAL(18,2)     NOT NULL,
        Stock         INT               NOT NULL,
        FechaRegistro DATETIME2         NOT NULL CONSTRAINT DF_Productos_Fecha DEFAULT (SYSDATETIME())
    );

    CREATE INDEX IX_Productos_Nombre ON dbo.Productos (Nombre);
    CREATE INDEX IX_Productos_Categoria ON dbo.Productos (Categoria);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Productos)
BEGIN
    INSERT INTO dbo.Productos (Nombre, Categoria, Precio, Stock, FechaRegistro) VALUES
    (N'Laptop Lenovo IdeaPad 3',        N'Computadoras',   549.99, 12, DATEADD(DAY, -12, SYSDATETIME())),
    (N'Monitor LG 24" Full HD',         N'Monitores',      129.50, 18, DATEADD(DAY, -10, SYSDATETIME())),
    (N'Teclado mecánico Redragon Kumara', N'Accesorios',    39.99, 35, DATEADD(DAY,  -8, SYSDATETIME())),
    (N'Mouse Logitech M170',            N'Accesorios',      14.75, 60, DATEADD(DAY,  -6, SYSDATETIME())),
    (N'Disco SSD Kingston 480 GB',      N'Almacenamiento',  44.90, 22, DATEADD(DAY,  -5, SYSDATETIME())),
    (N'Router TP-Link Archer C6',       N'Redes',           34.99, 15, DATEADD(DAY,  -4, SYSDATETIME())),
    (N'Impresora Epson EcoTank L3250',  N'Impresoras',     199.00,  7, DATEADD(DAY,  -3, SYSDATETIME())),
    (N'Memoria USB SanDisk 64 GB',      N'Almacenamiento',   9.99, 80, DATEADD(DAY,  -2, SYSDATETIME()));
END
GO

SELECT COUNT(*) AS TotalProductos FROM dbo.Productos;
GO
