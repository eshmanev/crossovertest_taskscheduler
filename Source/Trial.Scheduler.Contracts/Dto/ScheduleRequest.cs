using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class ScheduleRequest
    {
        [DataMember(Name = "commandId")]
        public int CommandId { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "time")]
        public string Time { get; set; }

        [DataMember(Name = "trigger")]
        public Trigger Trigger { get; set; }
    }
}