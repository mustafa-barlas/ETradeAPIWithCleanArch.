﻿using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Common;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Persistence.Contexts;

public class ETradeAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ETradeAPIDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<CompletedOrder> CompletedOrders { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }
    public DbSet<Menu> Menus { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

        var datas = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .HasKey(b => b.Id);

        builder.Entity<Order>()
            .HasIndex(o => o.OrderCode)
            .IsUnique();

        builder.Entity<Basket>()
            .HasOne(b => b.Order)
            .WithOne(o => o.Basket)
            .HasForeignKey<Order>(b => b.Id);

        builder.Entity<Order>()
            .HasOne(o => o.CompletedOrder)
            .WithOne(c => c.Order)
            .HasForeignKey<CompletedOrder>(c => c.OrderId);


        base.OnModelCreating(builder);
    }
}