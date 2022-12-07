using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Lascaux_Demo.Models.ViewModels
{
    public class VM_RoomProgramming
    {
        /// <summary>
        /// Name of the room
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// List of related viewmodel slots
        /// </summary>
        public List<VM_TimeSlot> TimeSlots { get; set; }

        public VM_RoomProgramming(string roomName, List<VM_TimeSlot> timeSlots)
        {
            RoomName = roomName;
            TimeSlots = timeSlots;
        }
    }
}
