namespace Atsumaru.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public Product? Product { get; set; }
        public int Qty { get; set; }
        public string? CartId { get; set; }
    }
}
