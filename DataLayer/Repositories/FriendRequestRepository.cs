using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class FriendRequestRepository : Repository<FriendRequest>
    {
        public FriendRequestRepository(DatingAppContext context) : base(context) { }

        public FriendRequest GetFriendRequestById(int id)
        {
            return items.FirstOrDefault((r) => r.FriendRequestId == id);
        }

        public List<FriendRequest> GetAllRequestsSentToUser(int id)
        {
            return items.Where((r) => r.ReceiverId.Equals(id)).ToList();
        }

        public void DeleteRequest(int id)
        {
            FriendRequest requestToDelete = GetFriendRequestById(id);
            items.Remove(requestToDelete);
            SaveChanges();
        }
    }
}
