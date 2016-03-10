using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Models
{
    public class ProgramType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ProgramType(int id, string desc)
        {
            Id = id;
            Description = desc;
        }
        public string test
        {
            get { return Id + Description; }
        }

    }

    public class RoomType
    {
        public byte Id { get; set; }
        public string Type { get; set; }

        public RoomType(byte id, string type)
        {
            Id = id;
            Type = type;
        }

        public RoomType()
        { }

        public string test
        {
            get { return Type + Id; }

        }
    }

    public class WeekDay
    {
        public string DayName { get; set; }
        public byte DayNumber { get; set; }

        public WeekDay() { }

        public WeekDay(string day, byte num)
        {
            DayName = day;
            DayNumber = num;
        }
    }

    public class DayBlockTime
    {
        public string DayTime { get; set; }
        public byte BlockNum { get; set; }

        public DayBlockTime() { }

        public DayBlockTime(string time, byte block)
        {           
            DayTime = time;
            BlockNum = block;
        }
    }


}
