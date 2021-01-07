using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
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
        private FriendRepository friendRepository;
        private FriendRequestRepository requestRepository;

        public PersonController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            postRepository = new PostRepository(context);
            friendRepository = new FriendRepository(context);
            requestRepository = new FriendRequestRepository(context);
        }

        public IActionResult Profile(int id)
        {
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
            ViewBag.PersonRelation = GetPersonRelation(id);
            return View(profileViewModel);
        }

        public string GetPersonRelation(int id)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            if (friendRepository.IsFriends(currentUser, id)) {
                return "Friends";
            } else if(requestRepository.FrienRequestOutgoing(currentUser, id)) {
                return "OutgoingRequest";
            } else if(requestRepository.FrienRequestIncoming(currentUser, id)) {
                return "IncomingRequest";
            } else {
                return "NotFriends";
            }
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
            string email = User.Identity.Name;
            int id = personRepository.GetIdByUserIdentityEmail((string)email);
            Person user = personRepository.GetPersonById((int)id);
            List<Post> posts = postRepository.GetAllPostsByPersonId((int)id);
            List<Friend> friends = friendRepository.GetAllFriendsByPersonId((int)id);
            PostUserViewModel postUserViewModel = CreatePostUserViewModel(posts, (int)id);
            FriendUserViewModel friendUserViewModel = CreateFriendUserViewModel(friends, (int)id);

            ProfileViewModel profileViewModel = new ProfileViewModel
            {
                PersonId = user.PersonId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Picture = user.Picture,
                Email = user.Email,
                Posts = postUserViewModel,
                Friends = friendUserViewModel
            };
            return View(profileViewModel);
        }

        public FriendUserViewModel CreateFriendUserViewModel(List<Friend> friends, int personId)
        {
            IEnumerable<FriendViewModel> friendsViewModel = friends.Select((p) => new FriendViewModel()
            {
                FirstPerson = p.FirstPerson,
                SecondPerson = p.SecondPerson
            });

            FriendUserViewModel friendUserViewModel = new FriendUserViewModel
            {
                PersonId = personId,
                Friends = friendsViewModel.ToList()
            };
            return friendUserViewModel;
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public void UpdateProfile(Person person)
        {


            var hej = person.FirstName;
            var ye = person;
            var bla ="";
            //if (ModelState.IsValid)
            //{
            //    if (person.PersonId == 0)
            //    {
            //        NotFound();
            //    }
            //    _context.Update(person);
            //    _context.SaveChanges();
            //}
        }
    }
}
