using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public byte[] Picture { get; set; }

        public string Email { get; set; }

        public bool AccountHidden { get; set; }
        public PostUserViewModel Posts { get; set; }
        public FriendUserViewModel Friends { get; set; }
    }
}
