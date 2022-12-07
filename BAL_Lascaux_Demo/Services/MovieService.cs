using DAL_Lascaux_Demo.Data;
using DAL_Lascaux_Demo.Models;

namespace BAL_Lascaux_Demo
{
    public class MovieService
    {
        public readonly CinemaDbContext _context;

        public MovieService(CinemaDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        /// <returns>List of movies</returns>
        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        /// <summary>
        /// Checks if a movie with the given Id exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Boolean</returns>
        public bool MovieExists(Guid movieId)
        {
            return _context.Movies.Where(movie => movie.Id.Equals(movieId)).Any();
        }

        /// <summary>
        /// Gets a movie by its Id
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>A movie object</returns>
        public Movie? GetMovieById(Guid movieId)
        {
            return _context.Movies.Where(movie => movie.Id.Equals(movieId)).FirstOrDefault();
        }
    }
}
