using FluentValidation;
using System;

namespace Trial.Scheduler.Core.Services
{
    public static class ServiceExtensions
    {
        public static void Handle<TRequest>(this ServiceBase service, TRequest request, Action<TRequest> handler)
        {
            HandleInternal(service, request, x =>
            {
                handler(x);
                return (object)null;
            });
        }

        public static TResponse HandleAndReturn<TRequest, TResponse>(this ServiceBase service, TRequest request, Func<TRequest, TResponse> handler)
        {
            return HandleInternal(service, request, handler);
        }

        private static TResponse HandleInternal<TRequest, TResponse>(this ServiceBase service, TRequest request, Func<TRequest, TResponse> handler)
        {
            try
            {
                var validationResult = service.Validate(request);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return handler(request);
            }
            catch (Exception ex)
            {
                // todo: Log the exception here

                throw;
            }
        }
    }
}