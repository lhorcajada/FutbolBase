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
    public class GetClub : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/catalog/club/{id}",
                    async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        var query = new GetClubQuery
                        {
                            ClubId = id

                        };
                        return await mediator.Send(query, cancellationToken);
                    })
                .WithName(nameof(GetClub))
                .WithTags(ClubConstants.ClubFeature)
                .Produces<GetClubResponse>();
        }

        public record GetClubQuery : IQuery<GetClubResponse>, ICacheRequest
        {
            public string ClubId { get; set; }
            public string CacheKey => ClubConstants.CachePrefix;
            public DateTime? AbsoluteExpirationRelativeToNow { get; }
        }

        public record GetClubResponse(string Id, string Name, CountriesResponse Country);

        public class ClubsRequestHandler : IRequestHandler<GetClubQuery, GetClubResponse>
        {
            private readonly CatalogDbContext _db;

            public ClubsRequestHandler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<GetClubResponse> Handle(GetClubQuery request, CancellationToken cancellationToken = default)
            {
                var club = await _db.Clubs
                    .Include(c => c.Country)
                    .FirstOrDefaultAsync(c => c.Id == request.ClubId, cancellationToken);
                if (club == null)
                    throw new KeyNotFoundException($"Club '{request.ClubId}' Not Found");

                return new GetClubResponse(club.Id, club.Name,
                    new CountriesResponse(club.CountryId, club.Country.Name, club.Country.Code));


            }
        }
    }
}
