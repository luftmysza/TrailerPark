using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TrailerPark.Core.Models;
using TrailerPark.Application.Interfaces;
using TrailerPark.Infrastructure.Config;

namespace TrailerPark.Infrastructure.Repositories;

public class MovieRepo : IMovieRepository
{
    private readonly AppDbContext _context;
    public MovieRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Movie?> GetByIdAsync(MovieQuery movieQuery)
    {
        return await _context.Movies
            .Include(movie => movie.Country)
            .Include(movie => movie.Ratings)
            .Include(movie => movie.Type)
            .FirstOrDefaultAsync(movie => movie.imdbID == movieQuery.imdbID);
    }
    public async Task<IEnumerable<Movie?>?> GetBySearchAsync(MovieQuery movieQuery)
    {
        Movie? movie = null;
        movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.imdbID == movieQuery.imdbID);
        movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.imdbID == movieQuery.imdbID);

        throw new NotImplementedException();
    }
    public async Task AddBatchAsync(IEnumerable<Movie?> movies)
    {   
        await _context.AddAsync<Movie>(movies.ToArray()[0]!);
        await _context.SaveChangesAsync();
    }
    // public async Task<Movie?> GetByTitleAsync(string title)
    // {
    //     return await _context.Movies.FirstOrDefaultAsync(m => m.Title == title);
    // }
    // public async Task<Movie?> GetByTypeAsync(string type)
    // {
    //     return await _context.Movies.FirstOrDefaultAsync(m => m.Type == type);
    // }
}