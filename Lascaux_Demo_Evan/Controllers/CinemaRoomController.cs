using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BAL_Lascaux_Demo;

namespace Lascaux_Demo_Evan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaRoomController : ControllerBase
    {
        private readonly CinemaRoomService _roomService;
        public CinemaRoomController(CinemaRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Gets all the rooms in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllRooms")]
        public IActionResult GetAllRooms()
        {
            return new OkObjectResult(_roomService.GetAllRooms());
        }
    }
}
