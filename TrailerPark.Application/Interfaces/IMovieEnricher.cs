using System;
using TrailerPark.Core.Models;

namespace TrailerPark.Application.Interfaces;

public interface IMovieEnricher
{
    public Task EnrichMovieAsync(Movie? fetchedMovie);
}
