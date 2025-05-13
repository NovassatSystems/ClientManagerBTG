namespace ClientManagerBTG.Infrastructure.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder ConfigureRepository(this MauiAppBuilder app)
    {
        app.Services.AddSingleton<IClientRepository, ClientRepository>();
        return app;
    }

    public static MauiAppBuilder ConfigureHelpers(this MauiAppBuilder app)
    {
        app.Services.AddSingleton<IAppInitializer, AppInitializer>();
        return app;
    }

    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder app)
    {
        app.Services.AddSingleton<IWindowService, WindowService>();
        return app;
    }

    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder app)
    {
        
        app.Services.AddTransient<ClientsListPage>();
        app.Services.AddTransient<ClientEditPage>();

        return app;
    }

    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder app)
    {
        app.Services.AddTransient<BaseViewModel>();
        app.Services.AddTransient<ClientsListViewModel>();
        app.Services.AddTransient<ClientEditViewModel>();

        return app;
    }

    public static MauiAppBuilder ConfigureCustomFonts(this MauiAppBuilder app)
    {
        app.ConfigureFonts(fonts =>
        {
            fonts.AddFont("Prometo.otf", "Prometo");
            fonts.AddFont("PrometoBlack.otf", "PrometoBlack");
            fonts.AddFont("PrometoBlackItalic.otf", "PrometoBlackItalic");
            fonts.AddFont("PrometoBold.otf", "PrometoBold");
            fonts.AddFont("PrometoBoldItalic.otf", "PrometoBoldItalic");
            fonts.AddFont("PrometoItalic.otf", "PrometoItalic");
            fonts.AddFont("PrometoLight.otf", "PrometoLight");
            fonts.AddFont("PrometoLightItalic.otf", "PrometoLightItalic");
            fonts.AddFont("PrometoMedium.otf", "PrometoMedium");
            fonts.AddFont("PrometoMediumItalic.otf", "PrometoMediumItalic");
            fonts.AddFont("PrometoThin.otf", "PrometoThin");
            fonts.AddFont("PrometoThinItalic.otf", "PrometoThinItalic");
            fonts.AddFont("PrometoXBold.otf", "PrometoXBold");
            fonts.AddFont("PrometoXBoldItalic.otf", "PrometoXBoldItalic");
            
        });

        return app;
    }
}