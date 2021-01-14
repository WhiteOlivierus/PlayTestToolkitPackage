using System.Numerics;
using System.Text.Json.Serialization;

namespace PlayTestWebUI.Models
{
    public class InputObject
    {
        [JsonPropertyName("startTime")]
        public float StartTime { get; set; }
        [JsonPropertyName("duration")]
        public float Duration { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("screenSpacePosition")]
        public Vector2 ScreenSpacePosition { get; set; }
    }
}
