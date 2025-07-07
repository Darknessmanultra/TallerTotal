using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerBack.src.data;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApiDbContext _context;

        public AddressRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetByUserIdAsync(string userId)
        {
            return await _context.Addresses
                .Where(a => a.UserId.ToString() == userId)
                .ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Address address)
        {
            _context.Addresses.Remove(address);
            return Task.CompletedTask;
        }
    }
}