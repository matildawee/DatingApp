using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{  
    public class PostController : Controller
    {
        private readonly DatingAppContext _context;
        private PostRepository postRepository;
      
        public PostController(DatingAppContext context)
        {
            _context = context;
            postRepository = new PostRepository(context);
        }
        public IActionResult Post(int personid)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetAllPosts(personid);
            return View(posts);
        }
    }
}
