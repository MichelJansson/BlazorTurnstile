# BlazorTurnstile
A Blazor library for [Cloudflare's Turnstile](https://developers.cloudflare.com/turnstile).

## Installing

To install the package add the following line to you csproj file replacing x.x.x with the latest version number (found at the top of this file):

```
<PackageReference Include="BlazorTurnstile" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package BlazorTurnstile
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

### Setup
Blazor Server applications will need to include the following JS file in their `_Host.cshtml` .

Blazor Client applications will need to include the following JS file in their `Index.html` .

Add the JS script at the bottom of the page using the following script tag.

```html
<script src="https://challenges.cloudflare.com/turnstile/v0/api.js?render=explicit"></script>
```

## Usage
The component can be used standalone or as part of a form.

Use the `BlazorTurnstile` component to render the Cloudflare Turnstile widget.

```razor
@using BlazorTurnstile

<Turnstile @bind-Token="@_turnstileToken"
           SiteKey="[your-site-key]"
           Theme="@TurnstileTheme.light"
           OnCallback="@TurnstileCallback"
           OnErrorCallback="@TurnstileErrorCallback"
           ResponseField="@false" />
```
