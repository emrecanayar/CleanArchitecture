using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration
{
    public class ErrorMessage
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_messages")]
        public string[] ErrorMessages { get; set; }

        [JsonProperty("correlation_id")]
        public string CorrelationId { get; set; }

    }
}
