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

        public RequestController(DatingAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(context);
            requestRepository = new FriendRequestRepository(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult GetFriendRequests()
        {
            string email = User.Identity.Name;
            int reciever = personRepository.GetIdByUserIdentityEmail(email);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(reciever);
            if (requests.Count > 0)
            {
                IEnumerable<RequestViewModel> model = requests.Select((r) => new RequestViewModel()
                {
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
        public ActionResult AcceptRequest(int senderId)
        {
            int receiverId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);

            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(receiverId);
            
            foreach(FriendRequest fr in requests)
            {
                if(fr.SenderId == senderId && fr.ReceiverId == receiverId)
                {
                    //requestRepository.AcceptRequest(fr);

                    fr.Accepted = true;
                    _context.Update(fr);
                    _context.SaveChanges();

                    return Json(new { Result = true });
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        public ActionResult DeclineRequest(int senderId)
        {
            int receiverId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(receiverId);

            foreach(FriendRequest fr in requests)
            {
                if (fr.SenderId.Equals(senderId) && fr.ReceiverId.Equals(receiverId))
                {
                    requestRepository.DeleteRequest(fr);
                    return Json(new { Result = true });
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        public ActionResult SendRequest(int receiverId)
        {
            int senderId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
                requestRepository.AddRequest(new FriendRequest
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Accepted = false
                });
                return Json(new { result = true });
        }
        
        [HttpPost]
        public ActionResult CancelRequest(int receiverId)
        {
            int senderId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            if (requestRepository.FriendRequestOutgoing(senderId, receiverId))
            {
                List<FriendRequest> requests = requestRepository.GetAllRequestsSentByUser(senderId);
                foreach (FriendRequest r in requests)
                {
                    if (r.ReceiverId == receiverId && r.SenderId == senderId)
                    {
                        requestRepository.DeleteRequest(r);
                        return Json(new { result = true });
                    }
                }
            }
            return Json(new { result = false });
        }
    }
}
