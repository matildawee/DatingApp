using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Person> profiles = new List<Person>();
            if (User.Identity.IsAuthenticated)
            {
                profiles = personRepository.GetAllVisibleProfilesExceptCurrent(User.Identity.Name);
            }
            else
            {
                profiles = personRepository.GetAllVisiblePersons();
            }

            List<Person> randomProfiles = new List<Person>();
            if(profiles.Count > 0) //Kollar så att profiler finns i databasen
            {
                if (profiles.Count == 1) //om det bara finns en profil så visas bara den
                {
                    var profile = profiles[0];
                    randomProfiles.Add(profile);
                }
                else if (profiles.Count == 2) //Om det finns två profiler visas de två
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var profile = profiles[i];
                        randomProfiles.Add(profile);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++) //annars slumpas tre personer fram
                    {
                        var profile = profiles[random.Next(profiles.Count)];
                        if (!randomProfiles.Exists((x) => x == profile))
                        {
                            randomProfiles.Add(profile);
                        }
                        else //ifall en person slumpas in två gångar backas index tills en ny person kommer in i listan
                        {
                            i--;
                        }
                    }
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
