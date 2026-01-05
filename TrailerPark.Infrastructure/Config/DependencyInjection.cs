using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TrailerPark.Application.Services;
using TrailerPark.Application.Interfaces;
using TrailerPark.Infrastructure.Omdb;
using TrailerPark.Infrastructure.Config;
using TrailerPark.Infrastructure.Enrichment;
using TrailerPark.Infrastructure.Repositories;

namespace TrailerPark.Intrastructure.Config;

public static class DependencyInjection
{
    public static WebApplicationBuilder InjectInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.UseInMemoryDatabase("DevDb")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }
            else 
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DB"));
            }
        });
            
        builder.Services.AddHttpClient<IExternalMovieProvider, OmdbClient>(client =>
        {
            client.BaseAddress = new Uri(builder?.Configuration["Omdb:BaseUrl"]!);
        });

        builder.Services.AddScoped<IMovieRepository, MovieRepo>();
        builder.Services.AddScoped<ICountryRepository, CountryRepo>();
        builder.Services.AddScoped<IMovieEnricher, MovieEnricher>();
        builder.Services.AddScoped<MovieService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        return builder;
    }
    public static async Task InjectSeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

        await DbSeeder.SeedDbAsync(context, env.ContentRootPath);
    }
}
