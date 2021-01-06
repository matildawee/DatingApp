using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class RequestController : Controller
    {
        private readonly DatingAppContext _context;
        private PersonRepository personRepository;
        private FriendRequestRepository requestRepository;
        private FriendRepository friendRepository;

        public RequestController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            requestRepository = new FriendRequestRepository(context);
            friendRepository = new FriendRepository(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult GetFriendRequests()
        {
            string email = User.Identity.Name;
            int id = personRepository.GetIdByUserIdentityEmail(email);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(id);
            if (requests.Count >= 1)
            {
                IEnumerable<RequestViewModel> model = requests.Select((r) => new RequestViewModel()
                {
                    FriendRequestId = r.FriendRequestId,
                    SenderId = r.SenderId,
                    FullName = r.Sender.FirstName + " " + r.Sender.LastName
                });
                return PartialView("_Requests", model);
            }
            else
            {
                return PartialView("_NoRequests");
            }
        }

        [HttpPost]
        public ActionResult GetNumberOfRequests()
        {
            int id = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(id);
            return Json(new {data = requests.Count });
        }

        [HttpPost]
        public ActionResult AcceptRequest(int id)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);

            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(currentUser);
            
            foreach(FriendRequest fr in requests)
            {
                if(fr.SenderId.Equals(id) && fr.ReceiverId.Equals(currentUser))
                {
                    requestRepository.DeleteRequest(fr.FriendRequestId);
                    friendRepository.AddFriend(new Friend
                    {
                        FirstPersonId = currentUser,
                        SecondPersonId = id,
                        // OBS! Lägga till kategori!
                    });
                    return Json(new { Result = true });
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        public ActionResult DeclineRequest(int id)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(currentUser);

            foreach(FriendRequest fr in requests)
            {
                if (fr.SenderId.Equals(id) && fr.ReceiverId.Equals(currentUser))
                {
                    requestRepository.DeleteRequest(fr.FriendRequestId);
                    return Json(new { Result = true });
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        public ActionResult SendRequest(int id)
        {
            int currentUser = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            if (requestRepository.FrienRequestIncoming(currentUser, id) || requestRepository.FrienRequestOutgoing(currentUser, id))
            {
                return Json(new { result = false });
            }
            else
            {
                requestRepository.AddRequest(new FriendRequest
                {
                    SenderId = currentUser,
                    ReceiverId = id,
                });
                return Json(new { result = true });
            }
        }

        [HttpPost]
        public ActionResult GetFriendStatus()
        {
            //var d = "jaja";
            //var e = "tjabba";
            //if (d != e)
            //{
            //    return Json(new { text = "Tjoho" });
            //}
            //else
            //{
            //    return Json(new { text = "Nehe" });
            //}
            return Json(new { text = "Nehe" });
        }
    }
}
