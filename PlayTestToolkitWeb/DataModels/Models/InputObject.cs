using System.Text.Json.Serialization;

namespace Data.Models
{
    public class InputObject
    {
        [JsonPropertyName("startTime")]
        public float StartTime { get; set; }
        [JsonPropertyName("duration")]
        public float Duration { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
