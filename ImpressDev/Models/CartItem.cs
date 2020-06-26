namespace ImpressDev.Models
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}