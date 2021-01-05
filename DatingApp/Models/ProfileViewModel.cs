using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class ProfileViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public PostUserViewModel Posts { get; set; }
    }
}
