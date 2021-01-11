using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public Person Author { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "The field must be between 1 and 300 characters")]
        public string PostText { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class PostUserViewModel
    {
        public int PersonId { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
