namespace Atsumaru.Models.ViewModel
{
    public enum ClassificationType
    {
        Category,
        Type
    }

    public class ClassificationViewModel
    {
        public ClassificationType CurrentType { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; } 
        public IEnumerable<category>? Categories { get; set; }
        public IEnumerable<type>? Types { get; set; }
    }
}
