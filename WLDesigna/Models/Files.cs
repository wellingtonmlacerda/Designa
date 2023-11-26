using Newtonsoft.Json; 
namespace WLDesigna.Pages{ 

    public class Files
    {
        [JsonProperty("T")]
        public required T T { get; set; }
    }

}