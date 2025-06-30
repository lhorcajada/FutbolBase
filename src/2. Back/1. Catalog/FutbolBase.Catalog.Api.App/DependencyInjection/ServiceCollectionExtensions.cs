using FluentValidation;
using FluentValidation.AspNetCore;
using FutbolBase.Api.App.Common.Behaviors;
using FutbolBase.Catalog.Api.App.Domain;
using FutbolBase.Catalog.Api.App.Features.Clubs.Commands;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutbolBase.Catalog.Api.App.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<App>())
                .AddApplicationPart(typeof(App).Assembly);

            services
                .AddCustomProblemDetails()
                .AddValidatorsFromAssembly(typeof(App).Assembly)
                .AddDiagnostics(configuration)
                .AddMediatR(typeof(App).Assembly);

            services.AddBehaviors()
             .AddEasyCaching(options => { options.UseInMemory(Cache.CacheDefaultName); });

           //TODO: De momento no lo añadimos
            //services.AddIntegrationEvents();

            return services;
        }


        static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TimeLoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(InvalidateCachingBehavior<,>));
        }

        static IServiceCollection AddDiagnostics(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationInsightsTelemetry(configuration);
        }

        static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            return services.AddProblemDetails(setup =>
            {
                setup.Map<DomainException>(exception =>
                {
                    var details = new ProblemDetails
                    {
                        Title = exception.Title,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = exception.Description,
                        Type = exception.GetType().Name
                    };

                    return details;
                });
                setup.Map<InvalidOperationException>(exception =>
                {
                    var details = new ProblemDetails
                    {
                        Title = "",
                        Status = StatusCodes.Status409Conflict,
                        Detail = exception.Message,
                        Type = exception.GetType().Name
                    };

                    return details;
                });
                setup.Map<ValidationException>(exception => {
                    var details = new ProblemDetails
                    {
                        Title = "",
                        Status = StatusCodes.Status409Conflict,
                        Detail = exception.Message,
                        Type = exception.GetType().Name
                    };

                    return details;
                });
                setup.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                setup.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                setup.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
        }
    }
}