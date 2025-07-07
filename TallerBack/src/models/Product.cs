using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public ProductCondition Condition { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public string? CloudinaryPublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
    public enum ProductCondition
    {
        New = 1,
        Used = 2
    }
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Url { get; set; }

        public int Order { get; set; }

        public Product Product { get; set; }
    }
}