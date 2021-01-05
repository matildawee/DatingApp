using DataLayer.Models;
using System;
using System.Collections.Generic;
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
    }
}
