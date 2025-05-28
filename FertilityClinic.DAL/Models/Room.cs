using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int FloorId { get; set; }
        public int RoomNumber { get; set; }
        public virtual Floor Floor { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
