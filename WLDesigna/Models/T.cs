using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace WLDesigna.Pages{ 

    public class T
    {
        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("direction")]
        public required string Direction { get; set; }

        [JsonProperty("locale")]
        public required string Locale { get; set; }

        [JsonProperty("script")]
        public required string Script { get; set; }

        [JsonProperty("PDF")]
        public List<PDF>? PDF { get; set; }

        [JsonProperty("JWPUB")]
        public List<JWPUB>? JWPUB { get; set; }

        [JsonProperty("RTF")]
        public List<RTF>? RTF { get; set; }

        [JsonProperty("BRL")]
        public List<BRL>? BRL { get; set; }

        [JsonProperty("MP3")]
        public List<MP3>? MP3 { get; set; }
    }

}