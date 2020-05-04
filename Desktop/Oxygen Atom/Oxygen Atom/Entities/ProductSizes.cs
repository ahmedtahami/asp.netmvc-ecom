using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Oxygen_Atom.Entities
{
    public class ProductSizes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Size")]
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual Sizes Sizes { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product{ get; set; }
        public int Quantity { get; set; }
    }
}