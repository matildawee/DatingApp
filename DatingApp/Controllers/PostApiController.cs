using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostApiController : ControllerBase
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;
        private PostRepository postRepository;
        public PostApiController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            postRepository = new PostRepository(context);
        }

        [HttpPost]
        [Route("AddPost")]
        public void AddPost(Post post)
        {
            if (ModelState.IsValid)
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

                _context.Posts.Add(newPost);
                _context.SaveChanges();
            }
        }
    }
}
