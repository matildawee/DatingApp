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
        [Required(ErrorMessage = "Enter first name with uppercase first letter")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")] //Anger att första bokstaven måste vara stor bokstav, och efterföljande tecken måste vara bokstäver
        public string FirstName { get; set; }

        [DisallowNull]
        [Required(ErrorMessage = "Enter last name with uppercase first letter")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")] //Anger att första bokstaven måste vara stor bokstav, och efterföljande tecken måste vara bokstäver
        public string LastName { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public List<FriendRequest> FriendRequestSent  { get; set; }
        public List<FriendRequest> FriendRequestReceived { get; set; }

        public bool AccountHidden { get; set; }
    }
}
