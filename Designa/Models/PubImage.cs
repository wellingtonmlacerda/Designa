using Newtonsoft.Json;
namespace Designa.Models
{
    public class PubImage
    {
        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        [JsonProperty("modifiedDatetime")]
        public DateTime? ModifiedDatetime { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; } = string.Empty;
    }
}