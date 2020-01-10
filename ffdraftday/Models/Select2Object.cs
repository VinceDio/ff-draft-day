using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Select2Object
    {
        public string id { get; set; }
        public string text { get; set; }

        public Select2Object(object initId, string initText)
        {
            id = initId.ToString();
            text = initText;
        }
    }
}
