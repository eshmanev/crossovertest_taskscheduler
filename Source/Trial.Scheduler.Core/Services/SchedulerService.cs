using FluentValidation.Results;
using Trial.Scheduler.Core.Validation;

namespace Trial.Scheduler.Core.Services
{
    public class ServiceBase
    {
        public IValidatorLocator ValidatorLocator { get; set; }

        public ValidationResult Validate<T>(T instance)
        {
            var validator = ValidatorLocator.GetValidator(typeof(T));
            return validator != null ? validator.Validate(instance) : new ValidationResult();
        }
    }
}