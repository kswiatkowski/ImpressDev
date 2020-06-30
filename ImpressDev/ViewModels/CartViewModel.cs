using ImpressDev.Models;
using System.Collections.Generic;

namespace ImpressDev.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}