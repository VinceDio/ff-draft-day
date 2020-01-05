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
    }
}
