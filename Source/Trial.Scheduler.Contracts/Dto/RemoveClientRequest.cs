using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class RemoveClientRequest
    {
        [DataMember]
        public int ClientId { get; set; } 
    }
}