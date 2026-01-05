using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using TrailerPark.Core.Models;

namespace TrailerPark.Application.Interfaces;

public interface ICountryRepository
{
    public DbSet<Country> All {get;}
}
