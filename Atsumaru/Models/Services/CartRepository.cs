using Atsumaru.Data;
using Atsumaru.Models.Interface;
using Microsoft.EntityFrameworkCore;
namespace Atsumaru.Models.Services
{
    public class CartRepository:ICartRepository
    {
        private AtsumaruContextDB db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(AtsumaruContextDB db)
        {
            this.db = db;
        }
        public CartRepository(AtsumaruContextDB db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;

            InitializeCartId();
        }
        private void InitializeCartId()
        {
            ISession? session = _httpContextAccessor.HttpContext?.Session;
            string? storedCartId = session?.GetString("CartId");

            if (string.IsNullOrEmpty(storedCartId))
            {
                CartId = Guid.NewGuid().ToString();
                session?.SetString("CartId", CartId);
            }
            else
            {
                CartId = storedCartId;
            }
        }
        public List<Cart>? carts { get; set; }
        public string? CartId { get; set; }


        public static CartRepository GetCart(IServiceProvider services)
        {
            // Get both required services from the IServiceProvider
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            AtsumaruContextDB context = services.GetService<AtsumaruContextDB>()
                ?? throw new Exception("Error initializing AtsumaruContextDB"); 
            IHttpContextAccessor httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>(); 

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            
            return new CartRepository(context, httpContextAccessor)
            {
                CartId = cartId 
            };
        }

        public void AddCart(Product product)
        {
            var CartItem = db.Carts.FirstOrDefault(s =>
                s.Product.Id == product.Id && s.CartId == CartId);

            if (CartItem == null)
            {
                CartItem = new Cart
                {
                    CartId = CartId,
                    Product = product,
                    Qty = 1,
                };
                db.Carts.Add(CartItem);
            }
            else
            {
                CartItem.Qty++;
            }

            db.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = db.Carts
                .Where(s => s.CartId == CartId);

            db.Carts.RemoveRange(cartItems);
            db.SaveChanges();
        }

        public List<Cart> GetAllCartItems()
        {
            return carts ??= db.Carts
                .Where(s => s.CartId == CartId)
                .Include(p => p.Product)
                .ToList();
        }

        public decimal GetCartTotal()
        {
            var totalCost = db.Carts
                .Where(s => s.CartId == CartId)
                .Select(s => s.Product.Price * s.Qty)
                .Sum();

            return totalCost;
        }

        public int RemoveCart(Product product)
        {
            var CartItem = db.Carts.FirstOrDefault(s =>
                s.Product.Id == product.Id && s.CartId == CartId);

            var quantity = 0;

            if (CartItem != null)
            {
                db.Carts.Remove(CartItem);
            }

            db.SaveChanges();
            return quantity;
        }
        public async Task<bool> IncreaseQuantity(string productId)
        {
            if (!int.TryParse(productId, out int parsedProductId))
            {
                return false;
            }

            var cartItem = await db.Carts.FirstOrDefaultAsync(s =>
                s.Product.Id == parsedProductId && s.CartId == CartId);
            if (cartItem == null)
            {
                return false;
            }

            cartItem.Qty++;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DecreaseQuantity(string productId)
        {

            if (!int.TryParse(productId, out int parsedProductId))
            {
                return false;
            }
            var cartItem = await db.Carts.FirstOrDefaultAsync(s =>
                s.Product.Id == parsedProductId && s.CartId == CartId);

            if (cartItem == null)
            {
                return false; 
            }

            if (cartItem.Qty > 1)
            {
                cartItem.Qty--;
            }
            else
            {
            
                db.Carts.Remove(cartItem);
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}
