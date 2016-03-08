using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Models
{

    public class FreeRoom
    {
        public string Code { get; set; }
        public int Size { get; set; }
        public byte Count { get; set; }
        public string Type { get; set; }

        public FreeRoom(string code, int size, byte count, string type)
        {
            Code = code;
            Size = size;
            Count = count;
            Type = type;
        }

        public string test
        {
            get { return Code + Size + Count + Type; }
        }
    }

    public class WatchedRoom
    {
        public string Code { get; set; }

        public WatchedRoom() { }

        public WatchedRoom(string code) { Code = code; }

        public string test
        {
            get { return Code; }
        }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Size { get; set; }
        public byte Count { get; set; }
        public string Type { get; set; }

        public Room(int id,string code, int size, byte count, string type)
        {
            Id = id;
            Code = code;
            Size = size;
            Count = count;
            Type = type;
        }

        public string test
        {
            get { return Id + Code + Size + Count + Type; }
        }
    }

}
