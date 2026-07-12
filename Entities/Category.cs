using System.ComponentModel.DataAnnotations;

namespace OrnivaApi.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
