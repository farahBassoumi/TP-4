using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using tp.Data;
using tp.Models;
using tp.Services;

namespace tp.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<Movies> _logger;
        private readonly ApplicationDBContext _db;
        private readonly IMovieService _movieService;
        public MovieController(ApplicationDBContext db, ILogger<Movies> logger, IMovieService MS)
        {
            _logger = logger;
            _db = db;
            _movieService = MS;
        }

        public IActionResult Index()//get movies
        {


            var movies = _db.movies.ToList();
            return View(movies);
        }

        //********************************************    ADD   MOVIE    ******************************************************
        [HttpGet]
        public IActionResult AddMovie()

        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddMovie(Movies moviemodel)

        {

            if (ModelState.IsValid)
            {
                Genres genre = await _db.genres.FirstOrDefaultAsync(x => x.Name == moviemodel.GenreName);

                if (genre == null)
                {
                    genre = new Genres(moviemodel.Name);
                    await _db.genres.AddAsync(genre);
                    await _db.SaveChangesAsync();
                    //movie.Genre_Id = genreToAdd.Id;

                }
                moviemodel.Genre_Id = genre.Id;


                moviemodel.MoviePicturePath = UploadFile(moviemodel);

                ViewBag.Message = "File uploaded successfully!";

            
                return RedirectToAction("Index");
            }
            else return View();
        

    }

        //********************************************    UPLOAD FILE    ******************************************************

        private string UploadFile(Movies m)
        {
            string file = string.Empty;
            if (m.MoviePicture != null)
            {
                string filePath = Path.Combine("uploads", Guid.NewGuid().ToString() + m.MoviePicture.FileName);
                file = filePath;
                using (var filestream = new FileStream(Path.Combine("wwwroot", filePath), FileMode.Create))
                {
                    m.MoviePicture.CopyTo(filestream);
                }
            }
            return file;
        }

        //********************************************    ADD   GENRE    ******************************************************

        [HttpGet]
        public IActionResult AddGenre()

        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddGenre(Genres genre)

        {
            // _logger.LogInformation(movie.Name);
            //var movie =  new Movie(addMovieViewModel.Name);
            await _db.genres.AddAsync(genre);
            await _db.SaveChangesAsync();


            return RedirectToAction("AddGenre");
        }

        //********************************************    EDIT MOVIE    ******************************************************

        [HttpGet]
        public IActionResult EditMovie(Guid id)
        {
            Movies movie = _db.movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(Guid id, Movies updatedMovie)
        {
            {
                _logger.LogInformation("inside the edit action 1");

                Movies existingMovie = await _db.movies.FirstOrDefaultAsync(m => m.Id == id);
                if (existingMovie == null)
                {
                    _logger.LogInformation("inside the not found action");

                    return NotFound();
                }
                existingMovie.Name = updatedMovie.Name;
                existingMovie.GenreName = updatedMovie.GenreName;
                Genres updatedGenre = await _db.genres.FirstOrDefaultAsync(x => x.Name == updatedMovie.GenreName);
                if (updatedGenre == null)
                {
                    _logger.LogInformation("updatedGenre not Found");
                    Genres newgenre = new Genres(existingMovie.GenreName);

                    AddGenre(newgenre);

                    existingMovie.Genre_Id = newgenre.Id;

                }
                else
                {
                    _logger.LogInformation("updatedGenre found" + updatedGenre.Name);

                    existingMovie.Genre_Id = updatedGenre.Id;
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _logger.LogInformation("model statie is valid");

                        // Update the existing movie in the database
                        // _db.Entry(updatedMovie).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                    // return RedirectToAction("Index");


                }
                else
                {
                    _logger.LogInformation("ModelState.IsNotValid");

                    foreach (var key in ModelState.Keys)
                    {
                        var errors = ModelState[key].Errors;
                        foreach (var error in errors)
                        {
                            var errorMessage = error.ErrorMessage;
                            _logger.LogInformation("ModelState[key] " + ModelState[key] + " error:" + errorMessage);
                            // You can now log, display, or handle the error message as needed.
                        }
                    }

                }
                return View("EditMovie", updatedMovie);


            }


        }
        //********************************************    DELETE MOVIE    ******************************************************


        [HttpPost]

        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movieToDelete = await _db.movies.FindAsync(id);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            _db.movies.Remove(movieToDelete);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //********************************************    DETAILS   ******************************************************


        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _db.movies
                .Include(m => m.Customers) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        //********************************************    get Movie By Genre   ******************************************************

        public async Task<IActionResult> getMovieByGenre(Genres genre)
        {
            var movies = _movieService.GetMoviesByGenre(genre);
            return RedirectToAction("Index", "Movie", movies);

        }


        //********************************************    get Movie By Genre Id  ******************************************************

        public async Task<IActionResult> getMovieByGenreId(Guid genreId)
        {
            var movies = _movieService.GetMoviesByGenreId(genreId);
            return RedirectToAction("Index", "Movie", movies);

        }

        //********************************************    get ordered Movies   ******************************************************

        public IActionResult GetOrderedMovies()
        {
            var orderedMovies = _movieService.GetMoviesOrderedByTitle().ToList();
            return View(orderedMovies);
        }


    }

    }