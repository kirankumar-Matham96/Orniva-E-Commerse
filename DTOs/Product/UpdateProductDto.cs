namespace OrnivaApi.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        public string? Brand { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}