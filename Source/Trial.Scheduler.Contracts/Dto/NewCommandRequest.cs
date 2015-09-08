using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class NewCommandRequest
    {
        [DataMember(Name = "clientId")]
        public int ClientId { get; set; }

        [DataMember(Name = "commandText")]
        public string CommandText { get; set; }

        [DataMember(Name = "commandParameters", IsRequired = false, EmitDefaultValue = true)]
        public string CommandParameters { get; set; }
    }
}