using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerBack.src.interfaces;

namespace TallerBack.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet("cart")]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartDtoAsync(UserId);
            return Ok(cart);
        }

        [HttpPost("cart/add")]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            await _cartService.AddOrUpdateItemAsync(UserId, productId, quantity);
            return NoContent();
        }

        [HttpDelete("cart/remove")]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            await _cartService.RemoveItemAsync(UserId, productId);
            return NoContent();
        }

        [HttpDelete("cart/clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync(UserId);
            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                var orderId = await _orderService.CheckoutAsync(UserId);
                return Ok(new { orderId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("orders")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetUserOrdersDtoAsync(UserId);
            return Ok(orders);
        }
    }
}