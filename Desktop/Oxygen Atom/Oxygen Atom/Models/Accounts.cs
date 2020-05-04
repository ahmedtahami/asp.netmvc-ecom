using System.ServiceModel.Activation.Configuration;
using Microsoft.AspNet.Identity.Owin;
using Oxygen_Atom.App_Start;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Oxygen_Atom.Models
{
    public class Accounts
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public Accounts()
        {

        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
            private set => _userManager = value;
        }
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }
       
        public SignInStatus Login(LoginViewModel model)
        {
            var result = SignInManager.PasswordSignIn(model.UserName, model.Password,model.RememberMe,shouldLockout:false);
            return result;

        }

        public IdentityResult Register(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber

            };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                UserManager.AddToRole(user.Id, "Customer");
                return result;
            }
            else
            {
                return result;
            }
        }

    }
}