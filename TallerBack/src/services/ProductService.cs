using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerBack.src.dtos;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.services
{
    public class ProductService : IProductService
    {
        private readonly IUnitofWork _uow;
        private readonly ICloudinaryService _cloudinary;

        public ProductService(IUnitofWork uow, ICloudinaryService cloudinary)
        {
            _uow = uow;
            _cloudinary = cloudinary;
        }

        public async Task<Guid> CreateAsync(CreateProductDTO dto)
        {
            var imageUrls = await _cloudinary.UploadImagesAsync(dto.Images);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Images = imageUrls.Select(url => new ProductImage { Url = url }).ToList()
            };


            await _uow.Products.AddAsync(product);
            await _uow.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _uow.Products.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _uow.Products.GetAllAsync(p => !p.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductDTO dto)
        {
            var product = await _uow.Products.GetByIdAsync(id);
            if (product == null || product.IsDeleted) return false;

            product.Name = dto.Name ?? product.Name;
            product.Description = dto.Description ?? product.Description;
            product.Price = dto.Price ?? product.Price;
            product.Stock = dto.Stock ?? product.Stock;
            product.Brand = dto.Brand ?? product.Brand;
            product.Category = dto.Category ?? product.Category;
            product.Condition = dto.Condition ?? product.Condition;
            product.LastModifiedAt = DateTime.UtcNow;

            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddImagesAsync(Guid productId, List<IFormFile> images)
        {
            var product = await _uow.Products.GetByIdAsync(productId);
            if (product == null || product.IsDeleted) return false;

            var urls = await _cloudinary.UploadImagesAsync(images);
            foreach (var url in urls)
            {
                product.Images.Add(new ProductImage { Url = url });
            }

            await _uow.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteImageAsync(Guid imageId)
        {
            var product = await _uow.Products
                .GetAll()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Images.Any(i => i.Id == imageId));

            if (product == null) return false;

            var image = product.Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null) return false;

            if (product.Images.Count <= 1)
                return false;

            await _cloudinary.DeleteImageAsync(image.Url);

            product.Images.Remove(image);

            await _uow.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _uow.Products.GetByIdAsync(id);
            if (product == null || product.IsDeleted) return false;

            product.IsDeleted = true;
            product.LastModifiedAt = DateTime.UtcNow;

            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationDto<ProductListDTO>> GetFilteredAsync(ProductFilterDTO filter, bool includeDeleted = false)
        {
            var query = _uow.Products.Query(includeDeleted: includeDeleted);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(filter.Brand))
                query = query.Where(p => p.Brand.ToLower() == filter.Brand.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category.ToLower() == filter.Category.ToLower());

            if (filter.Condition.HasValue)
                query = query.Where(p => p.Condition == filter.Condition.Value);

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Name)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => new ProductListDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrls = p.Images.Select(i => i.Url).ToList()
                })
                .ToListAsync();

            return new PaginationDto<ProductListDTO>
            {
                PageNumber = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = total,
                Items = items
            };
        }

    }
}