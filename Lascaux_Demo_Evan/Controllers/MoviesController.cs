using DAL_Lascaux_Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BAL_Lascaux_Demo;

namespace Lascaux_Demo_Evan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet("allMovies")]
        public IActionResult GetAllMovies()
        {
            return new OkObjectResult(_movieService.GetAllMovies());
        }

        /// <summary>
        /// Gets movie bij Id
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>The requested movie</returns>
        [HttpGet("GetMovieById")]
        public IActionResult GetMovie(Guid movieId)
        {
            Movie? myMovie = _movieService.GetMovieById(movieId);

            if (myMovie != null)
            {
                return new OkObjectResult(myMovie);
            }
            else
            {
                return new NotFoundObjectResult("Movie not found ");
            }
        }
    }
}
