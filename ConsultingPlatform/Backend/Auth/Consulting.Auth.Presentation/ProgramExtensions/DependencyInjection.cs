using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Application;
using Consulting.Auth.Infrastructure.db;
using Consulting.Auth.Infrastructure.db.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Consulting.Auth.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FastEndpoints;
using Consulting.Auth.Infrastructure.mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;
using Consulting.Auth.Infrastructure.mongo.Repositories;
using Consulting.Auth.Domain;

namespace Consulting.Auth.Presentation.ProgramExtensions;

public static class DependencyInjection
{
    public static async Task ApplyMigrationsAndSeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();

        try
        {
            //if (dbContext.Database.GetPendingMigrations() != null)
            //{
            //    await dbContext.Database.EnsureCreatedAsync();
            //}

            await dbContext.Database.MigrateAsync();
            await IdentitySeeder.SeedAsync(services);
        }
        catch (Exception)
        {
            // TODO: Add logging
            throw;
        }
    }

    public static async Task InitializeMongoCollectionsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var mongo = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

        var collections = await mongo.Database.ListCollectionNames().ToListAsync();

        if (!collections.Contains("testResults"))
            await mongo.Database.CreateCollectionAsync("testResults");

        if (!collections.Contains("vacancies"))
            await mongo.Database.CreateCollectionAsync("vacancies");

        Console.WriteLine("MongoDB collections initialized.");
    }


    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddFastEndpoints();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IEmailService, EmailService>();

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<MongoDbContext>();
        builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();
        builder.Services.AddScoped<ITestResultService, TestResultService>();
        builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
        builder.Services.AddScoped<IVacancyService, VacancyService>();

    }

    public static void AddCustomAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };
        });

        builder.Services.AddAuthorization();
    }

    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("all-allowed", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }

    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
    }
}
