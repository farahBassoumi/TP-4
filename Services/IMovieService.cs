using tp.Models;

namespace tp.Services
{
    public interface IMovieService
    {
        IQueryable<Movies> GetMoviesByGenre(Genres genre);
        IQueryable<Movies> GetMoviesByGenreId(Guid genreId);


        IQueryable<Movies> GetMoviesOrderedByTitle();

    }
}
