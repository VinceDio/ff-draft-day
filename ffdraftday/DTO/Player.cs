using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.DTO
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string NFLTeam { get; set; }
        public int Bye { get; set; }
        public int Rank { get; set; }
    }
}
