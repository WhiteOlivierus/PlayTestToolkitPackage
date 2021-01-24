using System.Text.Json.Serialization;

namespace Data.Models
{
    public class InputKey
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("instruction")]
        public string Instruction { get; set; }
    }
}
