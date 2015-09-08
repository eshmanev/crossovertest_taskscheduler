using System.Collections.Generic;
using System.Web.Http;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Services;

namespace Trial.Scheduler.Admin.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet, Route("")]
        public IEnumerable<ClientDto> Get([FromUri]PageParamsDto request)
        {
            return _clientService.ListClients(request);
        }

        //[HttpGet, Route("{id:int}")]
        //public string Get(int id)
        //{
        //    return "abc" ;
        //}

        [HttpPost, Route("")]
        public NewClientResponse Create(NewClientRequest request)
        {
            return _clientService.AddClient(request);
        }
    }
}