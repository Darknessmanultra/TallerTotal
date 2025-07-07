using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerBack.src.dtos;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitofWork _uow;

        public OrderService(IUnitofWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CheckoutAsync(Guid userId)
        {
            var cart = await _uow.Carts.GetByUserIdAsync(userId);
            if (cart == null || !cart.Products.Any())
                throw new InvalidOperationException("Cart is empty.");

            var productMap = await _uow.Products
                .Query(p => !p.IsDeleted && cart.Products.Select(ci => ci.ProductId).Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var order = new models.Order
            {
                UserId = userId,
                Items = cart.Products.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = productMap[i.ProductId].Name,
                    PriceAtPurchase = productMap[i.ProductId].Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _uow.Orders.AddAsync(order);
            await _uow.Carts.RemoveAsync(cart);
            await _uow.SaveChangesAsync();

            return order.Id;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
        {
            return await _uow.Orders.GetAllByUserIdAsync(userId);
        }
        public async Task<IEnumerable<OrderDTO>> GetUserOrdersDtoAsync(Guid userId)
        {
            var orders = await _uow.Orders.GetAllByUserIdAsync(userId);

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.Id,
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDTO
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    PriceAtPurchase = i.PriceAtPurchase,
                    Quantity = i.Quantity
                }).ToList()
            });
        }

    }
}