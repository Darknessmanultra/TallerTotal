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
    public class CartService : ICartService
    {
        private readonly IUnitofWork _uow;

        public CartService(IUnitofWork uow)
        {
            _uow = uow;
        }

        public async Task<Cart> GetCartAsync(Guid userId)
        {
            var cart = await _uow.Carts.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _uow.Carts.AddAsync(cart);
                await _uow.Carts.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddOrUpdateItemAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Products.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
                cart.Products.Add(new CartProduct { ProductId = productId, Quantity = quantity });
            else
                item.Quantity += quantity;

            await _uow.Carts.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid userId, Guid productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Products.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Products.Remove(item);
                await _uow.Carts.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await GetCartAsync(userId);
            cart.Products.Clear();
            await _uow.Carts.SaveChangesAsync();
        }

        public async Task<CartDTO> GetCartDtoAsync(Guid userId)
        {
            var cart = await _uow.Carts.GetByUserIdAsync(userId);
            var products = await _uow.Products
                .Query(p => cart.Products.Select(i => i.ProductId).Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            return new CartDTO
            {
                CartId = cart.Id,
                Items = cart.Products.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    ProductName = products[i.ProductId].Name,
                    Price = products[i.ProductId].Price,
                    ImageUrls = products[i.ProductId].Images.Select(img=>img.Url).ToList(),
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}