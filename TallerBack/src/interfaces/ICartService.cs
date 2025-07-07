using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(Guid userId);
        Task AddOrUpdateItemAsync(Guid userId, Guid productId, int quantity);
        Task RemoveItemAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
        Task<CartDTO> GetCartDtoAsync(Guid userId);
    }
}