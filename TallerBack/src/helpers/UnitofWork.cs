using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TallerBack.src.data;
using TallerBack.src.interfaces;
using TallerBack.src.models;
using TallerBack.src.repositories;

namespace TallerBack.src.helpers
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UnitofWork(ApiDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Users = new UserRepository(_userManager);
            Addresses = new AddressRepository(_context);
            Orders = new OrderRepository(_context);
            Carts = new CartRepository(_context);
            Products = new ProductRepository(_context);
            DeactivationLogs = new DeactivationLogRepository(_context);
        }
        public IUserRepository Users { get; }
        public IAddressRepository Addresses { get; }
        public IOrderRepository Orders { get; }
        public ICartRepository Carts { get; }
        public IProductRepository Products { get; }
        public IDeactivationLogRepository DeactivationLogs { get; }
        public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}