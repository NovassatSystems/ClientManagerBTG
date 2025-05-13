namespace ClientManagerBTG.Shared.Behaviors;

public class AnimateOnNewBehavior : Behavior<Border>
{
    protected override void OnAttachedTo(Border bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.BindingContextChanged += OnBindingContextChanged;
    }

    protected override void OnDetachingFrom(Border bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.BindingContextChanged -= OnBindingContextChanged;
    }

    private async void OnBindingContextChanged(object sender, EventArgs e)
    {
        if (sender is not Border border || border.BindingContext is not ClientModel model)
            return;

        if (model.IsNew || model.IsEdited)
        {
            var originalColor = border.BackgroundColor;
            var highlight = model.IsNew
                ? Color.FromArgb("#001E62")   // Amarelo para novo
                : Color.FromArgb("#B3E5FC");  // Azul claro para edição

            for (int i = 0; i < 3; i++)
            {
                await border.ColorTo(highlight, TimeSpan.FromMilliseconds(250));
                await border.ColorTo(originalColor, TimeSpan.FromMilliseconds(250));
            }

            model.IsNew = false;
            model.IsEdited = false;
        }
    }
}

public static class BorderAnimationExtensions
{
    public static Task<bool> ColorTo(this Border border, Color toColor, TimeSpan duration)
    {
        Color fromColor = border.BackgroundColor;

        var tcs = new TaskCompletionSource<bool>();

        new Animation(v =>
        {
            var r = fromColor.Red + (toColor.Red - fromColor.Red) * v;
            var g = fromColor.Green + (toColor.Green - fromColor.Green) * v;
            var b = fromColor.Blue + (toColor.Blue - fromColor.Blue) * v;
            var a = fromColor.Alpha + (toColor.Alpha - fromColor.Alpha) * v;

            border.BackgroundColor = new Color((float)r, (float)g, (float)b, (float)a);


        }).Commit(border, "ColorAnimation", length: (uint)duration.TotalMilliseconds, finished: (v, c) => tcs.SetResult(c));

        return tcs.Task;
    }
}