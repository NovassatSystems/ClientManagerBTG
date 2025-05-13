using ClientManagerBTG.Features.Clients;
using ClientManagerBTG.Infrastructure.Extensions;
using ClientManagerBTG.Shared.Repository;
using CommunityToolkit.Maui;
using epj.RouteGenerator;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;

namespace ClientManagerBTG;

[AutoRoutes("Page")]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureRepository()
            .ConfigureHelpers()
            .ConfigureServices()
            .ConfigurePages()
            .ConfigureViewModels()
            .ConfigureCustomFonts();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        var app =  builder.Build();

        return app;
    }
}
