using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnivaApi.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [StringLength(100)]
        public string? Brand { get; set; }

        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; } = null!;
    }
}
