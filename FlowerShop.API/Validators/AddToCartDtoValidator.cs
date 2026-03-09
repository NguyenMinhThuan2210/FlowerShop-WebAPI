using FlowerShop.Core.DTOs.CartDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class AddToCartDtoValidator : AbstractValidator<AddToCartDto>
    {
        public AddToCartDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");
        }
    }
}
