using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(3)]
        public string NFLTeam { get; set; }
        [Required]
        [MaxLength(2)]
        public string Position { get; set; }

        public virtual List<Pick> Picks { get; set; }
        public virtual PlayerRank PlayerRank { get; set; }
        public List<TradeItem> TradeItems { get; set; }
        public List<Keeper> Keepers { get; set; }
    }
}
