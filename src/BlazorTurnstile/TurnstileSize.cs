using System.Text.Json.Serialization;

namespace BlazorTurnstile;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TurnstileSize
{
    normal,
    compact,
}
