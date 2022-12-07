using System.ComponentModel.DataAnnotations;

namespace DAL_Lascaux_Demo.Models
{
    public class Movie
    {
        /// <summary>
        /// Unique Id of the movie
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the movie
        /// </summary>
        public string Name{ get; set; }
        /// <summary>
        /// Date of release of the movie
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Link to the poster image of the movie
        /// </summary>
        public string? PosterLink { get; set; }

        public Movie(string movieName, DateTime releaseDate, string? posterLink)
        {
            Id = Guid.NewGuid();
            Name = movieName;
            ReleaseDate = releaseDate;
            PosterLink = posterLink;
        }

        /// <summary>
        /// Empty constructor for EF
        /// </summary>
        public Movie()
        {

        }
    }
}
