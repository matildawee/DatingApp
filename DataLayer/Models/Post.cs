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

        //DateTime localDate = DateTime.Now;
        //public DateTime Timestamp = DateTime.Now;
        public DateTime Timestam { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int AuthorId { get; set; }
        public Person Author { get; set; }

        [StringLength(200)]
        public string PostText { get; set; }
    }
}
