using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IOrderService
    {
        Task<Guid> CheckoutAsync(Guid userId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);
        Task<IEnumerable<OrderDTO>> GetUserOrdersDtoAsync(Guid userId);
    }
}