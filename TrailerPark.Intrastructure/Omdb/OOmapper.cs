using System;
using System.Globalization;
using TrailerPark.Core.Models;

namespace TrailerPark.Intrastructure.Omdb;

internal static class OOMapper
{
    internal static Movie? MapToMovie (this OmdbMovie inboundMovie)
    {
        if (inboundMovie?.imdbID is null) return null;
        
        DateOnly? inboundMovieDate = DateOnly.ParseExact(inboundMovie?.Released!, "dd MMM yyyy", CultureInfo.InvariantCulture);
        int inboundMovieRuntimeMin = int.Parse(inboundMovie?.Runtime?.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]!);
        // Movie outboundMovie = new Movie()
        // {
        //     imdbID      = inboundMovie?.imdbID ,
        //     Title       = inboundMovie?.Title ,
        //     Year        = inboundMovieDate?.Year ,
        //     Rated       = inboundMovie?.Rated ,
        //     Released    = inboundMovieDate,
        //     RuntimeMin  = Int32.Parse(inboundMovie?.Runtime!) ,
        //     Genre       = inboundMovie?.Genre?.ToUpper() ,
        //     Director    = new Person() { Name = inboundMovie?.Director?.ToUpper()! } ,
        //     Writer      = new Person() { Name = inboundMovie?.Writer?.ToUpper()! } ,
        //     // Actors      = new Person() { Name = inboundMovie?.Writer?.ToUpper()! } ,
        //     Plot        = inboundMovie?.Plot ,
        //     LanguageISO = inboundMovie?.Language ,
        //     CountryISO  = inboundMovie?.Country ,
        //     Awards      = inboundMovie?.Awards ,
        //     Poster      = inboundMovie?.Poster ,
        //     Metascore   = inboundMovie?.Metascore ,
        //     imdbRating  = inboundMovie?.imdbRating ,
        //     imdbVotes   = Int32.Parse(inboundMovie?.imdbVotes!) ,
        //     Type        = inboundMovie?.Type ,
        //     DVD         = inboundMovie?.DVD ,
        //     Production  = inboundMovie?.Production ,
        //     Website     = inboundMovie?.Website ,
        //     Response    = inboundMovie?.Response
        // };
        Movie outboundMovie = new Movie(){ imdbID = inboundMovie?.imdbID! };
            outboundMovie.Title       = inboundMovie?.Title;
            outboundMovie.Year        = inboundMovieDate?.Year;
            outboundMovie.Rated       = inboundMovie?.Rated;
            outboundMovie.Released    = inboundMovieDate;
            outboundMovie.RuntimeMin  = inboundMovieRuntimeMin;
            outboundMovie.Genre       = inboundMovie?.Genre?.ToUpper();
            outboundMovie.Director    = new Person { Name = inboundMovie?.Director?.ToUpper()! };
            // outboundMovie.Writer      = new Person { Name = inboundMovie?.Writer?.ToUpper()! };
            // outboundMovie.Actors      = new List { new Person { Name = inboundMovie?.Writer?.ToUpper()! } };
            // outboundMovie.Rating      = new Ratung { ????? };
            outboundMovie.Plot        = inboundMovie?.Plot;
            outboundMovie.LanguageISO = inboundMovie?.Language;
            outboundMovie.CountryISO  = inboundMovie?.Country;
            outboundMovie.Awards      = inboundMovie?.Awards;
            outboundMovie.Poster      = inboundMovie?.Poster;
            outboundMovie.Metascore   = inboundMovie?.Metascore;
            outboundMovie.imdbRating  = inboundMovie?.imdbRating;
            outboundMovie.imdbVotes   = Int32.Parse(inboundMovie?.imdbVotes!, NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            outboundMovie.Type        = new Core.Models.Type { Name = inboundMovie?.Type };
            outboundMovie.DVD         = inboundMovie?.DVD;
            outboundMovie.BoxOffice   = decimal.Parse(inboundMovie?.BoxOffice?.Replace(",", "")!,
                                                        NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                                                        CultureInfo.GetCultureInfo("en-US"));
            outboundMovie.Production  = inboundMovie?.Production;
            outboundMovie.Website     = inboundMovie?.Website;
            outboundMovie.Response    = inboundMovie?.Response;

        return outboundMovie;
    }
}