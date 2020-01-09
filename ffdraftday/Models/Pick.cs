using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Pick
    {
        public int Id { get; set; }
        public int DraftId { get; set; }
        [Required]
        [Range(1,20)]
        public int Round { get; set; }
        [Required]
        [Range(1,20)]
        public int Selection { get; set; }
        public int OverallPick { get; set; }
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public bool IsKeeper { get; set; }
        public string Note { get; set; }
        public bool AutoPick { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Draft Draft { get; set; }
        public virtual Team Team { get; set; }
        public virtual Player Player { get; set; }
    }
}
