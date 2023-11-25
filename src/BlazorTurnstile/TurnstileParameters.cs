using System.Text.Json.Serialization;

namespace BlazorTurnstile;

internal record TurnstileParameters(string sitekey)
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("appearance")]
    public TurnstileAppearance? Appearance { get; set; }

    [JsonPropertyName("theme")]
    public TurnstileTheme? Theme { get; set; }

    [JsonPropertyName("size")]
    public TurnstileSize? Size { get; set; }

    [JsonPropertyName("response-field")]
    public bool? ResponseField { get; set; }

    [JsonPropertyName("response-field-name")]
    public string? ResponseFieldName { get; set; }
}
