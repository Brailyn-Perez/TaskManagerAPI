using FluentValidation;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(250);

            RuleFor(x => x.IsCompleted)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Status)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .NotNull()
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");
        }
    }
}
