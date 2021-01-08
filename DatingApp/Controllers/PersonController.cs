using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;
        private PostRepository postRepository;
        private FriendRequestRepository requestRepository;

        public PersonController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            postRepository = new PostRepository(context);
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
            int loggedInUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            if (requestRepository.IsFriends(loggedInUser, id))
            {
                return "Friends";
            }
            else if (requestRepository.FriendRequestOutgoing(loggedInUser, id))
            {
                return "OutgoingRequest";
            }
            else if (requestRepository.FriendRequestIncoming(loggedInUser, id))
            {
                return "IncomingRequest";
            }
            else
            {
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

        public IActionResult MyProfile()
        {
            string email = User.Identity.Name;
            int id = personRepository.GetIdByUserIdentityEmail((string)email);
            Person user = personRepository.GetPersonById((int)id);
            List<Post> posts = postRepository.GetAllPostsByPersonId((int)id);
            List<FriendRequest> friends = requestRepository.GetFriendsByPersonId((int)id);
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
                AccountHidden = user.AccountHidden,
                Posts = postUserViewModel,
                Friends = friendUserViewModel
            };
            return View(profileViewModel);
        }

        public FriendUserViewModel CreateFriendUserViewModel(List<FriendRequest> friends, int personId)
        {
            IEnumerable<FriendViewModel> friendsViewModel = friends.Select((p) => new FriendViewModel()
            {
                FirstPerson = personRepository.GetPersonById(p.SenderId),
                SecondPerson = personRepository.GetPersonById(p.ReceiverId)
            });

            FriendUserViewModel friendUserViewModel = new FriendUserViewModel
            {
                PersonId = personId,
                Friends = friendsViewModel.ToList()
            };
            return friendUserViewModel;
        }

        public ActionResult RemoveFriend(int friendToRemove)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> friends = requestRepository.GetFriendsByPersonId(currentUser);
            foreach (FriendRequest f in friends)
            {
                if (f.ReceiverId.Equals(currentUser) && f.SenderId.Equals(friendToRemove) || f.ReceiverId.Equals(friendToRemove) && f.SenderId.Equals(currentUser))
                {
                    requestRepository.DeleteFriendOrRequest(f);
                    //return Json(new { Result = true });
                }
            }
            return RedirectToAction("Profile", "Person", new { id = friendToRemove });
        }

        [AllowAnonymous]
        public FileContentResult LoadPicture(int id)
        {
            byte[] image = personRepository.GetPictureById(id);
            if (image != null)
            {
                return new FileContentResult(image, "image/jpeg");
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfile(ProfileViewModel profileModel)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail((string)User.Identity.Name);
            Person personToUpdate = personRepository.GetPersonById(currentUser);
            personToUpdate.FirstName = profileModel.FirstName;
            personToUpdate.LastName = profileModel.LastName;
            personToUpdate.Description = profileModel.Description;
            personToUpdate.AccountHidden = profileModel.AccountHidden;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(personToUpdate.PersonId))
                    {
                        //error
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("MyProfile");
        }

        private bool UserExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }

        public IActionResult UploadImage()
        {
            foreach (var file in Request.Form.Files)
            {
                int currentUser = personRepository.GetIdByUserIdentityEmail((string)User.Identity.Name);
                Person personToUpdate = personRepository.GetPersonById(currentUser);

                System.IO.MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                personToUpdate.Picture = ms.ToArray();

                ms.Close();
                ms.Dispose();

                _context.Update(personToUpdate);
                _context.SaveChanges();
            }
            return RedirectToAction("MyProfile");
        }
    }
}
