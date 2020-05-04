using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Oxygen_Atom.Models;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Oxygen_Atom.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private Accounts accounts = new Accounts();
        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);

            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SignInStatus result = accounts.Login(model);
                if (result == SignInStatus.Success && User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (result == SignInStatus.Success && User.IsInRole("Customer"))
                {
                    MigrateShoppingCart(model.UserName);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                                                  && returnUrl.StartsWith("/")
                                                  && !returnUrl.StartsWith("//") &&
                                                  !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = accounts.Register(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                AddError(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        private void AddError(IdentityResult result)
        {
            foreach (string re in result.Errors)
            {
                ModelState.AddModelError("", re);
            }
        }
    }
}