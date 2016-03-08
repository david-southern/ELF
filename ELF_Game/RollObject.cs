using System;
using System.Collections.Generic;
using System.Text;

namespace ELF
{
    class RollObject
    {
        public Random rand { get; set; }
        public int timer { get; set; }

        public RollObject(int t)
        {
            timer = t;
            rand = new Random();
        }
    }
}
