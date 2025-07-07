using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.interfaces
{
    public interface IUnitofWork : IDisposable
    {
        IUserRepository Users { get; }
        IAddressRepository Addresses { get; }
        IDeactivationLogRepository DeactivationLogs { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        Task<int> SaveChangesAsync();
    }
}