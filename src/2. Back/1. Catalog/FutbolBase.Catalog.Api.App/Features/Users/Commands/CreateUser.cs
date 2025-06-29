using FutbolBase.Api.App.Common;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FutbolBase.Catalog.Api.App.Features.Users.Commands
{
    public class CreateUser : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (Command command, IMediator mediator, CancellationToken cancellationToken) 
                    => await mediator.Send(command, cancellationToken))
                .WithName(nameof(CreateUser))
                .WithTags(UserConstants.UserFeature)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
        }

        public class Command : ICommand, IInvalidateCacheRequest, IRequest<IResult>
        {
            public string Alias { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int ClubId { get; set; }
            public string PrefixCacheKey => UserConstants.CachePrefix;
        }

        public class Handler : IRequestHandler<Command, IResult>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser
                {
                    Name = request.Alias,
                    Email = request.Email,
                    UserName = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                return !result.Succeeded ? Results.BadRequest(result.Errors) : Results.Ok("Usuario registrado exitosamente");
            }
        }
    }
}
