using System;
using System.Collections.Generic;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Localhost;Database=PowerUsageDb;Trusted_Connection=True;TrustServerCertificate=True");

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
