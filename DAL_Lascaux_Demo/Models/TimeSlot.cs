using System.ComponentModel.DataAnnotations;
using DAL_Lascaux_Demo.Models.PostModels;

namespace DAL_Lascaux_Demo.Models
{
    public class TimeSlot
    {
        /// <summary>
        /// Unique Id of the timeslot
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Starting date of the slot
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Ending date of the slot
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Id of the room
        /// </summary>
        public Guid RoomId { get; set; }
        /// <summary>
        /// Id of the movie
        /// </summary>
        public Guid MovieId { get; set; }

        /// <summary>
        /// Create a timeslot for a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="weeksOfPermanence"></param>
        /// <param name="roomId"></param>
        public TimeSlot(Movie movie, int weeksOfPermanence, Guid roomId)
        {
            if (weeksOfPermanence >= 1 && weeksOfPermanence <= 3)
            {
                Id = Guid.NewGuid();
                StartDate = movie.ReleaseDate;
                EndDate = StartDate.AddDays(weeksOfPermanence * 7);
                MovieId = movie.Id;
                RoomId = roomId;
            }
        }

        /// <summary>
        /// Empty constructor for EF
        /// </summary>
        public TimeSlot()
        {

        }

        /// <summary>
        /// Create a timeslot from a postmodel
        /// </summary>
        /// <param name="pm_timeSlot"></param>
        public TimeSlot(PM_TimeSlot pm_timeSlot)
        {
            Id = Guid.NewGuid();
            StartDate = pm_timeSlot.StartDate;
            EndDate = pm_timeSlot.EndDate;
            MovieId = pm_timeSlot.MovieId;
            RoomId =  pm_timeSlot.RoomId;    
        }


        /// <summary>
        /// Creates a timeslot for the same movie in multiple rooms 
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="weeksOfPermanence"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
        public static List<TimeSlot> CreateMultiRoomSlot(Movie movie, int weeksOfPermanence, List<CinemaRoom> rooms)
        {
            List<TimeSlot> _myTimeSlots = new();

            foreach (CinemaRoom room in rooms)
            {
                TimeSlot slot = new (movie, weeksOfPermanence, room.Id);
                _myTimeSlots.Add(slot);
            }

            return _myTimeSlots;
        }
    }
}
