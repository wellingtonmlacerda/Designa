using Newtonsoft.Json; 
using System; 
namespace WLDesigna.Pages{ 

    public class PubImage
    {
        [JsonProperty("url")]
        public required string Url { get; set; }

        [JsonProperty("modifiedDatetime")]
        public DateTime? ModifiedDatetime { get; set; }

        [JsonProperty("checksum")]
        public required string Checksum { get; set; }
    }

}