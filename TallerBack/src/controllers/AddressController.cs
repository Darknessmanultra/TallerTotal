using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerBack.src.dtos;
using TallerBack.src.interfaces;

namespace TallerBack.src.controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            var addresses = await _addressService.GetUserAddressesAsync(UserId);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var address = await _addressService.GetByIdAsync(id, UserId);
            return address == null ? NotFound() : Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressDTO dto)
        {
            var id = await _addressService.CreateAsync(dto, UserId);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _addressService.DeleteAsync(id, UserId);
            return result ? NoContent() : NotFound();
        }
    }
}