using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Position
    {
        public string Code;
        public int Sequence;

        public Position(string code, int seq)
        {
            Code = code;
            Sequence = seq;
        }
    }
}
