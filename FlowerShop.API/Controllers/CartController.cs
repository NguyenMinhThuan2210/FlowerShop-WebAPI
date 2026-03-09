using FlowerShop.API.Validators;
using FlowerShop.Core.DTOs.CartDTOs;
using FlowerShop.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowerShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IValidator<AddToCartDto> _validator;
        public CartController(ICartService cartService, IValidator<AddToCartDto> validator)
        {
            _cartService = cartService;
            _validator = validator;
        }
        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdString, out Guid userId) ? userId : Guid.Empty;
        }
        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("Token không hợp lệ!");

            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("Token không hợp lệ!");

            var success = await _cartService.AddItemToCartAsync(userId, dto);
            if (!success) return BadRequest("Lỗi khi thêm vào giỏ hàng");

            return Ok(new { Message = "Đã ném hoa vào giỏ thành công!" });
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity([FromBody] AddToCartDto dto) 
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            var success = await _cartService.UpdateItemQuantityAsync(userId, dto.ProductId, dto.Quantity);
            if (!success) return BadRequest("Lỗi khi cập nhật giỏ hàng.");

            return Ok(new { Message = "Đã cập nhật số lượng!" });
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            var success = await _cartService.RemoveItemAsync(userId, productId);
            if (!success) return BadRequest("Lỗi khi xóa khỏi giỏ hàng.");

            return Ok(new { Message = "Đã xóa khỏi giỏ!" });
        }
    }
}
