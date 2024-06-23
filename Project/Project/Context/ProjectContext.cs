using Microsoft.EntityFrameworkCore;
using Project.Models;
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
    public DbSet<PaymentContract> PaymentContracts { get; set; }
    
}