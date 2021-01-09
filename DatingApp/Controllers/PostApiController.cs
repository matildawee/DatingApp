using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostApiController : ControllerBase
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;
        public PostApiController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
        }

        [HttpPost]
        [Route("AddPost")]
        public void AddPost(Post post)
        {
            if (post.PersonId == 0)
            {
                NotFound();
            }

            Post newPost = new Post()
            {
                PostText = post.PostText,
                PersonId = post.PersonId,
                AuthorId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name),
                Timestamp = DateTime.Now
            };
            try
            {
                _context.Posts.Add(newPost);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
