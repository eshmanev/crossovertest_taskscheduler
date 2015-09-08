using System;
using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class ScheduleCommandRequest
    {
        [DataMember(Name = "trigger")]
        public Trigger Trigger { get; set; }

        [DataMember(Name = "firstDateTime")]
        public DateTime FirstDateTime { get; set; }

        [DataMember(Name = "command")]
        public ExecuteCommandRequest Command { get; set; }
    }
}