using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Keeper
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        [Range(1,20)]
        public int Round { get; set; }
        [Range(0,5)]
        public int YearsRemaining { get; set; }
        public string Note { get; set; }

        public Team Team { get; set; }
        public Player Player { get; set; }
    }
}
