using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TallerBack.src.dtos;
using TallerBack.src.helpers;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.services
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager, IUnitofWork unitofWork)
        {
            _userManager = userManager;
            _unitofWork = unitofWork;
        }
        public async Task<PaginationDto<UserListDTO>> GetUsersAsync(UserFilterDTO filter)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(term) || u.UserName.ToLower().Contains(term));
            }
            var total = await query.CountAsync();

            var users = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var userDtos = new List<UserListDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserListDTO
                {
                    Id = user.Id.ToString(),
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    BirthDate = user.BirthDate,
                    Roles = roles.ToList()
                });
            }

            return new PaginationDto<UserListDTO>
            {
                PageNumber = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = total,
                Items = userDtos
            };
        }

        public async Task<UserListDTO?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserListDTO
            {
                Id = user.Id.ToString(),
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                BirthDate = user.BirthDate,
                Roles = roles.ToList()
            };
        }

        public async Task<bool> DeleteUserAsync(string id,string adminId, string motive)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _unitofWork.DeactivationLogs.AddAsync(new DeactivationLog
            {
                UserId = id,
                AdminId = adminId,
                Motive = motive
            });
            await _unitofWork.SaveChangesAsync();

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> ReactivateAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.LockoutEnd = null;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        
    }
}