using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBack.src.dtos;
using TallerBack.src.models;

namespace TallerBack.src.interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(CreateProductDTO dto);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateProductDTO dto);
        Task<bool> AddImagesAsync(Guid productId, List<IFormFile> images);
        Task<bool> DeleteImageAsync(Guid imageId);
        Task<bool> DeleteAsync(Guid id);
        Task<PaginationDto<ProductListDTO>> GetFilteredAsync(ProductFilterDTO filter, bool includeDeleted = false);

    }
}