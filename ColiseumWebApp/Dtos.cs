using System.Text.Json.Serialization;

namespace ColiseumWebApp;

public class PlayerColorDto
{
    [JsonPropertyName("ExperimentId")]
    [JsonRequired]
    public int ExperimentId { get; set; }
}