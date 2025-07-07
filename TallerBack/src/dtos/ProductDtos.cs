using System.ComponentModel.DataAnnotations;
using TallerBack.src.models;

namespace TallerBack.src.dtos
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public ProductCondition Condition { get; set; }
    }

    public class UpdateProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public ProductCondition? Condition { get; set; }
    }

    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public int Price { get; set; }
        public int Stock { get; set; }
    }
    public class ProductFilterDTO
    {
        public string? Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public ProductCondition? Condition { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
    public class AddProductImagesDto
    {
        [Required]
        public List<IFormFile> Images { get; set; } = new();
    }
}