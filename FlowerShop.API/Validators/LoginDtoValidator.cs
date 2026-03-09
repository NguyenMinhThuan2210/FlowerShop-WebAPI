using FlowerShop.Core.DTOs.AuthDTOs;
using FluentValidation;

namespace FlowerShop.API.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
