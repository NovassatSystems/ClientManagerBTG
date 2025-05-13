namespace ClientManagerBTG.Features.Clients.List;

public partial class ClientsListPage : ContentPage
{
    ClientsListViewModel _viewModel => BindingContext as ClientsListViewModel;
    public ClientsListPage(ClientsListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}