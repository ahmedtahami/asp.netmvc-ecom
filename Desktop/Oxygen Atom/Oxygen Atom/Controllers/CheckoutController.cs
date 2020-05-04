using System;
using Oxygen_Atom.Models;
using System.Web.Mvc;
using Oxygen_Atom.Entities;
using System.Linq;

namespace Oxygen_Atom.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        const string PromoCode = "FREE";

        // GET: Checkout
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                //Save Order
                context.Orders.Add(order);
                context.SaveChanges();

                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order.Total = cart.GetTotal();
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                    new { id = order.OrderId });
            }
            catch
            {
                return View(order);
            }
        }
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = context.Orders.Any(
                o => o.OrderId == id &&
                     o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}