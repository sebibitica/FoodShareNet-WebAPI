using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Services
{
    public class DonorService : IDonorService
    {
        private readonly IFoodShareDbContext _context;

        public DonorService(IFoodShareDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Donor>> GetAllAsync()
        {
            var donors = await _context.Donors
                .Include(d => d.City)
                .Include(d => d.Donations)
                    .ThenInclude(donation => donation.Product)
                .Include(d => d.Donations)
                    .ThenInclude(donation => donation.Status)
                .ToListAsync();
            return donors;
        }

        public async Task<Donor> GetAsync(int id)
        {
            var donor = await _context.Donors
                .Include(d => d.City)
                .Include(d => d.Donations)
                    .ThenInclude(donation => donation.Product)
                .Include(d => d.Donations)
                    .ThenInclude(donation => donation.Status)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (donor == null)
            {
                throw new NotFoundException("Donor",id);
            }
            return donor;
        }

        public async Task<Donor> CreateAsync(Donor donor)
        {
            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            return donor;
        }

        public async Task<bool> EditAsync(int id, Donor editDonor)
        {
            var donor = await _context.Donors
                .FirstOrDefaultAsync(b => b.Id == editDonor.Id);

            if (donor == null)
            {
                throw new NotFoundException("Donor", id);
            }

            donor.Name = editDonor.Name;
            donor.CityId = editDonor.CityId;
            donor.Address = editDonor.Address;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
            {
                throw new NotFoundException("Donor", id);
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
