namespace DAL_Lascaux_Demo.Models.PostModels
{
    /// <summary>
    /// Postmodel for creating timeslot
    /// </summary>
    public class PM_TimeSlot
    {
        /// <summary>
        /// Startdate of the slot
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Enddate of the slot
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Id of the room
        /// </summary>
        public Guid RoomId { get; set; }
        /// <summary>
        ///  Id of the movie
        /// </summary>
        public Guid MovieId { get; set; }

    }
}
