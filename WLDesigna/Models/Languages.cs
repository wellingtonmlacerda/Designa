using Newtonsoft.Json; 
namespace WLDesigna.Pages{ 

    public class Languages
    {
        [JsonProperty("T")]
        public required T T { get; set; }
    }

}