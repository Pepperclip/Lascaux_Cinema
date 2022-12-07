using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Lascaux_Demo.Models.ViewModels
{
    /// <summary>
    /// Viewmodel for the timeslot object
    /// </summary>
    public class VM_TimeSlot
    {
        /// <summary>
        /// Starting date of the timeslot
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// Ending date of the timeslot
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// Name of the movie played in the slot
        /// </summary>
        public string MovieName { get; set; }

        public VM_TimeSlot(TimeSlot timeSlot, string movieName)
        {
            StartDate = timeSlot.StartDate.ToShortDateString();
            EndDate = timeSlot.EndDate.ToShortDateString();
            MovieName = movieName;
        }
    }
}
