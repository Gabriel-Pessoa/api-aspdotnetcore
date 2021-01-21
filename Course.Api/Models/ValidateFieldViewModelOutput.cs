using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class ValidateFieldViewModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public ValidateFieldViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
