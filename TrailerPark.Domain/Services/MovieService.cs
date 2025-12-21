using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;

using TrailerPark.Core.Models;
using TrailerPark.Core.Interfaces;


namespace TrailerPark.Core.Services;

public class MovieService
{
    private readonly IMovieRepository _movieRepo;
    private readonly IExternalMovieProvider _omdbClient;
    public MovieService(IMovieRepository movieRepo, IExternalMovieProvider omdbClient)
    {
        _movieRepo = movieRepo;
        _omdbClient = omdbClient;
    }
    public async Task<IEnumerable<Movie?>?> Inbound(MovieQuery movieQuery)
    {
        IEnumerable<Movie?>? movieLocalList = null;
        IEnumerable<Movie?>? movieFetchedList = null;

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
    public async Task<IEnumerable<Movie?>?> GetLocalAsync(MovieQuery movieQuery)
    {
        IEnumerable<Movie?>? movieLocalList = null;
        if (movieQuery.imdbID is not null)
        {
            var getBuffer = await _movieRepo.GetByIdAsync(movieQuery);
            if (getBuffer is not null)
                movieLocalList = new[] { getBuffer };                
        }
        // else if (movieQuery.SearchString is not null)
        //     movieLocalList = await _movieRepo.GetBySearchAsync(movieQuery);

        return movieLocalList;
    }
    public async Task<IEnumerable<Movie?>?>  FetchOmdbAsync(MovieQuery movieQuery)
    {
        IEnumerable<Movie?>? movieFetchedList = null;
        if (movieQuery.imdbID is not null)
        {
            var fetchBuffer =  await _omdbClient.FetchByIdAsync(movieQuery);
            if (fetchBuffer is not null)
                movieFetchedList = new[] { fetchBuffer };
        }
        // else if (movieQuery.SearchString is not null)
        //     movieFetchedList = await _omdbClient.FetchBySearchAsync(movieQuery);

        return movieFetchedList;
    }
}