using System.Runtime.Serialization;

namespace Trial.Scheduler.Contracts.Dto
{
    [DataContract]
    public class PageParamsDto
    {
        [DataMember(Name = "start")]
        public int Start { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}