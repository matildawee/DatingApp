using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Post
    {
        public int PostId { get; set; }

        
        public DateTime Timestamp { get; }
        public int UserId { get; set; }
        public User UserPerson { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string PostText { get; set; }
    }
}
