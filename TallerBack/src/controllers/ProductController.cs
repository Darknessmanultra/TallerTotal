using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerBack.src.dtos;
using TallerBack.src.interfaces;
using TallerBack.src.models;

namespace TallerBack.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("filtered")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiltered([FromQuery] ProductFilterDTO filter)
        {
            var result = await _service.GetFilteredAsync(filter);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPost("{id}/images")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddImages(Guid id, [FromForm] AddProductImagesDto dto)
        {
            var success = await _service.AddImagesAsync(id, dto.Images);
            return success ? Ok(new { message = "Images added." }) : NotFound();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAttributes(Guid id, [FromBody] UpdateProductDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("images/{imageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImage(Guid imageId)
        {
            var deleted = await _service.DeleteImageAsync(imageId);
            return deleted ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminFiltered([FromQuery] ProductFilterDTO filter)
        {
            var result = await _service.GetFilteredAsync(filter, includeDeleted: true);
            return Ok(result);
        }
    }
}