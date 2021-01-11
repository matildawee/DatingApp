using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime Timestamp { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int AuthorId { get; set; }
        public Person Author { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "The field must be between 1 and 300 characters")]
        public string PostText { get; set; }
    }
}
