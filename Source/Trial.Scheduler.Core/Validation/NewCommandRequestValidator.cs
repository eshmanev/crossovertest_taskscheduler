using System.Linq;
using FluentValidation;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Validation
{
    public class NewCommandRequestValidator : AbstractValidator<NewCommandRequest>
    {
        public NewCommandRequestValidator(DataModel model)
        {
            RuleFor(x => x.ClientId)
                .Must(x => model.Client.Any(c => c.Id == x)).WithMessage("Client cannot be found or has been deleted");

            RuleFor(x => x.CommandText)
                .NotEmpty().WithMessage("Command text is required");
        }
    }
}