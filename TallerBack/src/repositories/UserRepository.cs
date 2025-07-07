using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TallerBack.src.data;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<ApplicationUser?> GetByIdAsync(Guid id) =>
        await _userManager.FindByIdAsync(id.ToString());

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
            await Task.FromResult(_userManager.Users.ToList());

        public async Task<ApplicationUser?> GetByEmailAsync(string email) =>
            await _userManager.FindByEmailAsync(email);

        public async Task<bool> UpdateAsync(ApplicationUser user) =>
            (await _userManager.UpdateAsync(user)).Succeeded;

        public async Task<bool> DeleteAsync(ApplicationUser user) =>
            (await _userManager.DeleteAsync(user)).Succeeded;
    }
}