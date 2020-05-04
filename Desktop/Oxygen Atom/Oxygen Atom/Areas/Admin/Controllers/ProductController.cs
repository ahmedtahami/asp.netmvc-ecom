using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Admin/Product
        public ActionResult Index()
        {
            List<Product> modeList = Handler.GetProducts().ToList();

            return View(modeList);
        }
        public ActionResult Create(Product product)
        {
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(Context.SubCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase ProductImage1, HttpPostedFileBase ProductImage2, HttpPostedFileBase ProductImage3)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(ProductImage1.FileName);
                string path = Path.Combine(Server.MapPath("~/Photos/"), filename);
                ProductImage1.SaveAs(path);
                product.ProductImage1 = "~/Photos/" + filename;

                string filename2 = Path.GetFileName(ProductImage2.FileName);
                string path2 = Path.Combine(Server.MapPath("~/Photos/"), filename2);
                ProductImage2.SaveAs(path2);
                product.ProductImage2 = "~/Photos/" + filename2;

                string filename3 = Path.GetFileName(ProductImage3.FileName);
                string path3 = Path.Combine(Server.MapPath("~/Photos/"), filename3);
                ProductImage3.SaveAs(path3);
                product.ProductImage3 = "~/Photos/" + filename3;

                Handler.AddProduct(product);
           

                return RedirectToAction("Index", "Product");

            }
            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(Context.SubCategories, "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = Handler.GetProductById(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(Context.SubCategories, "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase ProductImage1, HttpPostedFileBase ProductImage2, HttpPostedFileBase ProductImage3)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(ProductImage1.FileName);
                string path = Path.Combine(Server.MapPath("~/Photos/"), filename);
                ProductImage1.SaveAs(path);
                product.ProductImage1 = "~/Photos/" + filename;

                string filename2 = Path.GetFileName(ProductImage2.FileName);
                string path2 = Path.Combine(Server.MapPath("~/Photos/"), filename2);
                ProductImage2.SaveAs(path2);
                product.ProductImage2 = "~/Photos/" + filename2;

                string filename3 = Path.GetFileName(ProductImage3.FileName);
                string path3 = Path.Combine(Server.MapPath("~/Photos/"), filename3);
                ProductImage3.SaveAs(path3);
                product.ProductImage3 = "~/Photos/" + filename3;

                Handler.UpdateProduct(product);
                return RedirectToAction("Index", "Category");
            }

            ViewBag.CategoryId = new SelectList(Context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(Context.SubCategories, "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        public ActionResult Delete(int? Id)
        {

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = Handler.GetProductById(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? Id)
        {
            if (ModelState.IsValid)
            {
                Handler.DeleteProduct(Id);
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}