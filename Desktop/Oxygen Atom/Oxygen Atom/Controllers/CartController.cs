using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Linq;
using System.Web.Mvc;

namespace Oxygen_Atom.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Cart
        public ActionResult Index()
        {
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);

            // Set up our ViewModel
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(CartModel model)
        {
            context.Configuration.ProxyCreationEnabled = false;
            if (model.Count < 1)
            {
                model.Count++;
            }
            // Retrieve the product from the database
            Product selectedProduct = context.Products
                .Single(p => p.Id == model.Id);

            // Retrieve the productsize from the database
            ProductSizes selectedSize = context.ProductSizes
                .Single(p => p.Id == model.size_id && p.ProductId == model.Id);

            // Add it to the shopping cart
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);

            cart.AddToCart(selectedProduct, selectedSize, model.Count);

            // Go back to the main store page for more shopping
            return Json(new { Result = "Success", Message = "Save successfully" }, JsonRequestBehavior.AllowGet);
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);

            // Get the name of the album to display confirmation
            string product = context.Carts
                .Single(item => item.RecordId == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            CartItemRemoveModel results = new CartItemRemoveModel
            {
                Message = Server.HtmlEncode(product) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /Cart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}