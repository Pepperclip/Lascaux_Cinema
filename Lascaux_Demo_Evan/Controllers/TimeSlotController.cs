using BAL_Lascaux_Demo;
using DAL_Lascaux_Demo.Models.PostModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lascaux_Demo_Evan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly TimeSlotService _timeSlotService;

        public TimeSlotController(TimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        /// <summary>
        /// Gets timeslots related to the query
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Searchresults</returns>
        [HttpGet("searchTimeSlots")]
        public IActionResult SearchTimeSlots(string query)
        {
            return new OkObjectResult(_timeSlotService.SearchVMSlots(query));
        }

        /// <summary>
        /// Creates a timeslot
        /// </summary>
        /// <param name="pm_timeSlot"></param>
        /// <returns>Timeslot object</returns>
        [HttpPost("createTimeSlot")]
        public IActionResult CreateTimeSlot(PM_TimeSlot pm_timeSlot)
        {
            return _timeSlotService.CreateSlot(pm_timeSlot);
        }

        /// <summary>
        /// Gets viewmodel timeslots by date
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns>List of timeslot viewmodels</returns>
        [HttpGet("VMtimeSlotsByDate")]
        public IActionResult SearchTimeSlots(DateTime? minDate, DateTime? maxDate)
        {
            //Return programming after given date until forever
            if (minDate != null && maxDate == null)
            {
                return new OkObjectResult(_timeSlotService.GetVMSlotsAfterDate(minDate.Value));
            }
            //Return programming from lowest date recorded until given date
            else if (minDate == null && maxDate != null)
            {
                return new OkObjectResult(_timeSlotService.GetVMSlotsBeforeDate(maxDate.Value));
            }
            //Return programming between given dates
            //Extra check to see if the maxdate is actually after the mindate
            else if (minDate != null && maxDate != null)
            {
                if ((maxDate.Value - minDate.Value).Days > 0)
                {
                    return new OkObjectResult(_timeSlotService.GetVMSlotsInRange(minDate.Value, maxDate.Value));
                }
                else
                {
                    return new BadRequestObjectResult("Error: maximum date must be after minimum date!");
                }

            }
            //No dates specified = return programming between today until forever (standard overview)
            else
            {
                return new OkObjectResult(_timeSlotService.GetVMSlotsSinceToday());
            }

        }

        /// <summary>
        /// Gets timeslots by date
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns>List of timeslot viewmodels</returns>
        [HttpGet("timeSlotsByDate")]
        public IActionResult GetTimeSlotsByDate(DateTime? minDate, DateTime? maxDate)
        {
            //Return programming after given date until forever
            if (minDate != null && maxDate == null)
            {
                return new OkObjectResult(_timeSlotService.GetSlotsAfterDate(minDate.Value));
            }
            //Return programming from lowest date recorded until given date
            else if (minDate == null && maxDate != null)
            {
                return new OkObjectResult(_timeSlotService.GetSlotsBeforeDate(maxDate.Value));
            }
            //Return programming between given dates
            //Extra check to see if the maxdate is actually after the mindate
            else if (minDate != null && maxDate != null)
            {
                if ((maxDate.Value - minDate.Value).Days > 0)
                {
                    return new OkObjectResult(_timeSlotService.GetSlotsInRange(minDate.Value, maxDate.Value));
                }
                else
                {
                    return new BadRequestObjectResult("Error: maximum date must be after minimum date!");
                }

            }
            //No dates specified = return programming between today until forever (standard overview)
            else
            {
                return new OkObjectResult(_timeSlotService.GetSlotsSinceToday());
            }
        }
    }
}
