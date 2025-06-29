using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Domain.Entities;
using FutbolBase.Catalog.Api.App.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace FutbolBase.Catalog.Api.App.Features.Clubs.Commands
{
    public class CreateClub : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/club/create",
                    async (ClubCommand command, IMediator mediator, CancellationToken cancellationToken)
                        => await mediator.Send(command, cancellationToken))
                .WithName(nameof(CreateClub))
                .WithTags(ClubConstants.ClubFeature)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
        }
    }

    public class ClubCommand : ICommand, IInvalidateCacheRequest, IRequest<IResult>
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string PrefixCacheKey => ClubConstants.CachePrefix;
    }

    public class ClubHandler : IRequestHandler<ClubCommand, IResult>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ClubHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public async Task<IResult> Handle(ClubCommand request, CancellationToken cancellationToken)
        {
            var country = await _catalogDbContext.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Code == request.CountryCode, cancellationToken: cancellationToken);
            if (country == null)
                throw new KeyNotFoundException($"Country '{request.CountryCode}' Not Found");
            var club = new Club
            {
                CountryId = country.Id,
                Name = request.Name
            };
            _catalogDbContext.Clubs.Add(club);
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
            return Results.Created($"/clubs/{club.Id}", club);
        }
    }

}
