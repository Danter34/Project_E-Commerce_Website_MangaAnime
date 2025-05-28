using Atsumaru.Data;
using Atsumaru.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Atsumaru.Models.Services
{
    public class ProductRepository:IProductRepository
    {
        private AtsumaruContextDB db;
        public ProductRepository(AtsumaruContextDB db)
        {
            this.db = db;
        }
        public IEnumerable<Product> GetAllProducts() 
        {
            return db.Products;
        }
        public Product? GetProductDetail(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetBanner()
        {
            return db.Products.Where(p => p.Banner);
        }
        public IEnumerable<Product> GetHotupdate()
        {
            return db.Products.Where(p => p.Hotupdate);
        }
        public IEnumerable<Product> Gettop10()
        {
            return db.Products.Where(p => p.top10);
        }
        public IEnumerable<Product> GetNewproduct()
        {
            return db.Products.Where(p => p.Newproduct);
        }
        public IEnumerable<Product> GetLastupdate()
        {
            return db.Products.Where(p => p.Lastupdate);
        }
    }
}
