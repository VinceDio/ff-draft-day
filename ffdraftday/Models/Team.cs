using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DraftId { get; set; }
        public string Owner { get; set; }
        public int DraftPosition { get; set; }

        public virtual List<Pick> Picks { get; set; }
        public virtual Draft Draft { get; set; }
        public List<TradeItem> TradeItems { get; set; }
        public List<Trade> Trades1 { get; set; }
        public List<Trade> Trades2 { get; set; }
    }
}
