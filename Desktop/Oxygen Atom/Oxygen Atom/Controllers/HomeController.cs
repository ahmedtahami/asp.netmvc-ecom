using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using HomeViewModel = Oxygen_Atom.Models.HomeViewModel;

namespace Oxygen_Atom.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ShopHandler Handler = new ShopHandler();
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Home
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                FeaturedProducts = Handler.GetFeaturedProducts(),
                NewProducts = Handler.GetNewProducts(8),
                Categories = Handler.GetCategories(),
                SubCategories = Handler.GetSubCategories()
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            HomeViewModel model = new HomeViewModel();
            Product product = Handler.GetProductById(id);
            List<ProductSizes> availableSizes = Handler.GetProductSizeByProductId(id);

            model.Product = product;
            model.ProductSizes = availableSizes;

            return View(model);
        }
        public ActionResult ShopBySubCategory(int id)
        {
            HomeViewModel model = new HomeViewModel();
            List<Product> products = Handler.GetProductsBySubCategory(id);

            model.Products = products;

            return View(model);
        }
        public ActionResult ShopByCategory(int id, int? pageNo)
        {
            HomeViewModel model = new HomeViewModel();
            List<Product> products = Handler.GetProductsByCategory(id);

            model.Products = products;

            return View(model);
        }
        public ActionResult CategoriesPartialView()
        {
            HomeViewModel model = new HomeViewModel
            {
                Categories = Handler.GetCategories()
            };
            return PartialView(model);
        }

        public ActionResult SubCategoriesPartialView()
        {
            HomeViewModel model = new HomeViewModel
            {
                SubCategories = Handler.GetSubCategories()
            };
            return PartialView(model);
        }

        public ActionResult LoggedInUserPartialView()
        {
            return PartialView();
        }
    }
}