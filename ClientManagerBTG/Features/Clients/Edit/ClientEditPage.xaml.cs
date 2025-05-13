namespace ClientManagerBTG.Features.Clients.List;

public partial class ClientEditPage : ContentPage
{
	ClientEditViewModel _viewModel => BindingContext as ClientEditViewModel;
	public ClientEditPage(ClientEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }

    void AgeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        
        var digitsOnly = new string(entry.Text?.Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(digitsOnly))
        {
            entry.Text = string.Empty;
            return;
        }

        
        if (int.TryParse(digitsOnly, out int value))
        {
            if (value > 100)
                value = 100;

            entry.Text = value.ToString();
        }
        else
        {
            entry.Text = string.Empty;
        }
    }
}