namespace BlazorApp1;

public static class UiHelpers
{
    // First letter of a name, used for the round avatar badges.
    public static string Initial(string? name) =>
        string.IsNullOrWhiteSpace(name) ? "?" : name.Trim()[..1].ToUpperInvariant();
}
