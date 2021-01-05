using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
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
        private PersonRepository personRepository;
        private PostRepository postRepository;

        public PersonController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            postRepository = new PostRepository(context);
        }
        public IActionResult Profile(int id)
        {
            //var user = await _context.Persons
            //    .FirstOrDefaultAsync(m => m.PersonId == id);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //return View(user);

            Person user = personRepository.GetPersonById((int)id);
            List<Post> posts = postRepository.GetAllPostsByPersonId((int)id);
            PostUserViewModel postUserViewModel = CreatePostUserViewModel(posts, (int)id);
            ProfileViewModel profileViewModel = new ProfileViewModel
            {
                PersonId = user.PersonId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Picture = user.Picture,
                Posts = postUserViewModel
            };
            return View(profileViewModel);
        }
        public PostUserViewModel CreatePostUserViewModel(List<Post> posts, int personId)
        {
            IEnumerable<PostViewModel> postsViewModel = posts.Select((p) => new PostViewModel()
            {
                PostId = p.PostId,
                Author = personRepository.GetPersonById(p.AuthorId),
                PostText = p.PostText,
                Timestamp = p.Timestamp
            });

            PostUserViewModel postUserViewModel = new PostUserViewModel
            {
                PersonId = personId,
                Posts = postsViewModel.ToList()
            };
            return postUserViewModel;
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

        public async Task<IActionResult> Edit([Bind("PersonId,Email,FirstName,LastName,Description, Picture")] Person person)
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
            return View("MyProfile");
        }
    }
}
