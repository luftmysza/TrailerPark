using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TrailerPark.Core.Models;
using TrailerPark.Application.Interfaces;

namespace TrailerPark.Infrastructure.Enrichment;

public class MovieEnricher : IMovieEnricher
{
    private readonly IMovieRepository _movieRepo; 
    private readonly ICountryRepository _countryRepo;
    public MovieEnricher(IMovieRepository movieRepo, ICountryRepository countryRepo)
    {
        _movieRepo = movieRepo;
        _countryRepo = countryRepo;
    }

    public async Task EnrichMovieAsync(Movie? mappedMovie)
    {
        var list = await _countryRepo.All.ToListAsync();

        if (mappedMovie is not null && mappedMovie?.Country is not null)
            mappedMovie.Country = await _countryRepo.All.FirstOrDefaultAsync(country => country.Name == mappedMovie.Country.Name || country.Name.Contains(mappedMovie.Country.Name));
    }
}
