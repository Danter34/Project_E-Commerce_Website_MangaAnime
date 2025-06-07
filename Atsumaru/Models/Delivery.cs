namespace Atsumaru.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal DeliveryTotal { get; set; }
        public DateTime DeliveryPlaced { get; set; }
        public string Status { get; set; } = "Pending";
        public List<DeliveryDetail>? DeliveryDetails  { get; set; }
    }
}
