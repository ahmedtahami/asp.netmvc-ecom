using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oxygen_Atom.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public int CartId { get; set; }
        public int Count { get; set; }
    }
}