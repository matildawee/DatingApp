using DataLayer.Models;
using System.Collections.Generic;
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