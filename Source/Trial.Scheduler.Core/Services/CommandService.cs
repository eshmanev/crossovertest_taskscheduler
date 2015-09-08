using System;
using System.Globalization;
using System.Linq;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Services
{
    public class CommandService : ServiceBase, ICommandService
    {
        // normally it should be replaced with pair of interfaces (Repository/UnitOfWork) for testability purposes
        private readonly DataModel _model;

        public CommandService(DataModel model)
        {
            _model = model;
        }

        public ExecuteCommandRequest PrepareCommand(CommandIdentity identity)
        {
            return this.HandleAndReturn(identity, PrepareCommandCore);
        }

        public ScheduleCommandRequest PrepareCommand(ScheduleRequest identity)
        {
            return this.HandleAndReturn(identity, PrepareCommandCore);
        }

        private ScheduleCommandRequest PrepareCommandCore(ScheduleRequest request)
        {
            var command = _model.Command.Find(request.CommandId);
            var date = DateTime.Parse(request.Date, CultureInfo.InvariantCulture).ToLocalTime().Date;
            var time = DateTime.Parse(request.Time, CultureInfo.InvariantCulture).ToLocalTime().TimeOfDay;
            var dateTime = date.AddMinutes(new TimeSpan(time.Hours, time.Minutes, 0).TotalMinutes);
            return new ScheduleCommandRequest
            {
                Trigger = request.Trigger,
                FirstDateTime = dateTime,
                Command = new ExecuteCommandRequest
                {
                    ClientName = command.Client.Name,
                    CommandText = command.CommandText,
                    CommandParameters = command.Parameters
                }
            };
        }

        private ExecuteCommandRequest PrepareCommandCore(CommandIdentity identity)
        {
            var command = _model.Command.Find(identity.CommandId);
            return new ExecuteCommandRequest {ClientName = command.Client.Name, CommandText = command.CommandText, CommandParameters = command.Parameters};
        }

        public CommandDto[] ListCommands(PageParamsDto request)
        {
            return this.HandleAndReturn(request, ListCommandsCore);
        }

        private CommandDto[] ListCommandsCore(PageParamsDto request)
        {
            return _model.Command
                .OrderBy(x => x.Id)
                .Skip(request.Start)
                .Take(request.Count)
                .Select(x => new CommandDto
                {
                    CommandId = x.Id,
                    ClientId = x.ClientId,
                    ClientName = x.Client.Name,
                    ClientAddress = x.Client.Address,
                    CommandText = x.CommandText,
                    CommandParameters = x.Parameters
                })
                .ToArray();
        }

        public NewCommandResponse CreateCommand(NewCommandRequest request)
        {
            return this.HandleAndReturn(request, CreateCommandCore);
        }

        private NewCommandResponse CreateCommandCore(NewCommandRequest request)
        {
            var entity = new Command
            {
                ClientId = request.ClientId,
                CommandText = request.CommandText,
                Parameters = request.CommandParameters
            };
            _model.Command.Add(entity);
            _model.SaveChanges();
            return new NewCommandResponse {CommandId = entity.Id};
        }

        public void RemoveCommand(int commandId)
        {
            throw new System.NotImplementedException();
        }
    }
}