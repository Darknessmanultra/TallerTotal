using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}