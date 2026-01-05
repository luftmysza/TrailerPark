using System;
using System.Globalization;
using TrailerPark.Core.Models;

namespace TrailerPark.Application.Mapping;

internal static class MovieObjectMapper
{
    internal static Movie? MapToMovie (this OmdbMovie inboundMovie)
    {
        if (inboundMovie?.imdbID is null) return null;
        
        DateOnly? inboundMovieDate = DateOnly.ParseExact(inboundMovie?.Released!, "dd MMM yyyy", CultureInfo.InvariantCulture);
        int inboundMovieRuntimeMin = int.Parse(inboundMovie?.Runtime?.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]!);

        Movie outboundMovie = new Movie(){ imdbID = inboundMovie?.imdbID! };
            outboundMovie.Title         = inboundMovie?.Title;
            outboundMovie.Year          = inboundMovieDate?.Year;
            outboundMovie.Rated         = inboundMovie?.Rated;
            outboundMovie.Released      = inboundMovieDate;
            outboundMovie.RuntimeMin    = inboundMovieRuntimeMin;
            outboundMovie.Genre         = inboundMovie?.Genre?.ToUpper();
            outboundMovie.Director      = inboundMovie?.Director?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(director => director.Trim()).ToList()!;
            outboundMovie.Writer        = inboundMovie?.Writer?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(writer => writer.Trim()).ToList()!;
            outboundMovie.Actors        = inboundMovie?.Actors?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(actor => actor.Trim()).ToList()!;
            outboundMovie.Ratings       = inboundMovie?.Ratings?.Select(ratingKvp => new Rating{Source  = ratingKvp?["Source"].Trim(),
                                                                                                Value   = ratingKvp?["Value"].Trim() }).ToList();
            outboundMovie.Plot          = inboundMovie?.Plot;
            // outboundMovie.Language      = inboundMovie?.Language;
            outboundMovie.Country       = new Country { Name = inboundMovie?.Country?.Trim()! };
            outboundMovie.Awards        = inboundMovie?.Awards?.Trim();
            outboundMovie.Poster        = inboundMovie?.Poster?.Trim();
            outboundMovie.Metascore     = inboundMovie?.Metascore?.Trim();
            outboundMovie.imdbRating    = inboundMovie?.imdbRating?.Trim();
            outboundMovie.imdbVotes     = int.Parse(inboundMovie?.imdbVotes!, NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            outboundMovie.Type          = new Core.Models.Type { Name = inboundMovie?.Type?.Trim()! };
            outboundMovie.DVD           = inboundMovie?.DVD?.Trim();
            outboundMovie.BoxOffice     = decimal.Parse(inboundMovie?.BoxOffice?.Trim()?.Replace(",", "")!,
                                                        NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | 
                                                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                                                        CultureInfo.GetCultureInfo("en-US"));
            outboundMovie.Production    = inboundMovie?.Production?.Trim();
            outboundMovie.Website       = inboundMovie?.Website?.Trim();
            outboundMovie.Local         = false;

        return outboundMovie;
    }
}
