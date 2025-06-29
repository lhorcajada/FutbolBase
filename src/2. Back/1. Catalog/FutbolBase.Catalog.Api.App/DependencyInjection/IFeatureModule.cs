using Microsoft.AspNetCore.Routing;

namespace FutbolBase.Catalog.Api.App.DependencyInjection;

public interface  IFeatureModule
{
    void AddRoutes(IEndpointRouteBuilder app);
}