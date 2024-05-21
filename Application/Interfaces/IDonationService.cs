using FoodShareNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Interfaces
{
    public interface IDonationService
    {
        Task<bool> CreateDonation(Donation donation);
        Task<Donation> GetDonation(int id);
        Task<IList<Donation>> GetDonationsByCityId(int cityId);
    }
}
