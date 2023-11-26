using Newtonsoft.Json; 
namespace WLDesigna.Pages{ 

    public class File
    {
        [JsonProperty("url")]
        public required string Url { get; set; }

        [JsonProperty("stream")]
        public required string Stream { get; set; }

        [JsonProperty("modifiedDatetime")]
        public required string ModifiedDatetime { get; set; }

        [JsonProperty("checksum")]
        public required string Checksum { get; set; }
    }

}