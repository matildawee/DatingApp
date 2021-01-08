using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProfileApiController : ControllerBase
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;

        public ProfileApiController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
        }



        [HttpPost]
        [Route("updateprofile")]
        public async Task<ActionResult> UpdateProfile(Person person)
        {
            int oldPersonId = personRepository.GetIdByUserIdentityEmail((string)User.Identity.Name);
            Person oldPerson = personRepository.GetPersonById(oldPersonId);

            oldPerson.FirstName = person.FirstName;
            oldPerson.LastName = person.FirstName;
            oldPerson.Description = person.Description;
            oldPerson.AccountHidden = person.AccountHidden;

            
            _context.Update(oldPerson);
            await _context.SaveChangesAsync();   //async??????

            return null;
        }
    }
}
