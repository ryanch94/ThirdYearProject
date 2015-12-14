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
        public string ShortName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DayBlock { get; set; }
        public string Code { get; set; }
        public int ClassLength { get; set; }
        public int WeekDayNumber { get; set; }



        public DailyClass(int id, string nme, string shortnme, string frstnme, string lstnme, int dyblck, string cd, int clssL, int dynmbr)
        {
            Id = id;
            Name = nme;
            ShortName = shortnme;
            FirstName = frstnme;
            LastName = lstnme;
            DayBlock = dyblck;
            Code = cd;
            ClassLength = clssL;
            WeekDayNumber = dynmbr;


        }
        public override string ToString()
        {
            return ShortName + " " + Code + " " + DayBlock;
        }

        public string Test
        {
            get
            { return ShortName + " " + Code + " " + DayBlock; }
        }
    }
}
