using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class FriendViewModel
    {
        public Person FirstPerson { get; set; }
        public Person SecondPerson { get; set; }

    }

    public class FriendUserViewModel
    {
        public int PersonId { get; set; }
        public List<FriendViewModel> Friends { get; set; }
    }
}