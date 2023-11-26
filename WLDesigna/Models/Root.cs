using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace WLDesigna.Pages{ 

    public class Root
    {
        [JsonProperty("pubName")]
        public required string PubName { get; set; }

        [JsonProperty("parentPubName")]
        public required string ParentPubName { get; set; }

        [JsonProperty("booknum")]
        public int? Booknum { get; set; }

        [JsonProperty("pub")]
        public required string Pub { get; set; }

        [JsonProperty("issue")]
        public required string Issue { get; set; }

        [JsonProperty("formattedDate")]
        public required string FormattedDate { get; set; }

        [JsonProperty("fileformat")]
        public List<string>? Fileformat { get; set; }

        [JsonProperty("track")]
        public int? Track { get; set; }

        [JsonProperty("specialty")]
        public required string Specialty { get; set; }

        [JsonProperty("pubImage")]
        public required PubImage PubImage { get; set; }

        [JsonProperty("languages")]
        public required Languages Languages { get; set; }

        [JsonProperty("files")]
        public required Files Files { get; set; }
    }

}