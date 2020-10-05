using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class ResultOfMethod
    {
        public bool Success;
        public string ErrorMessage;

        public ResultOfMethod()
        {

        }

        public ResultOfMethod(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }
}
