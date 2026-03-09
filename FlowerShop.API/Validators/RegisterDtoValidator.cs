using FluentValidation;
using FlowerShop.Core.DTOs.AuthDTOs;
namespace FlowerShop.API.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .Length(3,50).WithMessage("Tên đăng nhập phải từ 3 đến 50 ký tự.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 kí tự");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ và tên không được để trống!");
        }
    }
}
