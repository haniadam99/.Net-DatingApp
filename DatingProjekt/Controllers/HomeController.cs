using DatingProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatingProjekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatingDBContext cntx;

        public HomeController(ILogger<HomeController> logger, DatingDBContext context)
        {
            _logger = logger;
            cntx = context;
        }

        //public ActionResult SearchUser(string name)
        //{
        //    var findUser = cntx.Users.Where(x => x.Name.Equals(name) || name == null);
        //    var user = findUser.ToList();
        //    return View(user);
        //}
        public IActionResult Index()
        {
            var getUser = cntx.Set<User>().ToList();
            return View(getUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult SearchUser(string? name)
        {
            var result = cntx.Users.Where(x => x.Name.Contains(name) && x.VisibleSearch.Equals("Ja")).ToList();
            return View(result);
        }
    }
}
