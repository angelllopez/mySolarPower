using Microsoft.EntityFrameworkCore;

namespace MySolarPower.Data.Models;

public partial class PowerUsageDbContext : DbContext
{
    public PowerUsageDbContext()
    {
    }

    public PowerUsageDbContext(DbContextOptions<PowerUsageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GridPowerBillingHistory> GridPowerBillingHistories { get; set; }

    public virtual DbSet<GridPowerDetail> GridPowerDetails { get; set; }

    public virtual DbSet<SolarPower> SolarPowers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GridPowerBillingHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GridPowerBillingHistory_Id");
        });

        modelBuilder.Entity<GridPowerDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GridPowerDetail_Id");
        });

        modelBuilder.Entity<SolarPower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SolarPower_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
