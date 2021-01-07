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

        public List<FriendRequest> GetFriendsByPersonId(int id)
        {
            return items.Where((p) => p.SenderId.Equals(id) || p.ReceiverId.Equals(id) && p.Accepted.Equals(true)).ToList();
        }

        public bool IsFriends( int loggedInUser, int id)
        {
            List<FriendRequest> requests = items.Where((r) => r.SenderId.Equals(loggedInUser) && r.ReceiverId.Equals(id) && r.Accepted.Equals(true)).ToList();
            List<FriendRequest> requests2 = items.Where((r) => r.SenderId.Equals(id) && r.ReceiverId.Equals(loggedInUser) && r.Accepted.Equals(true)).ToList();
            if (requests.Count > 0 || requests2.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<FriendRequest> GetAllRequestsSentToUser(int id)
        {
            return items.Where((r) => r.ReceiverId.Equals(id) && r.Accepted.Equals(false)).ToList();
        }

        public List<FriendRequest> GetAllRequestsSentByUser(int id)
        {
            return items.Where((r) => r.SenderId.Equals(id) && r.Accepted.Equals(false)).ToList();
        }

        public void DeleteFriendOrRequest(FriendRequest request)
        {
            items.Remove(request);
            SaveChanges();
        }

        public void AddRequest(FriendRequest request)
        {
            items.Add(request);
            SaveChanges();
        }

        public bool FriendRequestOutgoing(int currentUser, int id)
        {
            List<FriendRequest> requests = items.Where((r) => r.SenderId == currentUser && r.ReceiverId == id && r.Accepted.Equals(false)).ToList();
            if (requests.Count >= 1)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool FriendRequestIncoming(int currentUser, int id)
        {
            List<FriendRequest> requests = items.Where((r) => r.ReceiverId == currentUser && r.SenderId == id && r.Accepted.Equals(false)).ToList();
            if (requests.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void AcceptRequest(FriendRequest friend)
        //{
        //   // items.Where((r) => r == friend).ToList().ForEach(f => f.Accepted = true);
        //    friend.Accepted = true;
        //    SaveChanges();
        //}
    }
}
