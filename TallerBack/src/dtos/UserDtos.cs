using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNum { get; set; }
        public DateOnly BirthDate { get; set; }
    }
    public class UserListDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }
    public class UserFilterDTO
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class UserDeactivationDto
    {
        [Required]
        public string Motive { get; set; }
    }
}