namespace ClientManagerBTG.Features.Clients.List;

public partial class ClientsListViewModel : BaseViewModel
{
    #region Services
    readonly IClientRepository _clientRepository;
    #endregion

    #region Properties
    [ObservableProperty] public ObservableCollection<ClientModel> allClients;
    #endregion

    #region Commands
    [RelayCommand]
    private async Task OpenLinkedInAsync()
    {
        var uri = new Uri("https://www.linkedin.com/in/peternovassat");
        await Launcher.Default.OpenAsync(uri);
    }
    [RelayCommand]
     async Task OpenEditClientAsync(ClientModel client)
    {
        client.IsEdited = true;
        _windowService.OpenWindowCentered<ClientEditPage, ClientEditViewModel, ClientModel>(client);
    }

    [RelayCommand]
     async Task DeleteClientAsync(ClientModel client)
    {
        var confirm = await Application.Current.Windows.Last().Page.DisplayAlert(
            "Confirmar exclusão",
            $"Deseja realmente excluir {client.Name} {client.Lastname}?",
            "Excluir", "Cancelar");

        if (!confirm)
            return;

        await _clientRepository.DeleteAsync(client.Id);

        // Remove da lista visível
        var item = AllClients.FirstOrDefault(c => c.Id == client.Id);
        if (item is not null)
            AllClients.Remove(item);
    }

    [RelayCommand]
     void OpenAddClient()
    {
        var novo = new ClientModel
        {
            Id = Guid.NewGuid(),
            IsNew = true
        };

        _windowService.OpenWindowCentered<ClientEditPage, ClientEditViewModel, ClientModel>(novo);
    }
    #endregion

    #region Ctor
    public ClientsListViewModel(IClientRepository clientRepository, IWindowService windowService) : base(windowService)
    {
        _clientRepository = clientRepository;
        AllClients = [];

        WeakReferenceMessenger.Default.Register<ClientEditedMessage>(this, async (r, msg) =>
        {
            var updatedEntity = await _clientRepository.GetByIdAsync(msg.Value);
            var updatedModel = (ClientModel)updatedEntity;

            var index = AllClients.ToList().FindIndex(c => c.Id == msg.Value);
            if (index >= 0)
            {
                updatedModel.IsEdited = true;
                AllClients[index] = updatedModel;

                _ = Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    updatedModel.IsEdited = false;
                });
            }
        });

        WeakReferenceMessenger.Default.Register<ClientAddedMessage>(this, (r, msg) =>
        {
            msg.Value.IsNew = true;
            AllClients.Insert(0, msg.Value);

            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                msg.Value.IsNew = false;
            });
        });
    }
    #endregion

    #region Methods
    public async Task LoadAsync()
    {
        var items = await _clientRepository.GetAllAsync();

        var converted = items.Select(entity => (ClientModel)entity).ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            AllClients = [.. converted];
        });
    }

    
    #endregion
}