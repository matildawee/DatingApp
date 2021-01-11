using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DatingApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;

        public SearchController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
        }

        /*Hämtar matchande profiler för sökning*/
        [AllowAnonymous] /*Endast för att testa vid utloggad*/
        public ActionResult Result(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            List<Person> searchResult = new List<Person>();
            searchResult = personRepository.SearchResultByName(searchString);
            if (searchResult.Count > 0)
            {
                return View(searchResult);
            }
            else
            {
                return View("NoResult");
            }
        }
    }
}
