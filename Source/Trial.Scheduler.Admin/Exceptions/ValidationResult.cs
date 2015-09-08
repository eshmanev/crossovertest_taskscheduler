using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using Newtonsoft.Json;

namespace Trial.Scheduler.Admin.Exceptions
{
    internal class ValidationResult : IHttpActionResult
    {
        private readonly ValidationException _exception;

        public ValidationResult(ValidationException exception)
        {
            _exception = exception;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var errorDto = new ErrorDto
            {
                Message = "Invalid data specified",
                Details = _exception.Errors.ToDictionary(x => ToLowerCamelCase(x.PropertyName), x => x.ErrorMessage)
            };

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(JsonConvert.SerializeObject(errorDto)) };
            return Task.FromResult(response);
        }

        private string ToLowerCamelCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var firstChar = new String(value[0], 1).ToLower();
            return firstChar + value.Remove(0, 1);
        }
    }
}