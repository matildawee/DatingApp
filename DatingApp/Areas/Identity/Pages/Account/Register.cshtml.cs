using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DatingApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly DatingAppContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            DatingAppContext context,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public InputModelDetails Input2 { get; set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public class InputModelDetails
        {
            [Required]
            [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [RegularExpression(@"^[a-öA-Ö]+$", ErrorMessage = "Enter first name with only letters (A-Ö)")]
            [Display(Name = "Last name")]
            public string LastName { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, Input.Password);

                byte[] imageData = null;
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string path = wwwrootPath + "/img/default_profile_picture.jpg"; //Hämtar path för default-bild

                FileStream file = new FileStream(path, FileMode.Open);

                using (var binary = new BinaryReader(file))
                {
                    imageData = binary.ReadBytes((int)file.Length); 
                }
                if (result.Succeeded)
                {
                    var firstname = Input2.FirstName;
                    var lastname = Input2.LastName;

                    var person = new Person
                    {
                        Email = Input.Email,
                        FirstName = firstname,
                        LastName = lastname,
                        Description = "",
                        Picture = imageData,
                        AccountHidden = false
                    };
                    _context.Persons.Add(person);
                    _context.SaveChanges();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("MyProfile", "Person");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
