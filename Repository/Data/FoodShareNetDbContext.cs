using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Repository.Data
{
    public class FoodShareNetDbContext : DbContext , IFoodShareDbContext
    {
        public FoodShareNetDbContext(DbContextOptions<FoodShareNetDbContext> options) : base(options) { }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationStatus> DonationStatuses { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Bucuresti" },
                new City { Id = 2, Name = "Cluj-Napoca" },
                new City { Id = 3, Name = "Timisoara" },
                new City { Id = 4, Name = "Arad" }
            );

            modelBuilder.Entity<Courier>().HasData(
                new Courier { Id = 1, Name = "DPD", Price = 20 },
                new Courier { Id = 2, Name = "DHL", Price = 15 },
                new Courier { Id = 3, Name = "GLS", Price = 17.5M }
            );

            modelBuilder.Entity<DonationStatus>().HasData(
                new DonationStatus { Id = 1, Name = "Pending" },
                new DonationStatus { Id = 2, Name = "Approved" },
                new DonationStatus { Id = 3, Name = "Rejected" }
            );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Unconfirmed" },
                new OrderStatus { Id = 2, Name = "Confirmed" },
                new OrderStatus { Id = 3, Name = "InDelivery" },
                new OrderStatus { Id = 4, Name = "Delivered" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Tomatoes" , Image= "https://www.tastingtable.com/img/gallery/20-tricks-to-make-your-tomatoes-even-more-delicious/intro-1684770527.jpg" },
                new Product { Id = 2, Name = "Potatoes" , Image= "https://www.foodrepublic.com/img/gallery/10-things-you-probably-didnt-know-about-potatoes/l-intro-1689785695.jpg" },
                new Product { Id = 3, Name = "Meat" , Image= "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2020/06/meat-1200x675.jpg" }
            );

            modelBuilder.Entity<Donation>().HasData(
                new Donation { Id=1, DonorId=1, ProductId=1, Quantity=4, StatusId=1}
            );

            modelBuilder.Entity<Donor>().HasData(
                new Donor { Id = 1, Name="Mihai",CityId=1,Address="adr1"},
                new Donor { Id = 2, Name = "Dan", CityId = 2, Address = "adr2" }
            );

            modelBuilder.Entity<Courier>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Donation)
                .WithMany()
                .HasForeignKey(o => o.DonationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }

}
