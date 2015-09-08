using FluentValidation;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Core.Validation
{
    public class ExecuteCommandRequestValidator : AbstractValidator<ExecuteCommandRequest>
    {
        public ExecuteCommandRequestValidator()
        {
            RuleFor(x => x.ClientName).NotEmpty().WithMessage("Client name is required");
            RuleFor(x => x.CommandText).NotEmpty().WithMessage("Command text is required");
        }
    }
}