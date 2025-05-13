using ClientManagerBTG.Infrastructure.Extensions;
using ClientManagerBTG.Infrastructure.Helpers;

namespace ClientManagerBTG;

public partial class App : Application
{
    public App(IServiceProvider services)
    {
        InitializeComponent();
        var initializer = services.GetRequiredService<IAppInitializer>();
        Task.Run(() => initializer.InitializeAsync());
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());

        window.SetWindowMaximized();
        
        return window;
    }
}