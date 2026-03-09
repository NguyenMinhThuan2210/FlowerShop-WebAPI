using FlowerShop.Core.DTOs.OrderDTOs;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<CheckoutDto> _validator;
        public OrdersController(IOrderService orderService, IValidator<CheckoutDto> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdString, out Guid userId) ? userId : Guid.Empty;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("Token không hợp lệ.");
            var errorMessage = await _orderService.CheckoutAsync(userId, dto);
            if (errorMessage != null)
                return BadRequest(new { Message = errorMessage }); 
            return Ok(new { Message = "Đặt hàng thành công! Đang chờ shop xác nhận." });
        }
    }
}
