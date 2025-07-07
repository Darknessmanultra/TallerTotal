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
    public class CartRepository : ICartRepository
    {
        private readonly ApiDbContext _context;

        public CartRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task RemoveAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}