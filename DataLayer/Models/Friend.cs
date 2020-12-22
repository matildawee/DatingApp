using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class Friend
    {
        [Key]
        [ForeignKey("FirstPerson")]
        public int FirstPersonId { get; set; }
        public Person FirstPerson { get; set; }

        
        [ForeignKey("SecondPerson")]
        public int SecondPersonId { get; set; }
        public Person SecondPerson { get; set; }
    }
}
