using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorTurnstile;

internal class Interop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public Interop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorTurnstile/interop.js").AsTask());
    }

    public async ValueTask RenderAsync(
        DotNetObjectReference<Turnstile> component,
        ElementReference widgetElement,
        TurnstileParameters parameters)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("render", component, widgetElement, parameters);
    }

    public async ValueTask ResetAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("reset");
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}