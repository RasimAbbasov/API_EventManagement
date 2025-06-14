using API_EventManagement.Dtos.Users;
using FluentValidation;

namespace API_EventManagement.Validators.Users
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() 
        {
            RuleFor(x => x.UserName)
             .NotEmpty().WithMessage("Username is required.")
             .MinimumLength(8).WithMessage("Username must be at least 8 characters long.")
             .MaximumLength(20).WithMessage("Username must not exceed 20 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
