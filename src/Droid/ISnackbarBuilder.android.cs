namespace Plugin.Toast.Droid
{
    interface ISnackbarBuilder
    {
        string? ActionText { get; }
        int? ActionTextColor { get; }
        int SnackbarDuration { get; }
        string Text { get; }
    }
}