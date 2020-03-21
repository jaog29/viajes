using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Web.Data.Entities;

namespace Viajes.Web.Data.Entities
{
    public class DataContext : IdentityDbContext<UserEntity> { 

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<TripDetailEntity> TripDetails { get; set; }
        public DbSet<CostEntity> Costs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TripEntity>()
                .HasIndex(t => t.Id)
                .IsUnique();
        }

    }

}
