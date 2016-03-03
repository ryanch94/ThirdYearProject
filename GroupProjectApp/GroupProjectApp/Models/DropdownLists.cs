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
            get { return Id + Description ; }
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

        //public override string ToString()
        //{
        //    return Type + Id;
        //}
    }


   
}
