using System.Threading.Tasks;
using System.Web.Http;
using Trial.Scheduler.Admin.Service;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Services;

namespace Trial.Scheduler.Admin.Controllers
{
    [RoutePrefix("api/commands")]
    public class CommandsController : ApiController
    {
        private readonly ICommandService _commandService;
        private readonly ICommandScheduler _scheduler;

        public CommandsController(ICommandService commandService, ICommandScheduler scheduler)
        {
            _commandService = commandService;
            _scheduler = scheduler;
        }

        [HttpGet, Route("")]
        public CommandDto[] Get([FromUri] PageParamsDto request)
        {
            return _commandService.ListCommands(request);
        }

        [HttpPost, Route("")]
        public NewCommandResponse Create(NewCommandRequest request)
        {
            return _commandService.CreateCommand(request);
        }

        [HttpPost, Route("{commandId:int}/scheduled")]
        public Task Schedule([FromUri]int commandId, [FromBody]ScheduleRequest scheduler)
        {
            scheduler.CommandId = commandId;
            var request = _commandService.PrepareCommand(scheduler);
            return _scheduler.ScheduleExecutionAsync(request);
        }

        [HttpPost, Route("{commandId:int}/result")]
        public Task<ExecuteCommandResponse> Execute([FromUri]CommandIdentity identity)
        {
            var request = _commandService.PrepareCommand(identity);
            return _scheduler.ExecuteCommandAsync(request);
        }

        [HttpDelete, Route("{commandId:int}")]
        public void Delete(int commandId)
        {
            _commandService.RemoveCommand(commandId);
        }
    }
}