using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlazorTurnstile;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TurnstileAppearance {
    always,
    execute,
    [EnumMember(Value = "interaction-only")]
    interaction_only
}