namespace Atsumaru.Models
{
    public class DeliveryDetail
    {
        public int DeliveryDetailId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int DeliveryId { get; set; }
        public Delivery? Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
