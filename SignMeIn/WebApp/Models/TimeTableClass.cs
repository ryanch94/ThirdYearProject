using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    class TimeTableClass
    {
        public string Name { get; set; }
        public string RoomCode { get; set; }
        public string StartTime { get; set; }
        public int LengthHours { get; set; }
        public short Day { get; set; }
    }
}
