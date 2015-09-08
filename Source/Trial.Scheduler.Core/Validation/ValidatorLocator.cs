using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Trial.Scheduler.Core.Validation
{
    public class ValidatorLocator : IValidatorLocator
    {
        private readonly ConcurrentDictionary<Type, IValidator> _cache = new ConcurrentDictionary<Type, IValidator>();
        private readonly IEnumerable<IValidator> _validators;

        public ValidatorLocator(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public IValidator GetValidator(Type type)
        {
            return _cache.GetOrAdd(type, CreateValidator);
        }

        private IValidator CreateValidator(Type type)
        {
            var validatorType = typeof(AbstractValidator<>).MakeGenericType(type);
            return _validators.SingleOrDefault(x => validatorType.IsAssignableFrom(x.GetType()));
        }
    }
}