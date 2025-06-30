using FutbolBase.Catalog.Api.App.DependencyInjection;
using FutbolBase.Catalog.Api.App.Domain.Entities;
using FutbolBase.Catalog.Api.App.Infrastructure.Persistence;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catálogo API",
        Version = "v1",
        Description = "Documentación de Catálogo API"
    });
});
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddScoped<DbConnection>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("CatalogConnection");
    var connection = new SqlConnection(connectionString);
    return connection;
});
builder.Services.AddDbContext<CatalogDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CatalogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => new HealthCheckResult(HealthStatus.Healthy));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseProblemDetails()
    .UseHttpsRedirection()
    .UseRouting();

app.MapFeatures();
app.MapHealthChecks("./healthChecks");
app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Fútbol Base API");

app.Run();
