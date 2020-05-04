using Oxygen_Atom.Entities;
using Oxygen_Atom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Oxygen_Atom.Areas.Admin.Handlers
{
    public class ShopHandler
    {

        #region Category

        public void AddCategory(Category entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Categories.Add(entity);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IQueryable<SubCategory> sb = context.SubCategories.Where(s => s.CategoryId == id);
                Category found = context.Categories.FirstOrDefault(p => p.Id == id);
                context.SubCategories.RemoveRange(sb);
                context.Categories.Remove(found);
                context.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Categories
                    .Include(p => p.SubCategories)
                    .ToList();
            }
        }

        public Category GetCategory(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Categories.FirstOrDefault(p => p.Id == id);
            }
        }

        #endregion

        #region SubCategory

        public void AddSubCategory(SubCategory entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.SubCategories.Add(entity);
                context.SaveChanges();
            }
        }

        public void UpdateSubCategory(SubCategory entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteSubCategory(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                SubCategory found = context.SubCategories.FirstOrDefault(p => p.Id == id);
                context.SubCategories.Remove(found);
                context.SaveChanges();
            }
        }

        public List<SubCategory> GetSubCategories()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.SubCategories
                    .Include(p => p.Category)
                    .ToList();
            }
        }

        public SubCategory GetSubCategory(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.SubCategories
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public List<SubCategory> GetSubCategoriesByCategory(Category entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.SubCategories.Include(p => p.Category)
                    .Where(p => p.Category == entity)
                    .ToList();
            }
        }

        #endregion

        #region Product

        public void AddProduct(Product entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Products.Add(entity);
                context.SaveChanges();
            }


        }

        public void UpdateProduct(Product entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Product found = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
                context.Products.Remove(found);
                context.SaveChanges();
            }
        }

        public List<Product> GetProducts()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products
                    .Include(p => p.SubCategory)
                    .Include(p => p.Category)
                    .ToList();
            }
        }

        public Product GetProductById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products
                    .Include(p => p.SubCategory)
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public Product GetProduct(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products.Single(p => p.Id == id);
            }
        }

        public List<Product> GetFeaturedProducts()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsFeatured)
                    .ToList();

            }
        }

        public List<Product> GetNewProducts(int count)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.StartDate)
                    .Take(count)
                    .ToList();
            }
        }

        public List<Product> GetProductsBySubCategory(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products.Where(p => p.SubCategoryId == id)
                    .Include(p => p.SubCategory)
                    .Include(p => p.Category)
                    .ToList();
            }
        }

        public List<Product> GetProductsByCategory(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Products.Where(p => p.CategoryId == id)
                    .Include(p => p.SubCategory)
                    .Include(p => p.Category)
                    .ToList();
            }
        }

        #endregion

        #region Sizes

        public void AddSize(Sizes entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Sizes.Add(entity);
                context.SaveChanges();
            }
        }
        public void UpdateSize(Sizes entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteSize(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Sizes found = context.Sizes.FirstOrDefault(p => p.Id == id);
                context.Sizes.Remove(found);
                context.SaveChanges();
            }
        }

        public List<Sizes> GetSizes()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Sizes.ToList();
            }
        }

        public Sizes GetSize(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Sizes.FirstOrDefault(p => p.Id == id);
            }
        }

        #endregion

        #region ProductSizes
        public void AddProductSize(ProductSizes entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                ProductSizes temp = context.ProductSizes
                    .Where(p => p.ProductId == entity.ProductId && p.SizeId == entity.SizeId)
                    .SingleOrDefault();

                if (temp != null)
                {
                    temp.Quantity = temp.Quantity + entity.Quantity;
                    context.Entry(temp).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(entity).State = EntityState.Added;
                }

                context.SaveChanges();

            }
        }
        public void UpdateProductSize(ProductSizes entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProductSize(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ProductSizes found = context.ProductSizes.FirstOrDefault(p => p.Id == id);
                context.ProductSizes.Remove(found);
                context.SaveChanges();
            }
        }

        public List<ProductSizes> GetProductSizes()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ProductSizes
                    .Include(p => p.Product)
                    .Include(p => p.Sizes)
                    .ToList();
            }
        }
        public ProductSizes GetProductSize(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ProductSizes
                    .Include(p => p.Product)
                    .Include(p => p.Sizes)
                    .FirstOrDefault(p => p.ProductId == id);

            }
        }

        public List<ProductSizes> GetProductByName(string name)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ProductSizes
                    .Include(p => p.Product)
                    .Where(p => p.Product.Name == name).ToList();
            }
        }

        public List<ProductSizes> GetProductSizeByProductId(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ProductSizes
                    .Where(p => p.ProductId == id)
                    .Include(p => p.Sizes)
                    .ToList();
            }
        }
        #endregion

        #region Stock

        public void AddStock(Stock entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                //context.Stock.Add(entity);
                //context.SaveChanges();
                Stock temp = context.Stock
                    .Where(p => p.ProductId == entity.ProductId)
                    .FirstOrDefault();

                if (temp != null)
                {
                    temp.Quantity = entity.Quantity;
                    context.Entry(temp).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(entity).State = EntityState.Added;
                }

                context.SaveChanges();
            }
        }

        public List<Stock> GetStockedProducts()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Stock
                    .Include(p => p.Product)
                    .ToList();
            }
        }

        #endregion
    }
}
