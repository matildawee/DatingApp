using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [DisallowNull]
        public string Email { get; set; }

        [DisallowNull]
        [Required]
        [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")] //Anger att första bokstaven måste vara stor bokstav, och efterföljande tecken måste vara bokstäver
        public string FirstName { get; set; }

        [DisallowNull]
        [Required]
        [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")] //Anger att första bokstaven måste vara stor bokstav, och efterföljande tecken måste vara bokstäver
        public string LastName { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public List<FriendRequest> FriendRequestSent  { get; set; }
        public List<FriendRequest> FriendRequestReceived { get; set; }

        public bool AccountHidden { get; set; }
    }
}
