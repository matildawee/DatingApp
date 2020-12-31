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

        [TempData]
        public string StatusMessage { get; set; }

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
        public IActionResult Post()
        {
            return View();
        }

        public async Task<IActionResult> UpdateProfile()
        {
            //StatusMessage = "det gick bra";
            var user = await _context.Persons
               .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);

            var id = user.PersonId;
            
            if (id == null)
            {
                return NotFound();
            }
            var personToUpdate = await _context.Persons.FirstOrDefaultAsync(s => s.PersonId == id);
            //personToUpdate.Description = "hårdkodad beskrivning";


            if (await TryUpdateModelAsync<Person>(personToUpdate, "",
                    s => s.FirstName, s => s.LastName, s => s.Description))
            {
                try
                {
                    await _context.SaveChangesAsync();   
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View("Profile", personToUpdate);
        }

        

        public async Task<IActionResult> Edit([Bind("PersonId,Email,FirstName,LastName,Description")] Person person)
        {
            //var user = await _context.Persons
              // .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
            //var id = user.PersonId;

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
            return View("Profile");
        }
    }
}
