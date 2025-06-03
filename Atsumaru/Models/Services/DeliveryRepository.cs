using Atsumaru.Data;
using Atsumaru.Models.Interface;

namespace Atsumaru.Models.Services
{
    public class DeliveryRepository: IDeliveryRepository
    {
        private AtsumaruContextDB db;
        private ICartRepository CartRepository;

        public DeliveryRepository(AtsumaruContextDB db, ICartRepository CartRepository)
        {
            this.db = db;
            this.CartRepository = CartRepository;
        }

        public void PlaceDelivery(Delivery delivery)
        {
            var CartItems = CartRepository.GetAllCartItems();
            delivery.DeliveryDetails = new List<DeliveryDetail>();
            foreach (var item in CartItems)
            {
                var DeliveryDetail = new DeliveryDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };

                delivery.DeliveryDetails.Add(DeliveryDetail);
            }

            delivery.DeliveryPlaced = DateTime.Now;
            delivery.DeliveryTotal = CartRepository.GetCartTotal();
            db.Deliverys.Add(delivery);
            db.SaveChanges();
        }
    }
}
