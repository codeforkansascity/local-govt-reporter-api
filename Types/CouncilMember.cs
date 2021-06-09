using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalGovtReporterAPI.Types
{
    public class CouncilMember
    {
        [JsonProperty("Jurisdiction")]
        public string Jurisdiction { get; set; }

        [JsonProperty("PersonName")]
        public string PersonName { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("SubTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }
    }
}
