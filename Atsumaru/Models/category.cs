namespace Atsumaru.Models
{
    public class category
    {
        public int categoryId { get; set; }
        public string? categoryname { get; set; }
        public List<Product> Products { get; set; }
    }
}
