namespace Atsumaru.Models.Interface
{
    public interface ICartRepository
    {
        void AddCart(Product product);
        int RemoveCart(Product product);
        List<Cart> GetAllCartItems();
        void ClearCart();
        decimal GetCartTotal();

        Task<bool> IncreaseQuantity(string productId);
        Task<bool> DecreaseQuantity(string productId);
        public List<Cart> carts { get; set; }

    }
}
