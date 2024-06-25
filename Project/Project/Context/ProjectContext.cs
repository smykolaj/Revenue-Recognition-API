using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services.Interfaces;
using Version = Project.Models.Version;

namespace Project.Context;

public class ProjectContext : DbContext
{
    protected ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Company> Companies { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Version> Versions { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractDiscount> ContractDiscounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if (entry.State == EntityState.Deleted && entity is ISoftDelete)
            {
                entry.State = EntityState.Modified;
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            }
        }
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if(entry.State == EntityState.Deleted && entity is ISoftDelete)
            {
                entry.State = EntityState.Modified;
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Company)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.IdCompany)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Individual)
            .WithMany(i => i.Contracts)
            .HasForeignKey(c => c.IdIndividual)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Software)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.IdSoftware)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Version)
            .WithMany(v => v.Contracts)
            .HasForeignKey(c => c.IdVersion)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Version>()
            .HasOne(v => v.Software)
            .WithMany(s => s.Versions)
            .HasForeignKey(v => v.IdSoftware)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}