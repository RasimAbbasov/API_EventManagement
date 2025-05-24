using API_EventManagement.Dtos.Events;
using FluentValidation;

namespace API_EventManagement.Validators.Events
{
    public class EventCreateDtoValidator:AbstractValidator<EventCreateDto>
    {
        public EventCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.Date)
                .GreaterThan(DateTime.Now).WithMessage("Date must be in the future.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");
            RuleFor(x => x.OrganizerId)
                .GreaterThan(0).WithMessage("Organizer ID must be a positive integer.");
        }
    }
}
