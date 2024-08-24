using Newtonsoft.Json;
namespace Designa.Models
{
    public class TrackImage
    {
        [JsonProperty("url")]
        public required string Url { get; set; }

        [JsonProperty("modifiedDatetime")]
        public required string ModifiedDatetime { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; } = string.Empty;
    }
}