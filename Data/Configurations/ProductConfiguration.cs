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

            // Price
            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            // Brand
            builder.Property(p => p.Brand)
                   .HasMaxLength(100);

            // SKU
            builder.Property(p => p.SKU)
                   .HasMaxLength(50);

            builder.HasIndex(p => p.SKU)
                   .IsUnique();

            // Relationship
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
