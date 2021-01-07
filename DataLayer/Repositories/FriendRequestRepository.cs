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

        public void AddRequest(FriendRequest request)
        {
            items.Add(request);
            SaveChanges();
        }

        public bool FrienRequestOutgoing(int currentUser, int id)
        {
            List<FriendRequest> requests = items.Where((r) => r.SenderId == currentUser && r.ReceiverId == id).ToList();
            if (requests.Count >= 1)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool FrienRequestIncoming(int currentUser, int id)
        {
            List<FriendRequest> requests = items.Where((r) => r.ReceiverId == currentUser && r.SenderId == id).ToList();
            if (requests.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
