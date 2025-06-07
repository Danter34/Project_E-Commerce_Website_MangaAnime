namespace Atsumaru.Models.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetBanner();
        IEnumerable<Product> GetHotupdate();
        IEnumerable<Product> Gettop10();
        IEnumerable<Product> GetNewproduct();
        IEnumerable<Product> GetLastupdate();
        Product GetProductDetail(int id);
        Task<bool> IsProductInWishlistAsync(int productId, string userId);
        Task<bool> ToggleWishlistAsync(int productId, string userId);
        Task<List<Product>> GetProductsInWishlistAsync(string userId);
    }
}
