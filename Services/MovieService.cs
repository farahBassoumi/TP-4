using Microsoft.EntityFrameworkCore;
using tp.Data;
using tp.Models;

namespace tp.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDBContext _db;

        public MovieService(ApplicationDBContext db)
        {
            _db = db;
        }
        public IQueryable<Movies> GetMoviesByGenre(Genres genre)
        {
            var moviesByGenre = _db.movies.Where(movie => movie.GenreName == genre.Name);
            return (IQueryable<Movies>)moviesByGenre.ToList();  
        }


       public  IQueryable<Movies> GetMoviesByGenreId(Guid genreId)
        {
            var moviesByGenreId = _db.movies.Where(movie => movie.Genre_Id == genreId);
            return (IQueryable<Movies>)moviesByGenreId.ToList();
        }

       public  IQueryable<Movies> GetMoviesOrderedByTitle()
        {
            var orderedMovies = _db.movies.OrderBy(movie => movie.Name);

            return orderedMovies;
        }

    }
}
