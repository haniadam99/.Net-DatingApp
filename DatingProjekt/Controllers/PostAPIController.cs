using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatingProjekt.Models;
using System.Security.Claims;

namespace DatingProjekt.Controllers
{
    public class PostAPIController : Controller
    {
        private readonly DatingDBContext cntx;

        public PostAPIController(DatingDBContext context)
        {
            cntx = context;
        }

        // GET: PostsAPI
        public async Task<IActionResult> Index(string id)
        {
            return View(await cntx.Posts.Where(p => p.Profile == id).ToListAsync());
        }



        [Route("AddMessage")]
        [HttpPost]
        public void AddMessageToList(Post post)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            post.Author = userid;
            var profile = cntx.Users.FirstOrDefault(p => p.UserId == post.Profile);
            post.ProfileNavigation = profile;
            post.PublishDate = DateTime.Now;

            cntx.Posts.Add(post);
            cntx.SaveChanges();
        }
    }
}
