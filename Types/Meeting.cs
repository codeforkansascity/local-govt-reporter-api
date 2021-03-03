using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalGovtReporterAPI.Types
{
    public class Meeting
    {
        [JsonProperty("SourceURL")]
        public string SourceURL { get; set; }

        [JsonProperty("MeetingID")]
        public string MeetingID { get; set; }

        [JsonProperty("MeetingType")]
        public string MeetingType { get; set; }

        [JsonProperty("MeetingDate")]
        public string MeetingDate { get; set; }

        [JsonProperty("MeetingTime")]
        public string MeetingTime { get; set; }

        [JsonProperty("MeetingLocation")]
        public string MeetingLocation { get; set; }

        [JsonProperty("MeetingAddress")]
        public string MeetingAddress { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Jurisdiction")]
        public string Jurisdiction { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("County")]
        public string County { get; set; }

        [JsonProperty("AgendaURL")]
        public string AgendaURL { get; set; }

        [JsonProperty("PacketURL")]
        public string PacketURL { get; set; }

        [JsonProperty("MinutesURL")]
        public string MinutesURL { get; set; }

        [JsonProperty("VideoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("Tags")]
        public List<string> Tags { get; set; }   
        
    }
}
