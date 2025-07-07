using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitofWork _uow;

        public AddressService(IUnitofWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<AddressDTO>> GetUserAddressesAsync(string userId)
        {
            var addresses = await _uow.Addresses.GetByUserIdAsync(userId);
            return addresses.Select(a => new AddressDTO
            {
                Id = a.Id,
                Calle = a.Calle,
                Region= a.Region,
                Comuna = a.Comuna,
                Numero = a.Numero,
                PostalCode=a.PostalCode
            });
        }

        public async Task<AddressDTO?> GetByIdAsync(Guid id, string userId)
        {
            var addr = await _uow.Addresses.GetByIdAsync(id);
            if (addr == null || addr.UserId.ToString() != userId) return null;

            return new AddressDTO
            {
                Id = addr.Id,
                Calle = addr.Calle,
                Region= addr.Region,
                Comuna = addr.Comuna,
                Numero = addr.Numero,
                PostalCode=addr.PostalCode
            };
        }

        public async Task<Guid> CreateAsync(CreateAddressDTO dto, string userId)
        {
            var address = new Address
            {
                UserId = Guid.Parse(userId),
                Calle = dto.Calle,
                Region= dto.Region,
                Comuna = dto.Comuna,
                Numero = dto.Numero,
                PostalCode=dto.PostalCode
            };

            await _uow.Addresses.AddAsync(address);
            await _uow.SaveChangesAsync();
            return address.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            var addr = await _uow.Addresses.GetByIdAsync(id);
            if (addr == null || addr.UserId.ToString() != userId) return false;

            await _uow.Addresses.DeleteAsync(addr);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}