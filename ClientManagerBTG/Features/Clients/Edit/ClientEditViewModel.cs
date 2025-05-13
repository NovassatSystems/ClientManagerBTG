namespace ClientManagerBTG.Features.Clients.List;

public partial class ClientEditViewModel : BaseViewModel
{
    #region Services
    private readonly IClientRepository _clientRepository;
    #endregion

    #region Properties
    private ClientModel _original;

    [ObservableProperty] private string name;
    [ObservableProperty] private string lastname;
    [ObservableProperty] private int age;
    [ObservableProperty] private string address;
    [ObservableProperty] private string ageText;
    [ObservableProperty] private bool isNew;
    #endregion

    #region Commands

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Lastname) ||
            string.IsNullOrWhiteSpace(Address) ||
            Age <= 0 || Age > 100)
        {
            await AlertHelper.ShowAsync("Campos inválidos", "Preencha os campos corretamente.");

            return;
        }

        var model = new ClientModel
        {
            Id = _original.Id,
            Name = Name.Trim(),
            Lastname = Lastname.Trim(),
            Age = Age,
            Address = Address.Trim()
        };

        var exists = await _clientRepository.GetByIdAsync(model.Id) != null;

        if (exists)
        {
            await _clientRepository.UpdateAsync(model);
            WeakReferenceMessenger.Default.Send(new ClientEditedMessage(model.Id));
        }
        else
        {
            await _clientRepository.AddAsync(model);
            WeakReferenceMessenger.Default.Send(new ClientAddedMessage(model));
        }

        _windowService.ClosePage();
    }

    #endregion

    #region Ctor
    public ClientEditViewModel(IWindowService windowService, IClientRepository clientRepository) : base(windowService)
    {
        _clientRepository = clientRepository;
    }
    #endregion

    #region Methods

    public void Load(ClientModel model)
    {
        _original = model;

        Name = model.Name;
        Lastname = model.Lastname;
        Age = model.Age;
        Address = model.Address;
        AgeText = model.Age > 0 ? model.Age.ToString() : string.Empty;
        IsNew = model.IsNew;
    }

    public async Task LoadAsync()
    {
        
    }

    private bool CanSave()
    {
        if (_original == null || _original.IsNew) return true;

        return
            Name?.Trim() != _original.Name ||
            Lastname?.Trim() != _original.Lastname ||
            Address?.Trim() != _original.Address ||
            AgeText?.Trim() != _original.Age.ToString();
    }

    #endregion

    #region Property Change Triggers

    partial void OnNameChanged(string oldValue, string newValue) => SaveCommand.NotifyCanExecuteChanged();
    partial void OnLastnameChanged(string oldValue, string newValue) => SaveCommand.NotifyCanExecuteChanged();
    partial void OnAddressChanged(string oldValue, string newValue) => SaveCommand.NotifyCanExecuteChanged();

    partial void OnAgeTextChanged(string oldValue, string newValue)
    {
        SaveCommand.NotifyCanExecuteChanged();

        if (int.TryParse(newValue, out var parsed) && parsed <= 100)
            Age = parsed;
        else if (string.IsNullOrWhiteSpace(newValue))
            Age = -1;
    }

    #endregion
}
