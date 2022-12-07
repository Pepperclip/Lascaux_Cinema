using DAL_Lascaux_Demo.Data;
using DAL_Lascaux_Demo.Models;
using DAL_Lascaux_Demo.Models.PostModels;
using DAL_Lascaux_Demo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BAL_Lascaux_Demo
{
    public class TimeSlotService
    {
        private readonly CinemaDbContext _context;
        private readonly CinemaRoomService _roomService;
        private readonly MovieService _movieService;

        public TimeSlotService(CinemaDbContext context, CinemaRoomService cinemaRoomService, MovieService movieService)
        {
            _context = context;
            _roomService = cinemaRoomService;
            _movieService = movieService;
        }

        #region Slots
        /// <summary>
        /// Gets al the TimeSlots after the current date
        /// </summary>
        /// <returns>List of Viewmodels that contains timeslots per room</returns>
        public List<VM_RoomProgramming> GetSlotsSinceToday()
        {
            List<VM_RoomProgramming> programming = new();

            foreach (CinemaRoom room in _context.CinemaRooms)
            {
                //Get the timeslots that are either today or in the future
                List<TimeSlot> slots = _context.TimeSlots.Where(slot => slot.RoomId.Equals(room.Id)
                && slot.EndDate.CompareTo(DateTime.Now) >= 0).ToList();

                List<VM_TimeSlot> vmSlots = ToVMTimeSlots(slots);

                //Put the timeslots in the viewmodel
                VM_RoomProgramming _RoomProg = new(room.RoomName, vmSlots);
                programming.Add(_RoomProg);
            }

            return programming;
        }

        /// <summary>
        /// Gets al the TimeSlots between two given dates
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns>List of Viewmodels that contains timeslots per room</returns>
        public List<VM_RoomProgramming> GetSlotsInRange(DateTime minDate, DateTime maxDate)
        {
            List<VM_RoomProgramming> programming = new();

            foreach (CinemaRoom room in _context.CinemaRooms)
            {
                //Get the timeslots that are between min and max date parameters
                List<TimeSlot> slots = _context.TimeSlots.Where(slot =>
                //Slot's start date is later than or equal to the given minimum date
                slot.EndDate.CompareTo(minDate) >= 0 && maxDate.CompareTo(slot.StartDate) >= 0
                ).ToList();

                List<VM_TimeSlot> vmSlots = ToVMTimeSlots(slots);

                //Put the timeslots in the viewmodel
                VM_RoomProgramming _RoomProg = new(room.RoomName, vmSlots);
                programming.Add(_RoomProg);
            }

            return programming;
        }

        /// <summary>
        /// Gets al the TimeSlots before given date
        /// </summary>
        /// <param name="maxDate"></param>
        ///<returns>List of Viewmodels that contains timeslots per room</returns>
        public List<VM_RoomProgramming> GetSlotsBeforeDate(DateTime maxDate)
        {
            List<VM_RoomProgramming> programming = new();

            foreach (CinemaRoom room in _context.CinemaRooms)
            {

                List<TimeSlot> slots = _context.TimeSlots.Where(slot =>
                //Slot's start date is earlier than or equal to the given maximum date
                slot.StartDate.CompareTo(maxDate) >= 0).ToList();

                //Convert the slots to the viewmodel objects
                List<VM_TimeSlot> vmSlots = ToVMTimeSlots(slots);

                //Put the timeslots in the viewmodel
                VM_RoomProgramming _RoomProg = new(room.RoomName, vmSlots);
                programming.Add(_RoomProg);
            }

            return programming;
        }

        /// <summary>
        /// Gets al the TimeSlots after the given date
        /// </summary>
        /// <param name="minDate"></param>
        /// <returns>List of Viewmodels that contains timeslots per room</returns>
        public List<VM_RoomProgramming> GetSlotsAfterDate(DateTime minDate)
        {
            List<VM_RoomProgramming> programming = new();

            foreach (CinemaRoom room in _context.CinemaRooms)
            {

                List<TimeSlot> slots = _context.TimeSlots.Where(slot =>
                //Slot's start date is later than or equal to the given minimum date
                slot.StartDate.CompareTo(minDate) >= 0).ToList();

                //Convert the slots to the viewmodel objects
                List<VM_TimeSlot> vmSlots = ToVMTimeSlots(slots);

                //Put the timeslots in the viewmodel
                VM_RoomProgramming _RoomProg = new(room.RoomName, vmSlots);
                programming.Add(_RoomProg);
            }

            return programming;
        }
        #endregion

        #region Viewmodel_Slots

        /// <summary>
        /// Converts TimeSlot objects to timeslot Viewmodels
        /// </summary>
        /// <param name="timeSlots"></param>
        /// <returns>List of timeslot viewmodels</returns>
        public List<VM_TimeSlot> ToVMTimeSlots(List<TimeSlot> timeSlots)
        {
            List<VM_TimeSlot> _vMs = new();

            //every timeslot is converted to the viewmodel equivalent, name of movie is added
            foreach (TimeSlot slot in timeSlots)
            {
                Movie? myMovie = _context.Movies.Where(movie => movie.Id.Equals(slot.MovieId)).FirstOrDefault();

                if (myMovie != null)
                {
                    VM_TimeSlot _mySlot = new(slot, myMovie.Name);
                    _vMs.Add(_mySlot);
                }
            }

            //Sort by startdate before returning
            return _vMs.OrderBy(x => x.StartDate).ToList();
        }

        /// <summary>
        /// Searches in movie titles and finds related timeslots
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A viewmodel object of timeslots grouped by movie</returns>
        public List<VM_SearchResult> SearchVMSlots(string query)
        {
            //Everything is made lowercase so cases always match
            query = query.ToLower();

            //Find movies related to the query
            List<Movie> relatedMovies = _context.Movies.Where(movie =>
                //The searchterm is inside the name of the movie
                movie.Name.ToLower().Contains(query) ||
                //The name of the movie is inside the searchterm
                query.Contains(movie.Name.ToLower())
                ).ToList();

            //Get just the id's of the movies
            List<Guid> relatedMovieIds = relatedMovies.Select(movie => movie.Id).ToList();

            //Find timeslots for the related movies
            List<TimeSlot> relatedSlots = _context.TimeSlots.Where(slot => relatedMovieIds.Contains(slot.MovieId)).ToList();

            //Every movie is transformed into a viewmodel with all the information about dates and rooms etc.
            List<VM_SearchResult> searchResults = new();

            //Convert all resuults to the result viewmodel 
            foreach (Movie movie in relatedMovies)
            {
                List<TimeSlot> movieSlots = relatedSlots.Where(slot => slot.MovieId.Equals(movie.Id)).ToList();

                List<string> descriptions = ToDescription(movieSlots);

                VM_SearchResult myRes = new(movie.Name, descriptions, movie.PosterLink);
                searchResults.Add(myRes);
            }

            return (searchResults);
        }

        public List<VM_SearchResult> GetVMSlotsSinceToday()
        {
            //Search which movies will be played in the upcoming timeslots 
            List<TimeSlot> relatedSlots = _context.TimeSlots.Where(slot => slot.EndDate.CompareTo(DateTime.Now) >= 0).ToList();

            //Get the movie id's
            List<Guid> relatedMovies_Ids = relatedSlots.Select(slot => slot.MovieId).ToList();

            //Get the movies related to the id's
            List<Movie> relatedMovies = _context.Movies.Where(movie => relatedMovies_Ids.Contains(movie.Id)).ToList();

            //Every movie is transformed into a viewmodel with all the information about dates and rooms etc.
            List<VM_SearchResult> searchResults = new();

            foreach (Movie movie in relatedMovies)
            {
                List<TimeSlot> movieSlots = relatedSlots.Where(slot => slot.MovieId.Equals(movie.Id)).ToList();

                List<string> descriptions = ToDescription(movieSlots);

                VM_SearchResult myRes = new(movie.Name, descriptions, movie.PosterLink);
                searchResults.Add(myRes);
            }

            return (searchResults);
        }

        public List<VM_SearchResult> GetVMSlotsAfterDate(DateTime minDate)
        {
            //Search which movies were played in the timeslots after the given date
            List<TimeSlot> relatedSlots = _context.TimeSlots.Where(slot => slot.StartDate.CompareTo(minDate) >= 0).ToList();

            //Get the movie id's
            List<Guid> relatedMovies_Ids = relatedSlots.Select(slot => slot.MovieId).ToList();

            //Get the movies related to the id's
            List<Movie> relatedMovies = _context.Movies.Where(movie => relatedMovies_Ids.Contains(movie.Id)).ToList();

            //Every movie is transformed into a viewmodel with all the information about dates and rooms etc.
            List<VM_SearchResult> searchResults = new();

            foreach (Movie movie in relatedMovies)
            {
                List<TimeSlot> movieSlots = relatedSlots.Where(slot => slot.MovieId.Equals(movie.Id)).ToList();

                List<string> descriptions = ToDescription(movieSlots);

                VM_SearchResult myRes = new(movie.Name, descriptions, movie.PosterLink);
                searchResults.Add(myRes);
            }

            return (searchResults);
        }

        public List<VM_SearchResult> GetVMSlotsBeforeDate(DateTime maxDate)
        {
            //Search which movies were played in the timeslots before the given date
            List<TimeSlot> relatedSlots = _context.TimeSlots.Where(slot => slot.StartDate.CompareTo(maxDate) >= 0).ToList();

            //Get the movie id's
            List<Guid> relatedMovies_Ids = relatedSlots.Select(slot => slot.MovieId).ToList();

            //Get the movies related to the id's
            List<Movie> relatedMovies = _context.Movies.Where(movie => relatedMovies_Ids.Contains(movie.Id)).ToList();

            //Every movie is transformed into a viewmodel with all the information about dates and rooms etc.
            List<VM_SearchResult> searchResults = new();

            foreach (Movie movie in relatedMovies)
            {
                List<TimeSlot> movieSlots = relatedSlots.Where(slot => slot.MovieId.Equals(movie.Id)).ToList();

                List<string> descriptions = ToDescription(movieSlots);

                VM_SearchResult myRes = new(movie.Name, descriptions, movie.PosterLink);
                searchResults.Add(myRes);
            }

            return (searchResults);
        }

        public List<VM_SearchResult> GetVMSlotsInRange(DateTime minDate, DateTime maxDate)
        {
            //Search which movies were played between the two dates 
            List<TimeSlot> relatedSlots = _context.TimeSlots.Where(slot =>
            slot.EndDate.CompareTo(minDate) >= 0 && maxDate.CompareTo(slot.StartDate) >= 0
                ).ToList();

            //Get the movie id's
            List<Guid> relatedMovies_Ids = relatedSlots.Select(slot => slot.MovieId).ToList();

            //Get the movies related to the id's
            List<Movie> relatedMovies = _context.Movies.Where(movie => relatedMovies_Ids.Contains(movie.Id)).ToList();

            //Every movie is transformed into a viewmodel with all the information about dates and rooms etc.
            List<VM_SearchResult> searchResults = new();

            foreach (Movie movie in relatedMovies)
            {
                List<TimeSlot> movieSlots = relatedSlots.Where(slot => slot.MovieId.Equals(movie.Id)).ToList();

                List<string> descriptions = ToDescription(movieSlots);

                VM_SearchResult myRes = new(movie.Name, descriptions, movie.PosterLink);
                searchResults.Add(myRes);
            }

            return (searchResults);
        }

        #endregion

        /// <summary>
        /// Transforms a timeslot object into a more readable description 
        /// of a movie's permanence in a particular room of the cinema
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>Description of timeslot</returns>
        public List<string> ToDescription(List<TimeSlot> slots)
        {
            List<string> allDescriptions = new();

            foreach (TimeSlot slot in slots)
            {

                CinemaRoom? myRoom = _roomService.GetRoomInfo(slot.RoomId);

                //If the room has IMAX it gest a special description
                if (myRoom != null)
                {
                    if (myRoom.RoomType.Equals(RoomTypes.IMAX))
                    {
                        allDescriptions.Add(
                        string.Format("In {0} da {1} a {2} in IMAX",
                        myRoom.RoomName, slot.StartDate.ToShortDateString(), slot.EndDate.ToShortDateString()));
                    }
                    else
                    {
                        allDescriptions.Add(
                        string.Format("In {0} da {1} a {2}",
                        myRoom.RoomName, slot.StartDate.ToShortDateString(), slot.EndDate.ToShortDateString()));
                    }

                }

            }

            return allDescriptions;
        }

        /// <summary>
        /// Creates a new timeslot object and saves it to the database
        /// </summary>
        /// <param name="pm_timeSlot"></param>
        /// <returns>New Timeslot object</returns>
        public IActionResult CreateSlot(PM_TimeSlot pm_timeSlot)
        {
            if (
            //Check if the movie exists
            _movieService.MovieExists(pm_timeSlot.MovieId) &&
            //Check if room exists
              _roomService.RoomExists(pm_timeSlot.RoomId))
            {
                TimeSlot mySlot = new(pm_timeSlot);

                try
                {
                    _context.TimeSlots.Add(mySlot);
                    _context.SaveChanges();

                    return new OkObjectResult(mySlot);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            else
            {
                return new BadRequestObjectResult("Il film o la sala usata non esistono");
            }
        }
    }

}
