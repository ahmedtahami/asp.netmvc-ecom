using System.Collections.Generic;
using Oxygen_Atom.Entities;

namespace Oxygen_Atom.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}