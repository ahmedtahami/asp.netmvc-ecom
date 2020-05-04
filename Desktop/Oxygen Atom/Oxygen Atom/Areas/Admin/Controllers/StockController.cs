using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Areas.Admin.Models;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Linq;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Admin/Stock
        public ActionResult Index()
        {
            var model = Handler.GetStockedProducts();
            
            return View(model);
        }
    }
}