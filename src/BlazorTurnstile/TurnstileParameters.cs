namespace BlazorTurnstile;

internal record TurnstileParameters(string Sitekey)
{
    public string Action { get; set; } = string.Empty;
    public TurnstileAppearance? Appearance { get; set; }
    public TurnstileTheme? Theme { get; set; }
    public TurnstileSize? Size { get; set; }
    public TurnstileExecution? Execution { get; set; }
    public bool? ResponseField { get; set; }
    public string? ResponseFieldName { get; set; }
}
