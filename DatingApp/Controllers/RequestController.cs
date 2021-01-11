using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //Hämtar alla vänförfrågningar och returnerar en av två partialviews beroende på om det finns några vänförfrågningar. 
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
                    Sender = personRepository.GetPersonById(r.SenderId),
                    FullName = r.Sender.FirstName + " " + r.Sender.LastName
                });
                return PartialView("_Requests", model);
            }
            else
            {
                return PartialView("_NoRequests");
            }
        }

        //Hämtar antalet vänförfrågningar och returnerar resultatet som JSON
        [HttpPost]
        public ActionResult GetNumberOfRequests()
        {
            int id = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(id);
            return Json(new {data = requests.Count });
        }

        public ActionResult AcceptRequest(int senderId)
        {
            int receiverId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);

            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(receiverId);
            
            foreach(FriendRequest fr in requests) //Loopar genom alla vänförfrågningar användaren har fått
            {
                if(fr.SenderId == senderId && fr.ReceiverId == receiverId) //Kontrollerar att sändare och mottagare stämmer
                {
                    fr.Accepted = true;
                    try
                    {
                        _context.Update(fr);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        TempData["ProcessMessage"] = "Could not update profile picture, please try again";
                        return PartialView("Exception");
                    }
                }
            }
            return RedirectToAction("Profile", "Person", new { id = senderId });
        }   
        
        public ActionResult DeclineRequest(int senderId)
        {
            int receiverId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            List<FriendRequest> requests = requestRepository.GetAllRequestsSentToUser(receiverId);

            foreach(FriendRequest fr in requests)
            {
                if (fr.SenderId.Equals(senderId) && fr.ReceiverId.Equals(receiverId))
                {
                    try
                    {
                        _context.Remove(fr);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        TempData["ProcessMessage"] = "Could not decline friend, try again later";
                        return PartialView("Exception");
                    }
                }
            }
            return RedirectToAction("Profile", "Person", new { id = senderId });
        }

        public ActionResult SendRequest(int receiverId)
        {
            int senderId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            FriendRequest friendRequest = new FriendRequest //Skapar ny vänförfrågan
            {
                Sender = personRepository.GetPersonById(senderId),
                SenderId = senderId,
                Receiver = personRepository.GetPersonById(receiverId),
                ReceiverId = receiverId,
                Accepted = false
            };
            try
            {
                _context.Add(friendRequest);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                TempData["ProcessMessage"] = "Could not send request, try again later";
                return PartialView("Exception");
            }
            return RedirectToAction("Profile", "Person", new { id = receiverId });
        }

        //Avbryter och tar bort en förfrågan användaren har skickat
        public ActionResult CancelRequest(int receiverId)
        {
            int senderId = personRepository.GetIdByUserIdentityEmail(User.Identity.Name);
            if (requestRepository.FriendRequestOutgoing(senderId, receiverId))
            {
                //Hämtar alla förfrågningar användaren har skickat
                List<FriendRequest> requests = requestRepository.GetAllRequestsSentByUser(senderId); 
                foreach (FriendRequest r in requests)
                {
                    if (r.ReceiverId == receiverId && r.SenderId == senderId)
                    {
                        try
                        {
                            _context.Remove(r);
                            _context.SaveChanges();
                        }
                        catch (Exception)
                        {
                            TempData["ProcessMessage"] = "Could not cancel request, try again later";
                            return PartialView("Exception");
                        }
                    }
                }
            }
            return RedirectToAction("Profile", "Person", new { id = receiverId });
        }
    }
}
