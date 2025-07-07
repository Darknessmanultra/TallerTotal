using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerBack.src.data;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiDbContext _context;

        public ProductRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var query = _context.Products.AsQueryable();
            if (!includeDeleted)
                query = query.Where(p => !p.IsDeleted);

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>>? filter = null, bool includeDeleted = false)
        {
            var query = _context.Products.AsQueryable();

            if (filter != null)
                query = query.Where(filter);
            
            if (!includeDeleted)
                query = query.Where(p => !p.IsDeleted);

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.AsQueryable();
        }

        public async Task AddAsync(Product product) =>
            await _context.Products.AddAsync(product);

        public void Remove(Product product) =>
            _context.Products.Remove(product);

        public IQueryable<Product> Query(Expression<Func<Product, bool>>? filter = null, bool includeDeleted = false)
        {
            var query = _context.Products.AsQueryable();
            if (!includeDeleted)
                query = query.Where(p => !p.IsDeleted);

            if (filter != null)
                query = query.Where(filter);
            return query;
        }
    }
}