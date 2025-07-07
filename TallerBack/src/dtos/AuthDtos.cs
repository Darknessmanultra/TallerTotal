using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public int PhoneNum { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[0-9]).{6,}$", ErrorMessage = "Password must be at least 6 characters and contain at least one digit.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}