using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataLayer.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [DisallowNull]
        public string Email { get; set; }

        [DisallowNull]
        [Required(ErrorMessage = "Fyll i namn!")]
        public string FirstName { get; set; }

        [DisallowNull]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")] //Första bokstaven måste vara stor bokstav, och efterföljande tecken måste vara bokstäver
        public string LastName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Picture { get; set; }

        //public List<Post> Posts { get; set; }
    }
}
