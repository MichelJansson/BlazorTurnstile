using System.Text.Json.Serialization;

namespace BlazorTurnstile;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TurnstileTheme
{
    light,
    dark,
    auto
}
