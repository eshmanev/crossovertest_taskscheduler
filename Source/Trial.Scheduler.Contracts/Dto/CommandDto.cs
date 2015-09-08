using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class CommandDto
    {
        [DataMember(Name = "commandId")]
        public int CommandId { get; set; }

        [DataMember(Name = "clientId")]
        public int ClientId { get; set; }

        [DataMember(Name = "clientName")]
        public string ClientName { get; set; }

        [DataMember(Name = "clientAddress")]
        public string ClientAddress { get; set; }

        [DataMember(Name = "commandText")]
        public string CommandText { get; set; }

        [DataMember(Name = "commandParameters", IsRequired = false, EmitDefaultValue = true)]
        public string CommandParameters { get; set; }
    }
}