﻿using Microsoft.EntityFrameworkCore;

namespace PC_Builder.Models
{
    public class ApplicationContext : DbContext
    {
        // DbSet коллекции объектов, которые сопоставляются с определенной таблицей в базе данных.
        public DbSet<CPU> CPUs { get; set; } = null!;
        public DbSet<Cooling> Coolings { get; set; } = null!;
        public DbSet<Motherboard> Motherboards { get; set; } = null!;
        public DbSet<RAM> RAMs { get; set; } = null!;
        public DbSet<GPU> GPUs { get; set; } = null!;
        public DbSet<DataStorage> DataStorages { get; set; } = null!;
        public DbSet<Case> Cases { get; set; } = null!;
        public DbSet<PowerSupply> PowerSupplies { get; set; } = null!;
        public DbSet<ProductInCart> ProductsInCart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; } 
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}