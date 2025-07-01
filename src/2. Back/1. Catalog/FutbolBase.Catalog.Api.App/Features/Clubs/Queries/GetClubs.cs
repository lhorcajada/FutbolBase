using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using static FutbolBase.Catalog.Api.App.Features.Countries.Queries.GetCountries;

namespace FutbolBase.Catalog.Api.App.Features.Clubs.Queries
{
    public class GetClubs : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/catalog/clubs",
                    async (IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        return await mediator.Send(new ClubsQuery(), cancellationToken);
                    })
                .WithName(nameof(GetClubs))
                .WithTags(ClubConstants.ClubFeature)
                .Produces<ClubsResponse[]>();
        }

        public record ClubsQuery : IQuery<ClubsResponse[]>, ICacheRequest
        {
            public string CacheKey => ClubConstants.CachePrefix;
            public DateTime? AbsoluteExpirationRelativeToNow { get; }
        }

        public record ClubsResponse(string Id, string Name, CountriesResponse Country);

        public class ClubsRequestHandler : IRequestHandler<ClubsQuery, ClubsResponse[]>
        {
            private readonly CatalogDbContext _db;

            public ClubsRequestHandler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<ClubsResponse[]> Handle(ClubsQuery request, CancellationToken cancellationToken = default)
            {
                return await _db.Clubs
                    .Include(c=> c.Country)
                    .Select(td => new ClubsResponse(td.Id, td.Name, new CountriesResponse(td.CountryId, td.Country.Name, td.Country.Code)))
                    .ToArrayAsync(cancellationToken);
            }
        }
    }
}
