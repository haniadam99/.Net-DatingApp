using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Hosting.Internal;
using DatingProjekt.Models;
using Microsoft.Azure.Management.ContainerInstance.Fluent.Models;
using Topshelf.Runtime;
using DataLayer.Managers;
using DatingProjekt.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Http.Extensions;

namespace DatingProjekt.Controllers
{
    public class UserController : Controller
    {
        private readonly DatingDBContext cntx;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager _userManager;
        private readonly FriendRequestManager _friendRequestManager;
        private readonly FriendManager _friendManager;

        public UserController(DatingDBContext context, IHostingEnvironment hostingEnvironment)
        {
            cntx = context;
            this.hostingEnvironment = hostingEnvironment;
            _userManager = new UserManager();
            _friendRequestManager = new FriendRequestManager();
            _friendManager = new FriendManager();
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await cntx.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = cntx.Users.FirstOrDefault(m => m.UserId == userid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {

            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string uniqueFileName = null;

                if (user.FileName != null)
                {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + user.FileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                user.FileName.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                User u = new User
                {
                    UserId = userid,
                    Name = user.Name,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Orientation = user.Orientation,
                    VisibleSearch = user.VisibleSearch,
                    ImagePath = uniqueFileName
                };

                 _userManager.Add(u);
                await cntx.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }

        // GET: User/Edit/5
        public ActionResult Edit()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = cntx.Users.FirstOrDefault(m => m.UserId == userid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User model, string i)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = cntx.Users.FirstOrDefault(m => m.UserId == userid);

            string uniqueFileName = null;

            if (model.FileName != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.FileName.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            user.Name = model.Name;
            user.Birthday = model.Birthday;
            user.Gender = model.Gender;
            user.Orientation = model.Orientation;
            user.VisibleSearch = model.VisibleSearch;
            user.ImagePath = uniqueFileName;

            cntx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Profiles(string? id)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = cntx.Users.FirstOrDefault(p => p.UserId == id);

            ViewBag.UserId = userid;
            ViewBag.IsFriend = _friendManager.AreFriends(userid, id);
            ViewBag.PendingFriend = _friendRequestManager.Exists(userid, id);

            if (id == userid)
            {
                return RedirectToAction("Details", new User
                { UserId = userid });
            }
            else
            {
                return View(user);
            }
        }
    }
}