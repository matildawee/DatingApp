using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly DatingAppContext _context;

        public PersonController(DatingAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            
            var user = await _context.Persons
                .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
