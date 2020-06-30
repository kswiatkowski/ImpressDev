namespace ImpressDev.ViewModels
{
    public class CartRemoveViewModel
    {
        public decimal CartTotalPrice { get; set; }
        public int CartItemsQuantity { get; set; }
        public int RemoveBookId { get; set; }
        public int RemoveBookQuantity { get; set; }
    }
}