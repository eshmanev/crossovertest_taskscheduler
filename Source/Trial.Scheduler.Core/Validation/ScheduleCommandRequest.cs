using System;
using FluentValidation;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Core.Validation
{
    public class ScheduleCommandRequestValidator : AbstractValidator<ScheduleCommandRequest>
    {
        public ScheduleCommandRequestValidator()
        {
            RuleFor(x => x.FirstDateTime).GreaterThan(DateTime.Now);
            RuleFor(x => x.Command).SetValidator(new ExecuteCommandRequestValidator());
        }
    }
}