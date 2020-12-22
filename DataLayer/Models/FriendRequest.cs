using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class FriendRequest
    {
        [Key]
        public int FriendRequestId { get; set; }

        public int SenderId { get; set; }

        public Person Sender { get; set; }
        public int ReceiverId { get; set; }

        public Person Recevier { get; set; }
    }
}
