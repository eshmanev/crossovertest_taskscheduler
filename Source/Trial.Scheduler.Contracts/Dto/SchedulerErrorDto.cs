using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class SchedulerErrorDto
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}