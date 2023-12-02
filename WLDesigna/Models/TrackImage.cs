using Newtonsoft.Json; 
namespace WLDesigna.Models{ 

    public class TrackImage
    {
        [JsonProperty("url")]
        public required string Url { get; set; }

        [JsonProperty("modifiedDatetime")]
        public required string ModifiedDatetime { get; set; }

        [JsonProperty("checksum")]
        public int? Checksum { get; set; }
    }

}