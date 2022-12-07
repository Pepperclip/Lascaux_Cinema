using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Lascaux_Demo.Models.ViewModels
{
    public class VM_SearchResult
    {
        /// <summary>
        /// Name of the movie
        /// </summary>
        public string MovieName { get; set; }
        /// <summary>
        /// List of related timeslots
        /// </summary>
        public List<string> RelatedSlots { get; set; }
        /// <summary>
        /// Link to the poster of the movie
        /// </summary>
        public string? MoviePoster { get; set; }

        public VM_SearchResult(string movieName, List<string> relatedSlots, string? moviePoster)
        {
            MovieName = movieName;
            RelatedSlots = relatedSlots;
            MoviePoster = moviePoster;
        }
    }
}