using System;
using FluentValidation;

namespace Trial.Scheduler.Core.Validation
{
    public interface IValidatorLocator
    {
        IValidator GetValidator(Type type);
    }
}