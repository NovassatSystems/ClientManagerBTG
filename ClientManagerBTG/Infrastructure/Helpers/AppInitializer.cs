namespace ClientManagerBTG.Infrastructure.Helpers;
public interface IAppInitializer
{
    Task InitializeAsync();
}

public class AppInitializer : IAppInitializer
{
    private readonly IClientRepository _clientRepository;

    public AppInitializer(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task InitializeAsync()
    {
        if (!await _clientRepository.IsEmptyAsync())
            return;

        var resourceName = "ClientManagerBTG.Resources.Raw.clients.json";
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null) return;

        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var clients = JsonSerializer.Deserialize<List<ClientEntity>>(json);

        if (clients is not null && clients.Count > 0)
            await _clientRepository.SaveAllAsync(clients);
    }
}
