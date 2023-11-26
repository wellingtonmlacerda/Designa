using Newtonsoft.Json; 
namespace WLDesigna.Pages{ 

    public class Introduction
    {
        [JsonProperty("duration")]
        public required string Duration { get; set; }

        [JsonProperty("startTime")]
        public required string StartTime { get; set; }
    }

}