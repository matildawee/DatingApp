using DataLayer;
using DataLayer.Models;
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
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> MyProfile()
        { 
            var user = await _context.Persons
                .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public IActionResult Post()
        {
            return View();
        }

        public async Task<IActionResult> Edit([Bind("PersonId,Email,FirstName,LastName,Description")] Person person)
        {
            if (person.PersonId == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            TempData["Success"] = "Profile was updated successfully.";
            return View("Profile");
        }
    }
}
