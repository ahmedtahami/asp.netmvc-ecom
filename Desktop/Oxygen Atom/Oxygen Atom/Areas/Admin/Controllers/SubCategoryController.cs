using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Admin/SubCategory
        public ActionResult Index()
        {
            List<SubCategory> modelList = Handler.GetSubCategories().ToList();
            return View(modelList);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SubCategory SubCategory)
        {
            if (ModelState.IsValid)
            {
                Handler.AddSubCategory(SubCategory);
                return RedirectToAction("Index", "SubCategory");

            }
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", SubCategory.CategoryId);
            return View(SubCategory);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategory SubCategory = Handler.GetSubCategory(Id);
            if (SubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", SubCategory.CategoryId);
            return View(SubCategory);
        }

        [HttpPost]
        public ActionResult Edit(SubCategory SubCategory)
        {
            if (ModelState.IsValid)
            {
                Handler.UpdateSubCategory(SubCategory);
                return RedirectToAction("Index", "SubCategory");
            }
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", SubCategory.CategoryId);
            return View(SubCategory);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategory SubCategory = Handler.GetSubCategory(Id);
            if (SubCategory == null)
            {
                return HttpNotFound();
            }
            return View(SubCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                Handler.DeleteSubCategory(id);
                return RedirectToAction("Index", "SubCategory");
            }
            return View();
        }

    }
}