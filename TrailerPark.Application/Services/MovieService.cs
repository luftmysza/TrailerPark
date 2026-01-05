using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

using TrailerPark.Core.Models;
using TrailerPark.Application.Interfaces;
using TrailerPark.Application.Mapping;

namespace TrailerPark.Application.Services;

public class MovieService
{
    private readonly IMovieEnricher _movieEnricher; 
    private readonly IMovieRepository _movieRepo; 
    private readonly ICountryRepository _countryRepo;
    private readonly IExternalMovieProvider _omdbClient;
    public MovieService(IMovieRepository movieRepo, ICountryRepository countryRepo, IExternalMovieProvider omdbClient, IMovieEnricher movieEnricher)
    {
        _movieRepo = movieRepo;
        _countryRepo = countryRepo;
        _omdbClient = omdbClient;
        _movieEnricher = movieEnricher;
    }
    public async Task<IList<Movie?>?> Inbound(MovieQuery movieQuery)
    {
        IList<Movie?>? movieLocalList = null;
        IList<Movie?>? movieFetchedList = null;

        try
        {
            movieLocalList = await GetLocalAsync(movieQuery);
            if (movieLocalList is not null)
                return movieLocalList;
        }
        catch (Exception) {}
      
        try
        {
            movieFetchedList = await FetchOmdbAsync(movieQuery);
            if (movieFetchedList is not null)
            {
                await _movieRepo.AddBatchAsync(movieFetchedList);
                return movieFetchedList;
            }
        }
        catch (Exception) {}

        return null;  
    }
    public async Task<IList<Movie?>?> GetLocalAsync(MovieQuery movieQuery)
    {
        IList<Movie?>? movieLocalList = null;

        if (movieQuery.imdbID is not null)
        {
            var getBuffer = await _movieRepo.GetByIdAsync(movieQuery);
            if (getBuffer is not null)
                movieLocalList = new[] { getBuffer };                
        }
  
        return movieLocalList;
    }
    public async Task<IList<Movie?>?> FetchOmdbAsync(MovieQuery movieQuery)
    {
        IList<OmdbMovie?>? movieFetchedList = null;
        IList<Movie?>? movieMappedList = null;

        if (movieQuery.imdbID is not null)
        {
            var fetchBuffer = await _omdbClient.FetchByIdAsync(movieQuery);
            if (fetchBuffer is not null)
                movieFetchedList = new[] { fetchBuffer };
        }

        movieMappedList = await ProcessFetchedAsync(movieFetchedList);

        return movieMappedList;
    }
    private async Task<IList<Movie?>?> ProcessFetchedAsync(IList<OmdbMovie?>? fetchBuffer)
    {
        if (fetchBuffer is null) return null;
     
        IList<Movie?>? movieEnrichedList = new List<Movie?>();
        Movie stagedMovie = null!;
        
        foreach (OmdbMovie? fetchedMovie in fetchBuffer)
        {
            if (fetchedMovie is null) continue;
                
            stagedMovie = MovieObjectMapper.MapToMovie(fetchedMovie)!;
            await _movieEnricher.EnrichMovieAsync(stagedMovie);
            movieEnrichedList.Add(stagedMovie);
        }

        return movieEnrichedList;
    }
}