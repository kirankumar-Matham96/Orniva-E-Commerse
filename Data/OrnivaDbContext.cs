using Microsoft.EntityFrameworkCore;
using OrnivaApi.Entities;

namespace OrnivaApi.Data
{
    public class OrnivaDbContext : DbContext
    {
        public OrnivaDbContext(DbContextOptions<OrnivaDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OrnivaDbContext).Assembly);
        }
    }
}
