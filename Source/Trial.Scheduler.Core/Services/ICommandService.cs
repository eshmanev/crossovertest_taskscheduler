using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Core.Services
{
    public interface ICommandService
    {
        ExecuteCommandRequest PrepareCommand(CommandIdentity identity);

        ScheduleCommandRequest PrepareCommand(ScheduleRequest identity);

        CommandDto[] ListCommands(PageParamsDto request);

        NewCommandResponse CreateCommand(NewCommandRequest request);

        void RemoveCommand(int commandId);
    }
}