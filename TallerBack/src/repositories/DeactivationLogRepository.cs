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
    public class DeactivationLogRepository : IDeactivationLogRepository
    {
        private readonly ApiDbContext _context;

        public DeactivationLogRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DeactivationLog log)
        {
            await _context.DeactivationLogs.AddAsync(log);
        }

        public async Task<IEnumerable<DeactivationLog>> GetByUserIdAsync(string userId)
        {
            return await _context.DeactivationLogs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<DeactivationLog>> GetAllAsync()
        {
            return await _context.DeactivationLogs
                .OrderByDescending(l => l.Date)
                .ToListAsync();
        }
    }
}