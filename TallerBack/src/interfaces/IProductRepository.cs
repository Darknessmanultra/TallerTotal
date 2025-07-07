using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>>? filter = null, bool includeDeleted = false);
        IQueryable<Product> GetAll();
        Task AddAsync(Product product);
        void Remove(Product product);
        IQueryable<Product> Query(Expression<Func<Product, bool>>? filter = null, bool includeDeleted = false);
    }
}