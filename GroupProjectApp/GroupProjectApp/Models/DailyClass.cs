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
            return  Name + "   " + "Room:" + Code;
        }

        public string Test
        {
            get { return Name + "   " + "Room:" + Code; }
        }
    }

    public class ValidAuth
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class InvalidAuth
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
