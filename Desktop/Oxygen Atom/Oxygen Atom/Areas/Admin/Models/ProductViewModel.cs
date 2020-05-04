using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oxygen_Atom.Entities;

namespace Oxygen_Atom.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public virtual Product Product { get; set; }
        public List<Product> Products { get; set; }
        public ICollection<ProductSizes> SizesAvailable { get; set; }
        public virtual ProductSizes ProductSize { get; set; }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
    }
}