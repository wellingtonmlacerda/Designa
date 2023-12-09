using Newtonsoft.Json;
namespace Designa.Models
{
    public class Files
    {
        [JsonProperty("T")]
        public T T { get; set; } = new T();
    }
}