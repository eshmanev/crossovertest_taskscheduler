using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class NewClientResponse
    {
        [DataMember(Name = "clientId")]
        public int ClientId { get; set; }
    }
}