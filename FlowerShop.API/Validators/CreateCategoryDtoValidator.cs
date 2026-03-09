using FlowerShop.Core.DTOs.CategoryDTOs;
using FlowerShop.Core.DTOs.ProductDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Không được dài quá 100 kí tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Không được dài quá 500 kí tự");
        }
    }
}
