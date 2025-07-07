using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IDeactivationLogRepository
    {
        Task AddAsync(DeactivationLog log);
        Task<IEnumerable<DeactivationLog>> GetByUserIdAsync(string userId);
        Task<IEnumerable<DeactivationLog>> GetAllAsync();
    }
}