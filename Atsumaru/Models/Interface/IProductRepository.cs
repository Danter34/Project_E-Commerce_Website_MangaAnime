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
    }
}
