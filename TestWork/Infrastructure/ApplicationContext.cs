using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PositionOrder> PositionOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Иванович"
                },
                new Client
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Петр",
                    LastName = "Петров",
                    MiddleName = "Петрович"
                },
                new Client
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Джон",
                    LastName = "Смит",
                    MiddleName = "Смитович"
                });
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Id = Guid.Parse("dc25f4fb-f483-4a70-ae55-1b54c8fa7e3f"),
                    NameType = "Масла"
                },
                new ProductType
                {
                    Id = Guid.Parse("756aa072-c0e9-4843-9da4-8e52ab4f68ab"),
                    NameType = "Инструменты"
                },
                new ProductType
                {
                    Id = Guid.Parse("87f948cd-f9d9-4af9-964d-b91e51a8e8a1"),
                    NameType = "Аксессуары"
                });
            modelBuilder.Entity<Product>().HasData(
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      NameProduct = "Масло моторное Febi Sae, 5W-40, синтетическое, 5L",
                      PriceProduct = 1716,
                      CountProduct = 100,
                      ProductTypeId = Guid.Parse("dc25f4fb-f483-4a70-ae55-1b54c8fa7e3f")
                  },
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      NameProduct = "моторное масло SAE 5W-30 Longlife",
                      PriceProduct = 533,
                      CountProduct = 15,
                      ProductTypeId = Guid.Parse("dc25f4fb-f483-4a70-ae55-1b54c8fa7e3f")
                  },
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      NameProduct = "Домкрат подкатной 2 тонны",
                      PriceProduct = 2780,
                      CountProduct = 50,
                      ProductTypeId = Guid.Parse("756aa072-c0e9-4843-9da4-8e52ab4f68ab")
                  },
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      NameProduct = "Пылесос CYCLONE-1",
                      PriceProduct = 1740,
                      CountProduct = 30,
                      ProductTypeId = Guid.Parse("87f948cd-f9d9-4af9-964d-b91e51a8e8a1")
                  
                  });
        }
        
    }
}
