using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Keeper
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public int Round { get; set; }
        public string Note { get; set; }

        public Team Team { get; set; }
        public Player Player { get; set; }
    }
}
