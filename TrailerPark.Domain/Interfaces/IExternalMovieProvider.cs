using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using TrailerPark.Core.Models;

namespace TrailerPark.Core.Interfaces;

public interface IExternalMovieProvider
{
    public Task<Movie?> FetchByTitleAsync(MovieQuery movieQuery);
    public Task<IEnumerable<Movie?>?> FetchBySearchAsync(MovieQuery movieQuery);
    public Task<Movie?> FetchByIdAsync(MovieQuery movieQuery);
    public Task<Movie?> FetchByTypeAsync(MovieQuery movieQuery);
    
}
