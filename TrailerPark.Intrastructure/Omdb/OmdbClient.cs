using System.Net.Http;
using System.Net.Http.Json;

using Microsoft.Extensions.Configuration;

using TrailerPark.Core.Models;
using TrailerPark.Core.Interfaces;

namespace TrailerPark.Intrastructure.Omdb;

public class OmdbClient : IExternalMovieProvider
{
    private readonly HttpClient _http;
    private readonly string _apiBase;
    private readonly string _apiKey;

    public OmdbClient(HttpClient httpClient, IConfiguration configuration)
    {
        _http = httpClient;
        _apiBase = configuration["Omdb:BaseUrl"]!; 
        _apiKey = configuration["Omdb:ApiKey"]!;
    }
    public async Task<Movie?> FetchByIdAsync(MovieQuery movieQuery)
    {
        var url = $"{_apiBase}?i={Uri.EscapeDataString(movieQuery?.imdbID!)}&apikey={_apiKey}";

        OmdbMovie? movieFetched = await _http.GetFromJsonAsync<OmdbMovie>(url);
        if (movieFetched is null) return null;

        Movie? movieMapped = OOMapper.MapToMovie(movieFetched);
        return movieMapped;
    }
    public async Task<IEnumerable<Movie?>?> FetchBySearchAsync(MovieQuery movieQuery)
    {
        throw new NotImplementedException();
        //var url = $"?t={Uri.EscapeDataString(movieQuery?.SearchString!)}&apikey={_apiKey}";

        // IEnumerable<OmdbMovie>? movieFetchedList = await _http.GetFromJsonAsync<IEnumerable<OmdbMovie>>(url);
        // if (movieFetchedList is null) return null;

        // IEnumerable<Movie?>? movieMappedList = await Task.WhenAll(movieFetchedList.Select(async m => await FetchByIdAsync(new MovieQuery(){ imdbID = m.imdbID })));

        // return movieMappedList;
    }



    
    public async Task<Movie?> FetchByTitleAsync(MovieQuery movieQuery)
    {
        throw new NotImplementedException();
    }
    public async Task<Movie?> FetchByTypeAsync(MovieQuery movieQuery)
    {
        throw new NotImplementedException();
    }
}