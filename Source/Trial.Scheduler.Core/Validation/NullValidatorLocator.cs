using System;
using FluentValidation;

namespace Trial.Scheduler.Core.Validation
{
    public class NullValidatorLocator : IValidatorLocator
    {
        public static readonly IValidatorLocator Instance = new NullValidatorLocator();

        public IValidator GetValidator(Type type)
        {
            var validatorType = typeof(NullValidator<>).MakeGenericType(type);
            return (IValidator)Activator.CreateInstance(validatorType);
        }

        private class NullValidator<T> : AbstractValidator<T>
        {
            
        }
    }
}