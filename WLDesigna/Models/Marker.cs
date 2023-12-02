using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace WLDesigna.Models{ 

    public class Marker
    {
        [JsonProperty("duration")]
        public required string Duration { get; set; }

        [JsonProperty("startTime")]
        public required string StartTime { get; set; }

        [JsonProperty("mepsParagraphId")]
        public int? MepsParagraphId { get; set; }

        [JsonProperty("mepsLanguageSpoken")]
        public required string MepsLanguageSpoken { get; set; }

        [JsonProperty("mepsLanguageWritten")]
        public required string MepsLanguageWritten { get; set; }

        [JsonProperty("documentId")]
        public int? DocumentId { get; set; }

        [JsonProperty("markers")]
        public required List<Marker> Markers { get; set; }

        [JsonProperty("type")]
        public required string Type { get; set; }

        [JsonProperty("hash")]
        public required string Hash { get; set; }

        [JsonProperty("introduction")]
        public required Introduction Introduction { get; set; }
    }

}