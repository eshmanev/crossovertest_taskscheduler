using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public enum Trigger
    {
        [EnumMember]
        Single,

        [EnumMember]
        Daily
    }
}