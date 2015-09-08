using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class ExecuteCommandResponse
    {
        [DataMember(Name = "commandOutput")]
        public string CommandOutput { get; set; }
    }
}