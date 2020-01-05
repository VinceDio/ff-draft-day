using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Draft
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Commissioner { get; set; }
        public string Location { get; set; }
        [Range(4,12)]
        public int NumberOfTeams { get; set; }
        public int Rounds { get; set; }
        [Range(15,120)]
        public int ClockSeconds { get; set; }
        [Required]
        public DateTime StartTime { get; set; }

        public virtual List<Team> Teams { get; set; }
        public virtual List<Pick> Picks { get; set; }
        public virtual List<RosterPosition> RosterPositions { get; set; }
    }
}
