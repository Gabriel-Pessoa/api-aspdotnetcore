using System.Collections.Generic;

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
