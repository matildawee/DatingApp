﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DatingApp.Models
{
    public class ProfileViewModel
    {
        public int PersonId { get; set; }

        [Required]
        [DisallowNull]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")]
        public string FirstName { get; set; }

        [Required]
        [DisallowNull]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")]
        public string LastName { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public string Email { get; set; }

        public bool AccountHidden { get; set; }
        public PostUserViewModel Posts { get; set; }
        public FriendUserViewModel Friends { get; set; }
    }
}
