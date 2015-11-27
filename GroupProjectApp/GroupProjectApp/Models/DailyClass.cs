using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Classes
{
    public class DailyClass
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string LecturerFName { get; set; }
        public string LecturerLName { get; set; }
        public int DayBlock { get; set; }
        public int WeekDay { get; set; }
        public string RoomNumber { get; set; }

        public override string ToString()
        {
            return ModuleName + " " + RoomNumber;
        }
    }
}
