using System.Globalization;
using Microsoft.Extensions.Hosting;
using TrailerPark.Core.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace TrailerPark.Infrastructure.Config;

public static class DbSeeder
{
    public static async Task SeedDbAsync(AppDbContext context, string contentRootPath)
    {

        if (await context.Countries.AnyAsync())
            return;

        using var stream = new StreamReader( Path.Combine(
                    contentRootPath,
                    "Data",
                    "Country.csv")
                );
        if (stream is null) 
            throw new FileNotFoundException();
         
        using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
        {
            var record = csv.GetRecords<Country>();
            await context.AddRangeAsync(record);
        }
        await context.SaveChangesAsync();
    }
}
