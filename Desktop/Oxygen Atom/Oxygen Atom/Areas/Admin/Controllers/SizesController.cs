using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SizesController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Admin/Category
        public ActionResult Index()
        {

            List<Sizes> modelList = new ShopHandler().GetSizes().ToList();
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sizes size)
        {
            if (ModelState.IsValid)
            {
                Handler.AddSize(size);
                return RedirectToAction("Index", "Sizes");

            }
            return View(size);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sizes size = Handler.GetSize(Id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        [HttpPost]
        public ActionResult Edit(Sizes size)
        {
            if (ModelState.IsValid)
            {
                Handler.UpdateSize(size);
                return RedirectToAction("Index", "Sizes");
            }

            return View(size);
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sizes size = Handler.GetSize(Id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                Handler.DeleteSize(id);
                return RedirectToAction("Index", "Sizes");
            }
            return View();
        }
    }
}