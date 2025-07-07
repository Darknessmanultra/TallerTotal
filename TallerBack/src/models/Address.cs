using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Comuna { get; set; }
        [Required]
        public int PostalCode { get; set; }
    }
}