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

namespace DatingProjekt.Controllers
{
    public class FriendsController : Controller
    {
        private readonly DatingDBContext cntx;
        private readonly FriendRequestManager _friendRequest;
        private readonly FriendManager _friendManager;

        public FriendsController(DatingDBContext context)
        {
            cntx = context;
            _friendRequest = new FriendRequestManager();
            _friendManager = new FriendManager();
        }

        // GET: Friends
        public ActionResult Index()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var friends = cntx.Friends.Where(p => p.User1 == userid || p.User2 == userid).ToList();
            var friendsList = new List<User>();

            foreach (var item in friends)
            {
                if (item.User1.Equals(userid))
                {
                    friendsList.Add(cntx.Users.FirstOrDefault(p => p.UserId == item.User2));
                }
                else
                {
                    friendsList.Add(cntx.Users.FirstOrDefault(p => p.UserId == item.User1));
                }
            }
            return View(friendsList);
        }

       public ActionResult Add(string request)
        {
            string userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Friend f = new Friend
            {
                User1 = request,
                User2 = userid
            };

            var friend = cntx.FriendRequests.FirstOrDefault(f => f.UserSent.Equals(request) && f.UserPending.Equals(userid));

            _friendRequest.Delete(friend);
            _friendManager.Add(f);
            return RedirectToAction("Index", "Friends");
        }
    }
}
