using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrnivaApi.Entities;

namespace OrnivaApi.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table Name
            builder.ToTable("Category");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Name
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Description
            builder.Property(c => c.Description).HasMaxLength(500);

            // Unique Index
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
