using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class RequestViewModel
    {
        public int FriendRequestId { get; set; }
        public int SenderId { get; set; }
        public Person Sender { get; set; }
        public string FullName { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
