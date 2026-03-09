using FlowerShop.Core.DTOs.ProductDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá phải lớn hơn 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Không được dài quá 100 kí tự");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng tồn phải lớn hơn hoặc bằng 0");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Không được dài quá 500 kí tự");
        }
    }
}
