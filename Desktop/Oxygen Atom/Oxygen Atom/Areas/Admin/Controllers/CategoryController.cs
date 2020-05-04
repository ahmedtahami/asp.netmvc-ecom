using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();

        // GET: Admin/Category

        public ActionResult Index()
        {

            List<Category> modelList = Handler.GetCategories().ToList();
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                Handler.AddCategory(category);
                return RedirectToAction("Index", "Category");

            }
            return View(category);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = Handler.GetCategory(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Handler.UpdateCategory(category);
                return RedirectToAction("Index", "Category");
            }

            return View(category);
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = Handler.GetCategory(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                Handler.DeleteCategory(id);
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

    }
}