using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatingAppContext _context;

        private Random random;

        private PersonRepository personRepository;

        public HomeController(DatingAppContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            random = new Random();
            personRepository = new PersonRepository(context);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //   return View(await _context.Persons.ToListAsync());
        //}

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Person> profiles = new List<Person>();
            if (User.Identity.IsAuthenticated)
            {
                profiles = personRepository.GetAllProfilesExceptCurrent(User.Identity.Name);
            }
            else
            {
                profiles = personRepository.GetAllPersons();
            }

            List<Person> randomProfiles = new List<Person>();
            for (int i = 0; i < 3; i++)
            {
                var profile = profiles[random.Next(profiles.Count)];
                if (!randomProfiles.Exists((x) => x == profile))
                {
                    randomProfiles.Add(profile);
                }
                else
                {
                    i--;
                }
            }
            return View(randomProfiles);
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
