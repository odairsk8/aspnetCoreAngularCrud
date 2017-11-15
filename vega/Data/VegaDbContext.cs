using Microsoft.EntityFrameworkCore;
using Vega.Core.Entities;

namespace vega
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Make> Make { get; set; }
        public DbSet<Feature> Feature { get; set; }

        public DbSet<Model> Model { get; set; }

        public DbSet<Photo> Photo { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            modelBuider.Entity<VehicleFeature>()
            .HasKey(vf => new
            {
                vf.VehicleId,
                vf.FeatureId
            });
        }
    }
}