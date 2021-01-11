using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            if (id > 0)
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
            //Om vi kommer hit är något fel och vi återgår till startsidan
            return RedirectToAction("Index", "Home");
        }

        //Kollar vilken relation två användare har till varandra för att uppdatera relationsknappen i Profile-vyn
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

        //Skapar vymodell - PostUserViewModel, som innehåller de inlägg som gjorts på en användares vägg.
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

        //Hämtar den inloggade användarens profil, med tillhörande inlägg och vänlista.
        public IActionResult MyProfile()
        {
            string email = User.Identity.Name;
            int id = personRepository.GetIdByUserIdentityEmail((string)email);
            Person user = personRepository.GetPersonById((int)id);
            List<Post> posts = postRepository.GetAllPostsByPersonId((int)id);
            List<FriendRequest> friends = requestRepository.GetFriendsByPersonId((int)id);
            PostUserViewModel postUserViewModel = CreatePostUserViewModel(posts, (int)id);
            FriendUserViewModel friendUserViewModel = CreateFriendUserViewModel(friends, (int)id);
            if (ModelState.IsValid)
            {
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
            //Om vi kommer hit är något fel och vi återgår till startsidan
            return RedirectToAction("Index", "Home");
        }

        //Skapar vymodell - FriendUserViewModel, som innehåller den inloggade användarens vänner.
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
                /*Hämtar den FriendRequest där SenderID och ReceiverID stämmer, och tar bort den, kontroll görs åt båda hållen
                 * - användaren kan nämligen antingen vara den som skickat eller fått vänförfrågan. */
                if (f.ReceiverId.Equals(currentUser) && f.SenderId.Equals(friendToRemove) || f.ReceiverId.Equals(friendToRemove) && f.SenderId.Equals(currentUser))
                {
                    try
                    {
                        _context.Remove(f);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        TempData["ProcessMessage"] = "Could not remove friend, try again later";
                        return PartialView("Exception");
                    }
                    
                }
            }
            return RedirectToAction("Profile", "Person", new { id = friendToRemove });
        }

        //Hämtar prfilbild som FileContentResult för att visas i den vyn där metoden anropats.
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
        [ValidateAntiForgeryToken]
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
                catch (Exception)
                {
                    TempData["ProcessMessage"] = "Could not update profile";
                    return PartialView("Exception");
                }
            }

            return RedirectToAction("MyProfile");
        }

        //Uppdaterar profilbilden till användarens valda bild
        [ValidateAntiForgeryToken]
        public IActionResult UploadImage()
        {
            foreach (var file in Request.Form.Files) 
                //loopar igenom filerna som lagts in, ifall man lagt in flera bilder så kommer den senast inlagda vara den som sparas
            {
                int currentUser = personRepository.GetIdByUserIdentityEmail((string)User.Identity.Name);
                Person personToUpdate = personRepository.GetPersonById(currentUser);

                System.IO.MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                personToUpdate.Picture = ms.ToArray();
                    //skapar en memorystream som hämtar in bildfilen och lägger den som användarens nya bild

                ms.Close();
                ms.Dispose();
                try
                {
                    _context.Update(personToUpdate);
                    _context.SaveChanges();
                }
                catch(Exception)
                {
                    TempData["ProcessMessage"] = "Could not update profile picture, please try again";
                    return PartialView("Exception");
                }
                
            }
            return RedirectToAction("MyProfile");
        }

        public PartialViewResult UpdatePostWall(int id)
        {
            List<Post> posts = postRepository.GetAllPostsByPersonId(id);
            PostUserViewModel postUserViewModel = CreatePostUserViewModel(posts, id);
            return PartialView("/Views/Post/_Post.cshtml", postUserViewModel);
        }
    }
}
