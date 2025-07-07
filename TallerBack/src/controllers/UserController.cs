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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserFilterDTO filter)
        {
            var users = await _userService.GetUsersAsync(filter);
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(string id, [FromBody] UserDeactivationDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var success = await _userService.DeleteUserAsync(id, adminId, dto.Motive);
            return success ? NoContent() : NotFound();
        }
        [HttpPost("{id}/reactivate")]
        public async Task<IActionResult> Reactivate(string id)
        {
            var success = await _userService.ReactivateAsync(id);
            return success ? Ok(new { message = "Usuario reactivado." }) : NotFound();
        }
    }
}