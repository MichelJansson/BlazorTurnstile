using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorTurnstile;

internal class Interop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

#if NET8_0_OR_GREATER
    private static readonly JsonNamingPolicy _kebabCaseLowerNamingPolicy = JsonNamingPolicy.KebabCaseLower;
#else
    private static readonly JsonNamingPolicy _kebabCaseLowerNamingPolicy = new JsonKebabCaseLowerNamingPolicy();
#endif

    public Interop(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorTurnstile/interop.js").AsTask());
    }

    public async ValueTask RenderAsync(
        DotNetObjectReference<Turnstile> component,
        ElementReference widgetElement,
        TurnstileParameters parameters)
    {
        var module = await _moduleTask.Value;

        // Explicit JSON serialization to allow custom options
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = _kebabCaseLowerNamingPolicy,
            Converters = { new JsonStringEnumConverter(namingPolicy: _kebabCaseLowerNamingPolicy) }
        };
        var jsonElement = JsonSerializer.SerializeToElement(parameters, options);

        await module.InvokeVoidAsync("render", component, widgetElement, jsonElement);
    }

    public async ValueTask ResetAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("reset");
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
