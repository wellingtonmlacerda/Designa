using Newtonsoft.Json;
namespace Designa.Models
{

    public class Languages
    {
        [JsonProperty("T")]
        public T T { get; set; } = new T();
    }

}