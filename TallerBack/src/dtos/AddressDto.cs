using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }
        public int PostalCode { get; set; }
    }
    public class CreateAddressDTO
    {
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