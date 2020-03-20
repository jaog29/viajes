using Microsoft.EntityFrameworkCore;

namespace Viajes.Web.Data.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TripEntity> Trips { get; set; }

        public DbSet<CostEntity> Cost { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TripEntity>()
                .HasIndex(t => t.Id)
                .IsUnique();
        }

    }

}
