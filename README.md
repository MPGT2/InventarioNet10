# InventarioNet10 — CRUD de Productos

Aplicación web ASP.NET Core MVC en **.NET 10** con **Entity Framework Core** y **SQL Server**, desarrollada para la práctica de **Despliegue Multi-Entorno de Aplicaciones .NET 10**.

## Funcionalidad

- Listado de productos con búsqueda por nombre y filtro por categoría.
- Registro, consulta, edición y eliminación de productos (CRUD completo).
- Panel de inicio con totales de productos, stock y categorías.
- Datos de ejemplo cargados automáticamente la primera vez.
- Interfaz en español, adaptada a escritorio y móvil con Bootstrap.

## Tecnologías

| Componente | Versión |
|---|---|
| .NET | 10.0 |
| ASP.NET Core | MVC + Razor |
| Entity Framework Core | 10.0 |
| Base de datos | SQL Server (Express) |
| Frontend | Bootstrap 5 |

## Estructura

```
InventarioNet10/
├── Controllers/          HomeController, ProductosController
├── Data/                 AppDbContext, DbInitializer
├── Models/               Producto
├── Views/                Home, Productos, Shared
├── wwwroot/              CSS, JavaScript, Bootstrap
├── database.sql          Script de creación y datos de ejemplo
├── appsettings.json      Cadena de conexión
└── Program.cs            Configuración e inicio
```

## Ejecución local

```powershell
dotnet restore
dotnet run --urls "http://localhost:5080"
```

Abrir <http://localhost:5080>. La base `InventarioNet10` se crea automáticamente en la instancia `.\SQLEXPRESS`.

### Cadena de conexión

En `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Server=.\\SQLEXPRESS;Database=InventarioNet10;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

Para cambiar de entorno sin recompilar, definir la variable:

```powershell
$env:ConnectionStrings__Default = "Server=ENDPOINT;Database=InventarioNet10;User Id=USUARIO;Password=CLAVE;TrustServerCertificate=True"
```

## Publicación

```powershell
dotnet publish -c Release -o ./publish
```

El contenido de `./publish` es el paquete que se copia a IIS, se comprime en ZIP para Elastic Beanstalk o se sube al hosting compartido.

## Despliegue por escenario

| Escenario | Plataforma | Base de datos |
|---|---|---|
| 1 | IIS local + acceso LAN | SQL Server Express local |
| 2 | AWS EC2 Windows Server 2025 + IIS | SQL Server Express en la instancia |
| 3 | AWS Elastic Beanstalk (.NET 10 / Windows) | Amazon RDS SQL Server |
| 4 | Hosting compartido (MonsterASP) | SQL Server del hosting |

En IIS el Application Pool debe configurarse como **No Managed Code** y requiere el **.NET 10 Hosting Bundle**.

## Script de base de datos

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -i database.sql
```

## Autor

Michael Stanley Sanchez Menendez — Grupo 2  
Módulo: Implementación de Sistemas Informáticos  
Docente: Marvin Antonio Barrera Trigueros  
Ciclo 02/2026
