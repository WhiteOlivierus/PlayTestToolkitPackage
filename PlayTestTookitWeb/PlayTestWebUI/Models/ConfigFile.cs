using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlayTestWebUI.Models
{
    public class ConfigFile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("buildId")]
        public string BuildId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; }

        [JsonPropertyName("researchQuestion")]
        public string ResearchQuestion { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("tutorialDescription")]
        public string TutorialDescription { get; set; }
        [JsonPropertyName("input")]
        public IList<InputKey> Input { get; set; }
        [JsonPropertyName("googleForm")]
        public string GoogleForm { get; set; }
    }
}
