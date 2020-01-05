using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class RosterPosition
    {
        public int Id { get; set; }
        public int DraftId { get; set; }
        public int Sequence { get; set; }
        [Required]
        public string Position { get; set; }

        public virtual Draft Draft { get; set; }
    }
}
