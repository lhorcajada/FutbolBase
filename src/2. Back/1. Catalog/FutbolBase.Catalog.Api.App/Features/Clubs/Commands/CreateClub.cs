using FluentValidation;
using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Domain.Entities;
using FutbolBase.Catalog.Api.App.Domain.Entities.Clubs;
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
                    async (ClubCommand command, IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        await mediator.Send(command, cancellationToken);
                        return Results.Ok();
                    })
                .WithName(nameof(CreateClub))
                .WithTags(ClubConstants.ClubFeature)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
        }
    }

    public class ClubCommand : ICommand, IInvalidateCacheRequest
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string PrefixCacheKey => ClubConstants.CachePrefix;
    }

    public class ClubHandler : IRequestHandler<ClubCommand, Unit>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ClubHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public async Task<Unit> Handle(ClubCommand request, CancellationToken cancellationToken)
        {
            var country = await _catalogDbContext.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Code == request.CountryCode, cancellationToken: cancellationToken);
            if (country == null)
                throw new KeyNotFoundException($"Country '{request.CountryCode}' Not Found");
            var club = new Club(request.Name, country.Id);
            _catalogDbContext.Clubs.Add(club);
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
    public class Validator : AbstractValidator<ClubCommand>
    {
        public Validator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ClubNameMaxLength);
            RuleFor(r => r.CountryCode)
                .NotEmpty()
                .MaximumLength(ValidationConstants.CountryCodeMaxLength);

        }
    }


}
