using Oxygen_Atom.Areas.Admin.Handlers;
using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Oxygen_Atom.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductSizeController : Controller
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        private ShopHandler Handler = new ShopHandler();
        // GET: Admin/ProductSize
        public ActionResult Index()
        {
            List<ProductSizes> modelList = Handler.GetProductSizes().ToList();
            return View(modelList);
        }

        public ActionResult Create()
        {
            ViewBag.SizeId = new SelectList(Context.Sizes, "Id", "Name");
            ViewBag.ProductId = new SelectList(Context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductSizes productSizes)
        {
            if (ModelState.IsValid)
            {
                Handler.AddProductSize(productSizes);

                List<ProductSizes> list = Handler.GetProductByName(Handler.GetProductById(productSizes.ProductId).Name);
                int quantity = 0;
                foreach (ProductSizes item in list)
                {
                    quantity += item.Quantity;
                }

                Handler.AddStock(new Stock()
                {
                    ProductId = productSizes.ProductId,
                    Quantity = quantity

                });

                return RedirectToAction("Index", "ProductSize");

            }
            ViewBag.SizeId = new SelectList(Context.Categories, "Id", "Name", productSizes.SizeId);
            ViewBag.ProductId = new SelectList(Context.Categories, "Id", "Name", productSizes.ProductId);
            return View(productSizes);
        }
        public ActionResult Edit(int? Id)
        {
            ViewBag.SizeId = new SelectList(Context.Sizes, "Id", "Name");
            ViewBag.ProductId = new SelectList(Context.Products, "Id", "Name");

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductSizes productSize = Handler.GetProductSize(Id);
            if (productSize == null)
            {
                return HttpNotFound();
            }
            return View(productSize);
        }

        [HttpPost]
        public ActionResult Edit(ProductSizes productSizes)
        {
            if (ModelState.IsValid)
            {
                Handler.UpdateProductSize(productSizes);
                return RedirectToAction("Index", "ProductSize");
            }

            ViewBag.SizeId = new SelectList(Context.Categories, "Id", "Name", productSizes.SizeId);
            ViewBag.ProductId = new SelectList(Context.Categories, "Id", "Name", productSizes.ProductId);

            return View(productSizes);
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductSizes productSize = Handler.GetProductSize(Id);
            if (productSize == null)
            {
                return HttpNotFound();
            }
            return View(productSize);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                Handler.DeleteProductSize(id);
                return RedirectToAction("Index", "ProductSize");
            }
            return View();
        }

    }
}