using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Trial.Scheduler.Admin.Exceptions
{
    internal class ExceptionResult : IHttpActionResult
    {
        private readonly Exception _exception;

        public ExceptionResult(Exception exception)
        {
            _exception = exception;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var errorDto = new ErrorDto {Message = _exception.Message};
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError) {Content = new StringContent(JsonConvert.SerializeObject(errorDto))};
            return Task.FromResult(response);
        }
    }
}