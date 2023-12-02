using Newtonsoft.Json; 
namespace WLDesigna.Models{ 

    public class Files
    {
        [JsonProperty("T")]
        public T T { get; set; } = new T();
    }

}