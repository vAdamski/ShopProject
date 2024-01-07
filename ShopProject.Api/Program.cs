using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using ShopProject.Api.Services;
using ShopProject.Application;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Infrastructure;
using ShopProject.Persistence;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.LogCompleteSecurityArtifact = true;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));


JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        
        options.Authority = "https://localhost:5001";
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            NameClaimType = JwtClaimTypes.Name,
            RoleClaimType = JwtClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
    options.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
        policy.RequireRole("admin");
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "api1", "Full access" },
                    { "user", "User info" },
                    { "openid", "openid" },
                    { "role", "User role" },
                    { "IdentityServerApi" , "Identity Server Api Access"},
                    { "firstName", "First Name" },
                    { "lastName", "Last Name" }
                }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BudgetPlan",
        Version = "v1",
        Description = "",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Adam Ludwiczak",
            Email = ""
        },
        License = new OpenApiLicense
        {
            Name = "Used License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

using var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.EnsureCreated();

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeConfiguration:SecretKey").Get<string>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
        options.OAuthClientId("swagger");
        options.OAuthClientSecret("secret");
        options.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();