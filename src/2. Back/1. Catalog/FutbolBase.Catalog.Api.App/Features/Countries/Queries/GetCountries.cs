using Azure;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Api.App.Common;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace FutbolBase.Catalog.Api.App.Features.Countries.Queries
{
    public class GetCountries : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/catalog/countries",
                    async (IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        return await mediator.Send(new CountriesQuery(), cancellationToken);
                    })
                .WithName(nameof(GetCountries))
                .WithTags(CountryConstants.CountryFeature)
                .Produces<CountriesResponse[]>();
        }

        public record CountriesQuery : IQuery<CountriesResponse[]>, ICacheRequest
        {
            public string CacheKey => CountryConstants.CachePrefix;
            public DateTime? AbsoluteExpirationRelativeToNow { get; }
        }

        public record CountriesResponse(int Id, string Name, string Code);

        public class CountriesRequestHandler : IRequestHandler<CountriesQuery, CountriesResponse[]>
        {
            private readonly ReadOnlyCatalogDbContext _db;

            public CountriesRequestHandler(ReadOnlyCatalogDbContext db)
            {
                _db = db;
            }

            public async Task<CountriesResponse[]> Handle(CountriesQuery request, CancellationToken cancellationToken = default)
            {
                return await _db.Countries
                    .Select(td => new CountriesResponse(td.Id, td.Name, td.Code))
                    .ToArrayAsync(cancellationToken);
            }
        }
    }
}
