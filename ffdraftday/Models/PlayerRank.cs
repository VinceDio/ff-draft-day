using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class PlayerRank
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        [Required]
        public int Year { get; set; }
        public int Bye { get; set; }
        [Required]
        public int Rank { get; set; }
        public virtual Player Player { get; set; }
    }
}
