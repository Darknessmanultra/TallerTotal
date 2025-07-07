using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.data;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(ApplicationUser user);
        Task<bool> DeleteAsync(ApplicationUser user);
    }
}