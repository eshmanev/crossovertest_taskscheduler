using System.Linq;
using FluentValidation;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Validation
{
    public class CommandIdentityValidator : AbstractValidator<CommandIdentity>
    {
        public CommandIdentityValidator(DataModel model)
        {
            RuleFor(x => x.CommandId)
                .Must(x => model.Command.Any(c => c.Id == x))
                .WithMessage("Command cannot be found or has been deleted");
        }
    }
}