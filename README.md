# Entity Framework
## Crear migración
Por ejemplo, par el proyecto de Catalog:
- Desde el proyecto FutbolBase.Catalog.Api.App 
dotnet ef migrations add InitialMigration -o Infrastructure/Migrations --startup-project ../FutbolBase.Catalog.Api.Host

## Actualizar base de datos
- Desde el proyecto FutbolBase.Catalog.Api.App 
 dotnet ef database update --startup-project ../FutbolBase.Catalog.Api.Host