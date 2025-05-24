using API_EventManagement.Dtos.Organizers;
using FluentValidation;

namespace API_EventManagement.Validators.Organizers
{
    public class OrganizerCreateDtoValidator:AbstractValidator<OrganizerCreateDto>
    {
        public OrganizerCreateDtoValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");
        }
    }
}
