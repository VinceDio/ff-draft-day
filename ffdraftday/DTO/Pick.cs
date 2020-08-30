using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.DTO
{
    public class Pick
    {
        public int OverallPick { get; set; }
        public int Round { get; set; }
        public int Selection { get; set; }
        public Team Team { get; set; }
        public string Note { get; set; }
        public Player Player { get; set; }
        public bool IsKeeper { get; set; }
        public bool AutoPick { get; set; }
    }
}
