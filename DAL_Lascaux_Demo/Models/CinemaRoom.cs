using System.ComponentModel.DataAnnotations;

namespace DAL_Lascaux_Demo.Models
{
    public class CinemaRoom
    {
        /// <summary>
        /// unique Id of the room
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the room
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// Amount of seats in the room
        /// </summary>
        public int SeatingCapacity { get; set; }
        /// <summary>
        /// Type of room
        /// </summary>
        public RoomTypes RoomType { get; set; }

        public CinemaRoom(string roomName, int seatingCapacity, RoomTypes roomType )
        {
            Id = Guid.NewGuid();
            RoomName = roomName;
            SeatingCapacity = seatingCapacity;
            RoomType = roomType;
        }

        /// <summary>
        /// Empty constructor for EF
        /// </summary>
        public CinemaRoom()
        {

        }

    }

    /// <summary>
    /// Enum of possible types of cinema room
    /// </summary>
    public enum RoomTypes
    {
        Regular,
        IMAX,
    }
}

