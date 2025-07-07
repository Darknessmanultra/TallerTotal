using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;

namespace TallerBack.src.interfaces
{
    public interface IUserService
    {
        Task<PaginationDto<UserListDTO>> GetUsersAsync(UserFilterDTO filter);
        Task<UserListDTO?> GetUserByIdAsync(string id);
        Task<bool> DeleteUserAsync(string id,string adminId, string motive);
        Task<bool> ReactivateAsync(string id);
    }
}