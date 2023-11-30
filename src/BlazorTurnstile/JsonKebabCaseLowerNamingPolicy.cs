#if !NET8_0_OR_GREATER
using System.Text;
using System.Text.Json;

namespace BlazorTurnstile;

/// <summary>
/// Simple naive polyfill for JsonKebabCaseLowerNamingPolicy that got shipped with NET8
/// </summary>
internal sealed class JsonKebabCaseLowerNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return ConvertNameCore(name.AsSpan());
    }

    private static string ConvertNameCore(ReadOnlySpan<char> chars)
    {
        var destination = new StringBuilder();

        for (int i = 0; i < chars.Length; i++)
        {
            char current = chars[i];

            if (i > 0 && char.IsUpper(current))
            {
                destination.Append('-');
            }

            destination.Append(char.ToLowerInvariant(current));
        }

        return destination.ToString();
    }
}
#endif
