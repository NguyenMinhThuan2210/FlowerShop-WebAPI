using FlowerShop.Core.DTOs.OrderDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class CheckOutDtoValidator : AbstractValidator<CheckoutDto>
    {
        public CheckOutDtoValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^0\d{9}$").WithMessage("Số điện thoại phải có 10 kí tự và là số");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống")
                .MaximumLength(255).WithMessage("Không được quá 255 kí tự");
        }
    }
}
