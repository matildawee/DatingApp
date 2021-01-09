using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class ProfileViewModel
    {
        public int PersonId { get; set; }

        [DisallowNull]
        [Required(ErrorMessage = "Enter first name with uppercase first letter")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string FirstName { get; set; }

        [DisallowNull]
        [Required(ErrorMessage = "Enter last name with uppercase first letter")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
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
