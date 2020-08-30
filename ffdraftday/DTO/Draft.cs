using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.DTO
{
    public class Draft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Commissioner { get; set; }
        public string Location { get; set; }
        public int NumberOfTeams { get; set; }
        public int Rounds { get; set; }
        public int ClockSeconds { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public int CurrentPick { get; set; }
        public List<Team> Teams { get; set; }
        public List<RosterPosition> RosterPositions { get; set; }
    }
}
