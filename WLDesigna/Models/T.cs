using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace WLDesigna.Models{ 

    public class T
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("direction")]
        public string Direction { get; set; } = string.Empty;

        [JsonProperty("locale")]
        public string Locale { get; set; } = string.Empty;

        [JsonProperty("script")]
        public string Script { get; set; } = string.Empty;

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