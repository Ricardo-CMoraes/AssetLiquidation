using Microsoft.EntityFrameworkCore;

namespace AssetLiquidation.Core;

public class LiquidateDbContext : DbContext
{
    public LiquidateDbContext(DbContextOptions<LiquidateDbContext> options)
    : base(options) {}

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Occurrence> Occurrences =>Set<Occurrence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Asset>(entity =>
         {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.AssetId).IsRequired().HasMaxLength(50);
            entity.Property(a => a.InitialAmount).HasPrecision(18, 2);
            entity.Property(a => a.CurrentAmount).HasPrecision(18, 2);
         });

         modelBuilder.Entity<Occurrence>(entity =>
         {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Amount).HasPrecision(18,2);
            entity.HasOne<Asset>().WithMany().HasForeignKey(o => o.AssetId);
         });
    }
}
