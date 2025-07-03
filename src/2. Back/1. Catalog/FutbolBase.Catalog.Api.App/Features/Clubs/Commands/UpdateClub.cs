using System.Reflection.Emit;
using FluentValidation;
using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
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
    public class UpdateClub : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/catalog/club/{id}",
                    async (string id, UpdateClubCommand command,  IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        command.ClubId = id;
                        await mediator.Send(command, cancellationToken);
                        return Results.Ok();
                    })
                .WithName(nameof(UpdateClub))
                .WithTags(ClubConstants.ClubFeature)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
        }
    }

    public class UpdateClubCommand : ICommand, IInvalidateCacheRequest
    {
        public string ClubId { get; set; } = string.Empty;
        public string ClubName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PrefixCacheKey => ClubConstants.CachePrefix;
    }

    public class UpdateDeleteClubHandler : IRequestHandler<UpdateClubCommand, Unit>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public UpdateDeleteClubHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public async Task<Unit> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            var club = await _catalogDbContext.Clubs
                .FirstOrDefaultAsync(c => c.Id == request.ClubId, cancellationToken: cancellationToken);
            if (club == null)
                throw new KeyNotFoundException($"Club '{request.ClubId}' Not Found");
            var country = await _catalogDbContext
                .Countries
                .FirstOrDefaultAsync(c => c.Code == request.CountryCode, cancellationToken: cancellationToken);
            if (country == null)
                throw new KeyNotFoundException($"Country '{request.CountryCode}' Not Found");

            club.UpdateName(request.ClubName);
            club.UpdateCountry(country.Id);

            await _catalogDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
    public class UpdateClubValidator : AbstractValidator<DeleteClubCommand>
    {
        public UpdateClubValidator()
        {
            RuleFor(r => r.ClubId)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ClubNameMaxLength);
           
        }
    }


}
