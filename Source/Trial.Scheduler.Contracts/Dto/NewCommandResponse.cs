using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class NewCommandResponse
    {
        [DataMember(Name = "commandId")]
        public int CommandId { get; set; }
    }
}