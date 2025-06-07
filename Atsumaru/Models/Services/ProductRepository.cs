
using Atsumaru.Data;
using Atsumaru.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 

namespace Atsumaru.Models.Services
{
    public class ProductRepository : IProductRepository
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

        public async Task<Product?> GetProductDetailAsync(int id)
        {
            return await db.Products.FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<bool> IsProductInWishlistAsync(int productId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false; 
            }
            return await db.WishlistItems.AnyAsync(wi => wi.ProductId == productId && wi.UserId == userId);
        }


        public async Task<bool> ToggleWishlistAsync(int productId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false; 
            }

            var existingItem = await db.WishlistItems
                                       .FirstOrDefaultAsync(wi => wi.ProductId == productId && wi.UserId == userId);

            if (existingItem != null)
            {
                
                db.WishlistItems.Remove(existingItem);
                await db.SaveChangesAsync();
                return false; 
            }
            else
            {
               
                var wishlistItem = new WishlistItem
                {
                    ProductId = productId,
                    UserId = userId,
                    AddedDate = DateTime.Now 
                };
                db.WishlistItems.Add(wishlistItem);
                await db.SaveChangesAsync();
                return true; 
            }
        }
        public async Task<List<Product>> GetProductsInWishlistAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new List<Product>(); // Trả về danh sách rỗng nếu userId trống
            }

            // Lấy ProductId từ WishlistItems của người dùng
            var productIdsInWishlist = await db.WishlistItems
                                                .Where(wi => wi.UserId == userId)
                                                .Select(wi => wi.ProductId)
                                                .ToListAsync();

            // Lấy thông tin chi tiết các sản phẩm từ danh sách ProductIds
            var products = await db.Products
                                 .Where(p => productIdsInWishlist.Contains(p.Id))
                                 .ToListAsync();

            return products;
        }
    }
}