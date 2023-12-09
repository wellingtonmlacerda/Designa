using Newtonsoft.Json;
namespace Designa.Models
{
    public class Introduction
    {
        [JsonProperty("duration")]
        public required string Duration { get; set; }

        [JsonProperty("startTime")]
        public required string StartTime { get; set; }
    }
}