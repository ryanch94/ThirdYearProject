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
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DayBlock { get; set; }
        public int WeekDayNumber { get; set; }
        public string Code { get; set; }


        public DailyClass(int id, string nme,string frstnme, string lstnme, int dyblck, int dynmbr, string cd)
        {
            Id = id;
            Name = nme;
            FirstName = frstnme;
            LastName = lstnme;
            DayBlock = dyblck;
            WeekDayNumber = dynmbr;
            Code = cd;

        }
        public override string ToString()
        {
            return Name + " " + Code + " " + DayBlock;
        }

        public string Test
        {
            get
            { return Name + " " + Code + " " + DayBlock; }
        }
    }
}
