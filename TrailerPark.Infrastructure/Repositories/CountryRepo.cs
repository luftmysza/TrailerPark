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

public class CountryRepo : ICountryRepository
{
    private readonly AppDbContext _context;
    public DbSet<Country> All => _context.Countries;
    public CountryRepo(AppDbContext context)
    {
        _context = context;
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