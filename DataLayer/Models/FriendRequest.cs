
namespace DataLayer.Models
{
    public class FriendRequest
    {
        public int SenderId { get; set; }
        public Person Sender { get; set; }

        public int ReceiverId { get; set; }
        public Person Receiver { get; set; }

        public bool Accepted { get; set; }
    }
}
