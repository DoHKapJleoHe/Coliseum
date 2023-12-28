using System.Text.Json.Serialization;
using CardLib;

namespace ColiseumWebApp;

public class CardDto
{
    [JsonPropertyName("Number")]
    [JsonRequired]
    public int Number { get; set; }
    
    [JsonPropertyName("Color")]
    [JsonRequired]
    public Color Color { get; set; }
    
    public CardDto() { }
    
    public CardDto(Card card)
    {
        Number = card.Number;
        Color = card.Color;
    }
}