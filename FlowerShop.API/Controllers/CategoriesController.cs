using FlowerShop.Core.DTOs.CategoryDTOs;
using FlowerShop.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;
        public CategoriesController(ICategoryService categoryService, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Danh mục này không tồn tại");
            }
            return Ok(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var newCategory = await _categoryService.CreateCategoryAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Messages = errors });
            }

            var success = await _categoryService.UpdateCategoryAsync(id, updateDto);
            if(!success)
            {
                return NotFound("Không tìm thấy danh mục để sửa");
            }    
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound("Không tìm thấy danh mục để xóa");
            }
            return NoContent();
        }
    }
}
