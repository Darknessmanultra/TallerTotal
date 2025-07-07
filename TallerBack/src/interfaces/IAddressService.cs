using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;

namespace TallerBack.src.interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDTO>> GetUserAddressesAsync(string userId);
        Task<AddressDTO?> GetByIdAsync(Guid id, string userId);
        Task<Guid> CreateAsync(CreateAddressDTO dto, string userId);
        Task<bool> DeleteAsync(Guid id, string userId);
    }
}