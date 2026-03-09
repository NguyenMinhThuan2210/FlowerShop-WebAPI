using FlowerShop.Core.DTOs.ProductDTOs;
using FlowerShop.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;
        public ProductsController(IProductService productService, IValidator<CreateProductDto> createValidator, IValidator<UpdateProductDto> updateValidator)
        {
            _productService = productService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            var product = await _productService.GetProductsAsync(parameters);
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Sản phẩm không tồn tại!");
            return Ok(product);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var newProduct = await _productService.CreateProductAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct!.Id }, newProduct);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var sucess = await _productService.UpdateProductAsync(id, updateDto);
            if (!sucess) return NotFound("Không tìm thấy sản phẩm để sửa");
            return NoContent();
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success) return NotFound("Không tìm thấy sản phẩm để xóa");
            return NoContent();
        }
    }
}
