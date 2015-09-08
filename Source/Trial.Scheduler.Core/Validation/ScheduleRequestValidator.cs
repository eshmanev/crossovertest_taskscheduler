using System;
using System.Linq;
using FluentValidation;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Validation
{
    public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
    {
        public ScheduleRequestValidator(DataModel model)
        {
            RuleFor(x => x.CommandId)
                .Must(x => model.Command.Any(c => c.Id == x))
                .WithMessage("Command cannot be found or has been deleted");

            RuleFor(x => x.Date).Must(ValidDate).WithMessage("Invalid date");
            RuleFor(x => x.Time).Must(ValidTime).WithMessage("Invalid time");
        }

        private bool ValidDate(string date)
        {
            DateTime temp;
            return DateTime.TryParse(date, out temp);
        }

        private bool ValidTime(string time)
        {
            DateTime temp;
            return DateTime.TryParse(time, out temp);
        }
    }
}