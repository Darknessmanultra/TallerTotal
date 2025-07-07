using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TallerBack.src.models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Phone]
        public int PhoneNum { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }
        public bool Active { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }

    public class DeactivationLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserId { get; set; }

        [Required]
        public string AdminId { get; set; }

        [Required]
        public string Motive { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}