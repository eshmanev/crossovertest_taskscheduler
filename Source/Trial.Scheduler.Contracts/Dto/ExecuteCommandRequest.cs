using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class ExecuteCommandRequest
    {
        [DataMember(Name = "clientName")]
        public string ClientName { get; set; }

        [DataMember(Name = "commandText")]
        public string CommandText { get; set; }

        [DataMember(Name = "commandParameters")]
        public string CommandParameters { get; set; }
    }
}