using FlowerShop.Core.DTOs.CategoryDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Không được dài quá 100 kí tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Không được dài quá 500 kí tự");
        }
    }
}
