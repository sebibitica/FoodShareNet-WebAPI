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
    public class ProductService : IProductService
    {
        private readonly IFoodShareDbContext _context;

        public ProductService(IFoodShareDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
