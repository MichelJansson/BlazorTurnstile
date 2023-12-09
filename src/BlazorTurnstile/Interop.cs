using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorTurnstile;

internal class Interop : IAsyncDisposable
{
#if NET8_0_OR_GREATER
    private static readonly JsonNamingPolicy _kebabCaseLowerNamingPolicy = JsonNamingPolicy.KebabCaseLower;
#else
    private static readonly JsonNamingPolicy _kebabCaseLowerNamingPolicy = new JsonKebabCaseLowerNamingPolicy();
#endif

    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly IJSRuntime _js;

    public Interop(IJSRuntime jsRuntime)
    {
        _js = jsRuntime;
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorTurnstile/interop.js").AsTask());
    }

    public async ValueTask<string> RenderAsync(
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

        return await module.InvokeAsync<string>("render", component, widgetElement, jsonElement);
    }

    public ValueTask ExecuteAsync(ElementReference widgetElement) => _js.InvokeVoidAsync("turnstile.execute", widgetElement);

    public ValueTask ResetAsync(string widgetId) => _js.InvokeVoidAsync("turnstile.reset", widgetId);

    public ValueTask RemoveAsync(string widgetId) => _js.InvokeVoidAsync("turnstile.remove", widgetId);

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
