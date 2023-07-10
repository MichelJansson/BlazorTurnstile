using System.Text.Json.Serialization;

namespace BlazorTurnstile;

internal record TurnstileParameters(string sitekey)
{
    [JsonPropertyName("theme")]
    public TurnstileTheme? Theme { get; set; }

    [JsonPropertyName("response-field")]
    public bool? ResponseField { get; set; }

    [JsonPropertyName("response-field-name")]
    public string? ResponseFieldName { get; set; }
}
