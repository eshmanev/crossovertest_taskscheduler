using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trial.Scheduler.Admin.Exceptions
{
    internal class ErrorDto
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "details")]
        public Dictionary<string, string> Details { get; set; }
    }
}