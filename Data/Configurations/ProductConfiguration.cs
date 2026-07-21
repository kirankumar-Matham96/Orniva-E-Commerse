using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrnivaApi.Entities;

namespace OrnivaApi.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table Name
            builder.ToTable("Products");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Name
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            // Description
            builder.Property(p => p.Description)
                   .IsRequired();

            // Short Description
            builder.Property(p => p.ShortDescription)
                   .HasMaxLength(500);

            // Image Url
            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(500);

            // Price
            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            // Descount Price
            builder.Property(p => p.DiscountPrice)
                   .HasPrecision(18, 2);

            // Brand
            builder.Property(p => p.Brand)
                   .HasMaxLength(100);

            // SKU
            builder.Property(p => p.SKU)
                   .HasMaxLength(50);

            builder.HasIndex(p => p.SKU)
                   .IsUnique();

            // Slug
            builder.Property(p => p.Slug)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.HasIndex(p => p.Slug)
                   .IsUnique();

            // Stock
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Product_StockQuantity",
                    "[StockQuantity] >= 0");
            });

            // category id
            builder.Property(p => p.CategoryId)
                   .IsRequired();

            // Relationship
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
