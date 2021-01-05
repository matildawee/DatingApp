using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace DatingApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PostApiController : ApiController
    {
        private readonly DatingAppContext _context;

        public PostApiController(DatingAppContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void AddPost(Post post)
        {
            
            var ja = post.PersonId;
            var hej = "h";
            //if (ModelState.IsValid)
            //{
            //    string postTo;
            //    if (string.IsNullOrWhiteSpace(post.PostToId))
            //    {
            //        postTo = User.Identity.GetUserId();
            //    }
            //    else
            //    {
            //        postTo = post.PostToId;
            //    }

            //    PostModels postModel = new PostModels()
            //    {
            //        Text = post.Text,
            //        PostFromId = User.Identity.GetUserId(),
            //        PostToId = postTo,
            //        PostDateTime = DateTime.Now
            //    };

            //    postRepository.Add(postModel);
            //    postRepository.Save();
            //}
        }

        //[HttpDelete]
        //public void DeletePost(int id)
        //{
        //    postRepository.Remove(id);
        //    postRepository.Save();
        //}
    } 
}
