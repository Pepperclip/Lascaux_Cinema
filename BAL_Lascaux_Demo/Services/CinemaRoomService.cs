using DAL_Lascaux_Demo.Data;
using DAL_Lascaux_Demo.Models;

namespace BAL_Lascaux_Demo;

public class CinemaRoomService
{
    public readonly CinemaDbContext _context;

    public CinemaRoomService(CinemaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the requested room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns>Cinemaroom object or null</returns>
    public CinemaRoom? GetRoomInfo(Guid roomId)
    {
        return _context.CinemaRooms.Where(room => room.Id.Equals(roomId)).FirstOrDefault();
    }

    /// <summary>
    /// Checks if a room with the given Id exists
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns>Boolean</returns>
    public bool RoomExists(Guid roomId)
    {
        return _context.CinemaRooms.Where(room => room.Id.Equals(roomId)).Any();
    }

    /// <summary>
    /// Gets all the rooms of the cinema
    /// </summary>
    /// <returns>A list of cinemarooms</returns>
    public List<CinemaRoom> GetAllRooms()
    {
        return _context.CinemaRooms.ToList();
    }
}
