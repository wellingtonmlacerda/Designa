using Newtonsoft.Json; 
namespace WLDesigna.Models{ 

    public class Languages
    {
        [JsonProperty("T")]
        public T T { get; set; } = new T();
    }

}