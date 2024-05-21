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
    public class OrderService : IOrderService
    {
        private readonly IFoodShareDbContext _context;

        public OrderService(IFoodShareDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            if (order.Quantity < 0)
            {
                throw new OrderException("Quantity can't be negative!");
            }

            var donation = await _context.Donations.
            FirstOrDefaultAsync(d => d.Id == order.DonationId);

            if (donation == null)
            {
                throw new NotFoundException("Donation",order.DonationId.ToString());
            }

            if (donation.Quantity < order.Quantity)
            {
                throw new OrderException("Order Quantity Exceeds available Donation Quantity!");
            }

            donation.Quantity -=order.Quantity;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders
            .Include(d => d.Beneficiary)
            .Include(d => d.Courier)
            .Include(d => d.Donation)
            .Include(d => d.Donation.Product)
            .Include(d => d.OrderStatus)
            .FirstOrDefaultAsync(d => d.Id == id);

            if (order == null)
            {
                throw new NotFoundException("Order", id.ToString());
            }

            return order;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, Domain.Enums.OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order",orderId.ToString());
            }

            if (!_context.OrderStatuses.Any(s => s.Id == (int)status))
            {
                throw new NotFoundException("Status",(int)status);
            }

            order.OrderStatusId = (int)status;
            order.DeliveryDate = (int)status == (int)Domain.Enums.OrderStatus.Delivered ? DateTime.UtcNow : order.DeliveryDate;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
