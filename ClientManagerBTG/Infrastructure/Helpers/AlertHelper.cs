namespace ClientManagerBTG.Infrastructure.Helpers;

public static class AlertHelper
{
    public static async Task ShowAsync(string title, string message, string ok = "OK")
    {
        var currentWindow = Application.Current.Windows.Last();
        await currentWindow.Page.DisplayAlert(title, message, ok);
    }
}
