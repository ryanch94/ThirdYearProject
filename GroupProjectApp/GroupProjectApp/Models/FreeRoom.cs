using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Models
{
    class FreeRoom
    {
        //"[{\"Code\":\"A0004\",\"Size\":132,\"Count\":0,\"Type\":\"Lecture Hall\"},
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
}
