using FluentValidation;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {

        public UpdateTaskCommandValidator() 
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .Must(status => status == "Pending" || status == "InProgress" || status == "Completed")
                .WithMessage("Status must be either 'Pending', 'InProgress', or 'Completed'.");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .WithMessage("Due date is required.")
                .GreaterThan(DateTime.Now)
                .WithMessage("Due date must be in the future.");

        }
    }
}
