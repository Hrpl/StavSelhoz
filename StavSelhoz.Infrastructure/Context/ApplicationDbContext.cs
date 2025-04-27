using StavSelhoz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<FinanceEntity> Finances { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderProductsEntity> OrderProducts { get; set; }
    public DbSet<OrderStatusEntity> OrderStatus { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProviderEntity> Providers { get; set; }
    public DbSet<ProviderProductsEntity> ProviderProducts { get; set; }
    public DbSet<DeportamentsEntity> Deportaments { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }
}
