using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Services
{
    public class DonationService : IDonationService
    {
        private readonly IFoodShareDbContext _context;

        public DonationService(IFoodShareDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDonation(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Donation> GetDonation(int id)
        {
            var donation = await _context.Donations
                .Include(m => m.Donor)
                .Include(m => m.Status)
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                throw new NotFoundException("Donation",id);
            }
            return donation;
        }

        public async Task<IList<Donation>> GetDonationsByCityId(int cityId)
        {
            var donations = await _context.Donations
            .Include(m => m.Donor)
            .Include(m => m.Status)
            .Include(m => m.Product)
            .Where(m => m.Donor.CityId == cityId)
            .ToListAsync();

            return donations;
        }
    }
}
