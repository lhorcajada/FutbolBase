using FluentValidation;
using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace FutbolBase.Catalog.Api.App.Features.Clubs.Commands
{
    public class DeleteClub : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/catalog/club/{id}",
                    async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        var request = new DeleteClubCommand()
                        {
                            ClubId = id
                        };
                        await mediator.Send(request, cancellationToken);
                        return Results.Ok();
                    })
                .WithName(nameof(DeleteClub))
                .WithTags(ClubConstants.ClubFeature)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
        }
    }

    public class DeleteClubCommand : ICommand, IInvalidateCacheRequest
    {
        public string ClubId { get; set; } = string.Empty;
        public string PrefixCacheKey => ClubConstants.CachePrefix;
    }

    public class DeleteClubHandler : IRequestHandler<DeleteClubCommand, Unit>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public DeleteClubHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public async Task<Unit> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            var club = await _catalogDbContext.Clubs
                .FirstOrDefaultAsync(c => c.Id == request.ClubId, cancellationToken: cancellationToken);
            if (club == null)
                throw new KeyNotFoundException($"Club '{request.ClubId}' Not Found");

            _catalogDbContext.Clubs.Remove(club);
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
    public class DeleteClubValidator : AbstractValidator<DeleteClubCommand>
    {
        public DeleteClubValidator()
        {
            RuleFor(r => r.ClubId)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ClubNameMaxLength);
           
        }
    }


}
