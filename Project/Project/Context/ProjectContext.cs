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

        modelBuilder.Entity<AppUser>().HasData(new List<AppUser>()
        {
            new ()
            {
                IdUser = 1,
                Email = "a@a.com",
                Login = "admin",
                Password = "admin",
                IsAdmin = true
            }
        });

        modelBuilder.Entity<Category>().HasData(new List<Category>()
        {
            new()
            {
                IdCategory = 1,
                CategoryName = "Business"
            },
            new ()
            {
                IdCategory = 2,
                CategoryName = "Education"
            }
        });

       modelBuilder.Entity<Software>().HasData(new List<Software>
            {
                new Software
                {
                    IdSoftware = 1,
                    Name = "Business Suite",
                    Description = "A comprehensive business management software",
                    IdCategory = 1,
                    Price = 5000.00m
                },
                new Software
                {
                    IdSoftware = 2,
                    Name = "Education Hub",
                    Description = "An interactive educational platform",
                    IdCategory = 2,
                    Price = 3000.00m
                }
            });

            modelBuilder.Entity<Version>().HasData(new List<Version>
            {
                new Version
                {
                    IdVersion = 1,
                    VersionNumber = "1.0",
                    Date = new DateTime(2024, 1, 1),
                    Comments = "Initial release",
                    IdSoftware = 1
                },
                new Version
                {
                    IdVersion = 2,
                    VersionNumber = "2.0",
                    Date = new DateTime(2024, 6, 1),
                    Comments = "Major update",
                    IdSoftware = 2
                },
                new Version
                {
                    IdVersion = 3,
                    VersionNumber = "2.1",
                    Date = new DateTime(2024, 12, 1),
                    Comments = "Minor update",
                    IdSoftware = 2
                }
            });

            modelBuilder.Entity<Discount>().HasData(new List<Discount>
            {
                new Discount
                {
                    IdDiscount = 1,
                    Name = "Summer Sale",
                    Offer = "15% off",
                    Value = 15.0m,
                    TimeStart = new DateTime(2024, 6, 1),
                    TimeEnd = new DateTime(2024, 6, 30)
                },
                new Discount
                {
                    IdDiscount = 2,
                    Name = "Winter Sale",
                    Offer = "25% off",
                    Value = 25.0m,
                    TimeStart = new DateTime(2024, 12, 1),
                    TimeEnd = new DateTime(2024, 12, 31)
                }
            });

        base.OnModelCreating(modelBuilder);
    }
}