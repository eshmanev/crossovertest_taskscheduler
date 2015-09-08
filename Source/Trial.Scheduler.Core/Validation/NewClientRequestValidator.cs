using System.Linq;
using FluentValidation;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Validation
{
    public class NewClientRequestValidator : AbstractValidator<NewClientRequest>
    {
        public NewClientRequestValidator(DataModel model)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Must(x => !model.Client.Any(c => c.Name == x)).WithMessage("Client with this name already exists");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}