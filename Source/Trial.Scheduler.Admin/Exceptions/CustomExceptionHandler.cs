using System.ServiceModel;
using System.Web.Http.ExceptionHandling;
using FluentValidation;

namespace Trial.Scheduler.Admin.Exceptions
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is EndpointNotFoundException)
                HandleEndpointNotFoundException(context);
            else if (context.Exception is ValidationException)
                HandleValidationException(context);
            else
                HandleUnexpected(context);
        }

        private void HandleUnexpected(ExceptionHandlerContext context)
        {
            context.Result = new ExceptionResult(context.Exception);
        }

        private void HandleEndpointNotFoundException(ExceptionHandlerContext context)
        {
            context.Result = new ExceptionResult(context.Exception);
        }

        private void HandleValidationException(ExceptionHandlerContext context)
        {
            context.Result = new ValidationResult((ValidationException)context.Exception);
        }
    }
}