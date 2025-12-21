using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TrailerPark.Core.Services;
using TrailerPark.Core.Interfaces;

using TrailerPark.Intrastructure.Omdb;
using TrailerPark.Infrastructure.Config;
using TrailerPark.Intrastructure.Repositories;
using Microsoft.AspNetCore.Builder;

namespace TrailerPark.Intrastructure.Config;

public static class DependencyInjection
{
    public static WebApplicationBuilder InjectCustom(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("localDb")
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

        builder.Services.AddHttpClient<IExternalMovieProvider, OmdbClient>(client =>
        {
            client.BaseAddress = new Uri(builder?.Configuration["Omdb:BaseUrl"]!);
        });

        builder.Services.AddScoped<IMovieRepository, MoviesRepo>();
        builder.Services.AddScoped<MovieService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        return builder;
    }
}
