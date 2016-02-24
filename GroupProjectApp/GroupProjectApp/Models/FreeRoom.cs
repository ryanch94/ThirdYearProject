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
        public int Count { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return "Room:" + Code + "  " + Type;
        }

        public string Test
        {
            get { return "Room:" + Code + "  " + Type; }
        }
    }
}
