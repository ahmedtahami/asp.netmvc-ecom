using Oxygen_Atom.Entities;
using System.Collections;
using System.Collections.Generic;
using Oxygen_Atom.Areas.Admin.Handlers;

namespace Oxygen_Atom.Models
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> NewProducts { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<ProductSizes> ProductSizes { get; set; }
        public virtual Product Product { get; set; }
    }
}