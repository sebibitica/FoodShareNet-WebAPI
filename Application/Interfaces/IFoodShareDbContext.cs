using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodShareNet.Application.Interfaces
{
    public interface IFoodShareDbContext
    {
        DbSet<Courier> Couriers { get; }
        DbSet<Beneficiary> Beneficiaries { get; }
        DbSet<Donor> Donors { get; }
        DbSet<Donation> Donations { get; }
        DbSet<DonationStatus> DonationStatuses { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderStatus> OrderStatuses { get; }
        DbSet<City> Cities { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
