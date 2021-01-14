using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlayTestWebUI.Models
{
    public class DataFile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("configId")]
        public string ConfigId { get; set; }

        [JsonPropertyName("startTime")]
        public double StartTime { get; set; }

        [JsonPropertyName("input")]
        public IList<InputObject> Input { get; set; } = new List<InputObject>();
    }
}
