using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models.Users
{
    public class UserViewModelOutput
    {
        public int Code { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
    }
}
