using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        
        public DateTime Timestamp { get; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int AuthorId { get; set; }
        public Person Author { get; set; }

        [StringLength(200)]
        public string PostText { get; set; }
    }
}
