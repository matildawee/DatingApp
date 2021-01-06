using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class FriendRepository : Repository<Friend>
    {
        public FriendRepository(DatingAppContext context) : base(context) { }

        public void AddFriend(Friend friend)
        {
            items.Add(friend);
            SaveChanges();
        }

        public List<Friend> GetAllFriendsByPersonId(int id)
        {
            //List<Friend> friendlist = items.Where((p) => p.FirstPersonId.Equals(id)).ToList();
            //List<Friend> friendlist2 = items.Where((p) => p.SecondPersonId == (id)).ToList();

            //foreach (var item in friendlist)
            //{
            //    friendlist2.Add(item);
            //}

            //return friendlist2; //.OrderByDescending((p) => p.LastName);
            
            return items.Where((p) => p.FirstPersonId.Equals(id)).ToList();

            //return items.Where(p => items.All(p => p.FirstPersonId.Equals(id))).ToList();
        }
           
        public bool IsFriends(int currentUser, int id)
        {
            List<Friend> friends = items.Where((f) => f.FirstPersonId == currentUser && f.SecondPersonId == id).ToList();
            if(friends.Count > 0)
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
