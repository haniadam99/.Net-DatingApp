using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatingProjekt.Models;
using DataLayer.Managers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using DatingProjekt.Data;
using Microsoft.AspNetCore.Identity;

namespace DatingProjekt.Controllers
{
    public class FriendRequestsController : Controller
    {
        private readonly FriendRequestManager _friendRequest;
        private readonly DatingDBContext cntx;
        // GET: FriendRequestController
        public FriendRequestsController()
        {
            cntx = new DatingDBContext();
            _friendRequest = new FriendRequestManager();
        }

        public ActionResult SendRequest()
        {

            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string url = HttpContext.Request.GetDisplayUrl().ToString();

            string referer = Request.Headers["Referer"].ToString();

            var lastPartOfUrl = referer.Substring(referer.LastIndexOf("/") + 1);

            User getRequesterProfile = cntx.Users.FirstOrDefault(x => x.UserId == userid);
            User getAskedFriendProfile = cntx.Users.FirstOrDefault(x => x.UserId == lastPartOfUrl);
            // Kollar om det redan finns en förfrågan.
            var requestSent = cntx.FriendRequests.FirstOrDefault(f => f.UserPending == lastPartOfUrl && f.UserSent == userid);

            if (requestSent == null)
            {
                var friendRequest = new FriendRequest
                {
                    UserPendingNavigation = getAskedFriendProfile,
                    UserSentNavigation = getRequesterProfile
                };

                _friendRequest.Add(friendRequest);
            }

            return RedirectToAction("Index", "Friends");
        }

        // GET: FriendRequests
        public async Task<IActionResult> Index()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var Requests = cntx.FriendRequests.Where(r => r.UserPending.Equals(userid)).ToList();
            var users = new List<User>();

            foreach (var r in Requests)
            {
                if (r.UserPending.Equals(userid))
                {
                    var user = cntx.Users.FirstOrDefault(u => u.UserId == r.UserSent);
                    users.Add(user);
                }
            }
            return View(users);
        }

        [HttpGet]
        public string CountRequests()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Requests = cntx.FriendRequests.Where(p => p.UserPending == userid).ToList();
            var profiles = new List<User>();

            foreach (var r in Requests)
            {
                if (r.UserPending.Equals(userid))
                {
                    var profile = cntx.Users.FirstOrDefault(p => p.UserId == r.UserPending);
                    profiles.Add(profile);
                }
            }
            return profiles.Count().ToString();
        }

        public ActionResult Delete(string request)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var friendRequest = cntx.FriendRequests.FirstOrDefault(f => f.UserSent.Equals(request) && f.UserPending.Equals(userid));
            _friendRequest.Delete(friendRequest);

            return RedirectToAction("Index", "FriendRequests");

        }
    }
}
