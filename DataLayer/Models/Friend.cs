using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Friend
    {
        public int FirstUserId { get; set; }
        public User FirstUser { get; set; }

        public int SecondUserId { get; set; }
        public User SecondUser { get; set; }
    }
}
