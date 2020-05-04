using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}