using Oxygen_Atom.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oxygen_Atom.Models
{
    public class ProductDetailModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Product Image 1")]
        public string ProductImage1 { get; set; }

        [Display(Name = "Product Image 2")]
        public string ProductImage2 { get; set; }

        [Display(Name = "Product Image 3")]
        public string ProductImage3 { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string Description { get; set; }
        public List<ProductSizes> SizesAvailable { get; set; }
    }
}