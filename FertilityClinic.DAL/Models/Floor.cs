using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Models
{
    public class Floor
    {
        public int FloorId { get; set; }
        public int SectionId { get; set; }
        public int FloorNumber { get; set; }
        public virtual Section Section { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
