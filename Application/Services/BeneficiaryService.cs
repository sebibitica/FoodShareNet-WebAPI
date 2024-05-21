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
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IFoodShareDbContext _context;

        public BeneficiaryService(IFoodShareDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Beneficiary>> GetAllAsync()
        {
            return await _context.Beneficiaries
                .Include(b => b.City)
                .ToListAsync();
        }

        public async Task<Beneficiary> GetAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries
                .Include(b => b.City)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (beneficiary == null)
            {
                throw new NotFoundException("Beneficiary", id.ToString());
            }

            return beneficiary;
        }

        public async Task<Beneficiary> CreateAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();
            return beneficiary;
        }

        public async Task<bool> EditAsync(int id, Beneficiary editBeneficiary)
        {
            var beneficiary = await _context.Beneficiaries
                .FirstOrDefaultAsync(b => b.Id == editBeneficiary.Id);

            if (beneficiary == null)
            {
                throw new NotFoundException("Beneficiary", editBeneficiary.Id.ToString());
            }

            beneficiary.Name = editBeneficiary.Name;
            beneficiary.Address = editBeneficiary.Address;
            beneficiary.CityId = editBeneficiary.CityId;
            beneficiary.Capacity = editBeneficiary.Capacity;

            await _context.SaveChangesAsync();

            return true;
        }

       public async Task<bool> DeleteAsync(int id)
       {
           var beneficiary = await _context.Beneficiaries.FindAsync(id);

           if (beneficiary == null)
           {
                throw new NotFoundException("Beneficiary", id);
           }

           _context.Beneficiaries.Remove(beneficiary);
           await _context.SaveChangesAsync();

           return true;
       }
    }
}
