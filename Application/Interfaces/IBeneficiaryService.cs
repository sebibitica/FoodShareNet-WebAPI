using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodShareNet.Domain.Entities;

namespace FoodShareNet.Application.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<IList<Beneficiary>> GetAllAsync();
        Task<Beneficiary> GetAsync(int id);
        Task<Beneficiary> CreateAsync(Beneficiary createBeneficiary);
        Task<bool> EditAsync(int id, Beneficiary editBeneficiary);
        Task<bool> DeleteAsync(int id);
    }
}
