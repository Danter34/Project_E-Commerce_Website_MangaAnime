namespace Atsumaru.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? tag { get; set; }
        public string? Detail { get; set; }
        public decimal? Price { get; set; }
        public bool Banner { get; set; }
        public bool Hotupdate { get; set; }
        public bool top10 {  get; set; }
        public bool Newproduct { get; set; }
        public bool Lastupdate { get; set; }
        public string? Image {  get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set;}
        public int categoryId { get; set; }
        public category? category { get; set; }
        public int typeId { get; set; }
        public type? type { get; set; }
    }
}
