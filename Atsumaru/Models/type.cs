namespace Atsumaru.Models
{
    public class type
    {
        public int typeId { get; set; }
        public string? typename { get; set; }

        public List<Product> Products { get; set; }
    }
}
